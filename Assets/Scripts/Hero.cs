using System;
using Moneybag.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using ZUtils;

namespace Moneybag
{
    [RequireComponent(typeof(Rigidbody))]
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 9,
            rotationSpeed = 15,
            groundCheckRadius = .35f;

        public int Money { get; private set; }
        
        private Rigidbody rg;
        private PlayerInput playerInput;

        private readonly int ANIM_MOVE_SPEED = Animator.StringToHash("MoveSpeed");

#region Properties

        private Vector2 inputDirection = Vector2.zero;

        public Vector3 MoveDirection
        {
            get
            {
                return Vector3.ClampMagnitude(new Vector3(inputDirection.x, 0, inputDirection.y), 1);
            }
        }
        
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

#endregion

#region Lifecycle

        private void Awake()
        {
            rg = GetComponent<Rigidbody>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Vector3 moveDirection = MoveDirection;
            
            if (moveDirection.sqrMagnitude > 0.05f) // rotate hero smoothly
                rg.rotation = Quaternion.Slerp(rg.rotation, Quaternion.LookRotation(moveDirection),
                    Time.deltaTime * rotationSpeed);
        }

        // since the character is driven by a rigidbody,
        // getting stick input and handling movement in fixed update is fine
        private void FixedUpdate()
        {
            // movement
            Vector3 velocity = MoveDirection * moveSpeed + rg.velocity.y * Vector3.up;
            
            // don't fly off ramps
            if (velocity.y > 0 && !IsGrounded) velocity.y = 0;

            rg.velocity = velocity;
        }

#endregion

#region Input

        public void Smack(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            
            Debug.Log("SMACK");
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            inputDirection = ctx.ReadValue<Vector2>();
            Debug.Log(inputDirection);
        }

#endregion

        public void CollectBag(Bag bag) => Money += bag.Value;
    }
}

