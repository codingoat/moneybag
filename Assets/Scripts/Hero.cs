using System;
using Moneybag.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using ZUtils;

namespace Moneybag
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 9,
            rotationSpeed = 15,
            groundCheckRadius = .35f;

        [Serializable]
        public class ActionCooldowns
        {
            public float smack = 1.5f, block = .8f;
        }
        [SerializeField] private ActionCooldowns actionCooldowns;

        [SerializeField] private MoneyStack moneyStackPrefab;
        [SerializeField] private MoneyPickup moneyPickupPrefab;
        [SerializeField] private HeroDetector attackHitbox;

        [SerializeField] private ParticleSystem weaponGlow, weaponTrail;
        
        [SerializeField] private AudioSource weaponAudio;
        [SerializeField] private AudioClip sfxSwing, sfxHit, sfxBlock, sfxClick, sfxMoneyPickup;

        public float ActionCooldownTimer { get; private set; }
        private bool CanDoAction => ActionCooldownTimer <= 0 && !KnockedBack;
        public bool Blocking { get; private set; }

        [SerializeField] private float knockbackLength, knockbackStrength;
        private float knockbackProgress;
        private Vector3 knockbackDirection;
        private bool KnockedBack => knockbackProgress < 1;
        
        private Rigidbody rg;
        private Animator animator;

        private readonly int ANIM_MOVE_SPEED = Animator.StringToHash("MoveSpeed"),
            ANIM_SMACK = Animator.StringToHash("Smack"),
            ANIM_BLOCK = Animator.StringToHash("Block");

#region Properties

        private Vector2 inputDirection = Vector2.zero;

        public Vector3 MoveDirection => Vector3.ClampMagnitude(new Vector3(inputDirection.x, 0, inputDirection.y), 1);

        private bool IsGrounded
        {
            get
            {
                Collider[] colls = new Collider[1];
                // ground
                return Physics.OverlapSphereNonAlloc(rg.position, groundCheckRadius, colls,
                    Layers.CreateMask(Layers.Ground)) > 0;
            }
        }

        public Vector3 Direction => transform.forward;

#endregion

#region Lifecycle

        private void Awake()
        {
            rg = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            
            weaponGlow.Stop();
            weaponTrail.Stop();
        }

        private void Update()
        {
            Vector3 moveDirection = MoveDirection;
            
            if (moveDirection.sqrMagnitude > 0.05f) // rotate hero smoothly
                rg.rotation = Quaternion.Slerp(rg.rotation, Quaternion.LookRotation(moveDirection),
                    Time.deltaTime * rotationSpeed);
            
            // timers
            ActionCooldownTimer = Mathf.Max(0, ActionCooldownTimer - Time.deltaTime);
            if (KnockedBack) knockbackProgress = Mathf.Min(1, knockbackProgress + Time.deltaTime / knockbackLength);
        }

        // since the character is driven by a rigidbody,
        // getting stick input and handling movement in fixed update is fine
        private void FixedUpdate()
        {
            // movement
            Vector3 velocity;

            if (KnockedBack) velocity = Easing.OutQuad(1 - knockbackProgress) * knockbackStrength * knockbackDirection; 
            else velocity = MoveDirection * moveSpeed;
            
            velocity += rg.velocity.y * Vector3.up; // apply gravity from rg
            
            // don't fly off ramps
            if (velocity.y > 0 && !IsGrounded) velocity.y = 0;

            rg.velocity = velocity;
        }

#endregion

#region Money

        public int Money { get; private set; }
        public int Bags => Money / Params.bagValue;
        
        /// <returns>The amount of money actually taken.</returns>
        public int TakeMoney(int desiredAmount)
        {
            int returnedAmount = Mathf.Min(desiredAmount, Money);
            Money -= returnedAmount;
            return returnedAmount;
        }

        public void AddMoney(int amount)
        {
            Money += amount;
            weaponAudio.PlayOneShot(sfxMoneyPickup);
        }
        
        public void CollectBag(MoneyPickup moneyPickup) => AddMoney(moneyPickup.Value);

#endregion

#region Input

        public void Move(InputAction.CallbackContext ctx)
        {
            inputDirection = ctx.ReadValue<Vector2>();
            animator.SetFloat(ANIM_MOVE_SPEED, inputDirection.magnitude);
        }

        private bool CheckCanDoAction()
        {
            if(!CanDoAction)
            {
                weaponAudio.PlayOneShot(sfxClick);
                return false;
            }

            return true;
        }
        
        public void Smack(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed || !CheckCanDoAction()) return;
            
            animator.SetTrigger(ANIM_SMACK);
            ActionCooldownTimer = actionCooldowns.smack;
        }

        public void Block(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed || !CheckCanDoAction()) return;
            
            animator.SetTrigger(ANIM_BLOCK);
            ActionCooldownTimer = actionCooldowns.block;
        }
        
#endregion

#region Smacking

        private bool lastSmackHitPlayers = false;
        
        /// <remarks>Called from animation</remarks>
        public void SmackStart()
        {
            if (!CanDoAction) return;
            
            lastSmackHitPlayers = attackHitbox.HasHeroes;
            bool lastSmackHitMeat = false, lastSmackHitBlock = false;
            
            if (attackHitbox.HasHeroes)
            {
                foreach (Hero otherHero in attackHitbox.Heroes)
                {
                    if (otherHero == this) continue;
                    if (otherHero.IsBlockingFromDirection(otherHero.transform.position - transform.position))
                    {
                        lastSmackHitBlock = true;
                        continue; // TODO: also stun attacker?
                    }

                    int takenMoney = otherHero.TakeMoney(1);
                    if (takenMoney > 0) Instantiate(moneyStackPrefab).Animate(otherHero.transform.position, transform);

                    otherHero.KnockBack(otherHero.transform.position - transform.position);
                    
                    lastSmackHitMeat = true;
                }
            }
            
            weaponTrail.Play();
            weaponAudio.PlayOneShot(sfxSwing);
            if (lastSmackHitMeat) weaponAudio.PlayOneShot(sfxHit);
            if (lastSmackHitBlock) weaponAudio.PlayOneShot(sfxBlock);
        }
        
        private const float thrownMoneySpeed = 7;
        /// <remarks>Called from animation</remarks>
        public void SmackEnd()
        {
            if (!lastSmackHitPlayers)
            {
                int lostMoney = TakeMoney(1);
                if (lostMoney > 0)
                {
                    Instantiate(moneyPickupPrefab, transform.position - transform.forward / 2 + Vector3.up, Quaternion.identity)
                        .Throw((transform.forward * -1 + Vector3.up / 2).normalized * thrownMoneySpeed);
                }
            }
            
            weaponTrail.Stop();
        }

        public void KnockBack(Vector3 direction)
        {
            knockbackDirection = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
            knockbackProgress = 0;
        }

#endregion

#region Blocking

        public bool IsBlockingFromDirection(Vector3 attackDirection)
        {
            if (!Blocking) return false;
            
            Vector3 projectedDirection = -1 * Vector3.ProjectOnPlane(attackDirection, Vector3.up);
            return Vector3.Angle(projectedDirection, Direction) < Params.blockAngle;
        }

        /// <remarks>Called from animation</remarks>
        public void BlockStart()
        {
            Blocking = true;
            weaponGlow.Play();
            weaponAudio.PlayOneShot(sfxSwing);
        }

        /// <remarks>Called from animation</remarks>
        public void BlockEnd()
        {
            Blocking = false;
            weaponGlow.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

#endregion
        
    }
}

