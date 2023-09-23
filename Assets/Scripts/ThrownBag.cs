using UnityEngine;
using ZUtils;

namespace Moneybag
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrownBag : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private AudioClip sfxBlock;
        
        [Space]
        [SerializeField] private MoneyPickup moneyPickupPrefab; // TODO: centralize to ItemSpawner
        
        private Rigidbody rg;


        private Hero owner;


        private void Awake()
        {
            rg = GetComponent<Rigidbody>();
        }

        public void Throw(Hero owner, Vector3 direction)
        {
            this.owner = owner;
            rg.velocity = direction * speed;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Hero hero))
            {
                if (hero.IsBlockingFromDirection(hero.transform.position - transform.position))
                {
                    AudioSource.PlayClipAtPoint(sfxBlock, transform.position);
                    SpawnPickup();
                }
                else
                {
                    int taken = hero.TakeMoney(Params.bagValue);
                    ItemSpawner.Instance.SpawnStacks(hero, owner, taken + Params.bagValue);
                    hero.KnockBack(transform.forward);
                }
                
            }
            else
            {
                SpawnPickup();
            }
            
            Destroy(gameObject);

            void SpawnPickup()
            {
                var pickup = Instantiate(moneyPickupPrefab, transform.position, Quaternion.identity);
                pickup.Value = Params.bagValue;
                pickup.Delay(.5f, () => pickup.Collectable = true);
            }
        }
    }
}