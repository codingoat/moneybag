using System.Linq;
using UnityEngine;
using ZUtils;
using Random = UnityEngine.Random;

namespace Moneybag
{
    public class BagSpawner : MonoBehaviour
    {
        [SerializeField] private MoneyPickup moneyPickupPrefab;
        [SerializeField] private AnimationCurve spawnCurve;
        [SerializeField] private Transform spawnZonesParent;
        
        private Bounds[] spawnZones;

        private int spawnedMoney = 0;

        private int TargetSpawnedMoney => Mathf.RoundToInt(GameManager.Instance.HeroCount * Params.bagsToWin *
                                                           Params.bagValue * Params.spawnedMoneyMultiplier);

        private void Awake()
        {
            spawnZones = spawnZonesParent.GetComponentsInChildren<Zone>().Select(zone => zone.Bounds).ToArray();
        }

        private void Update()
        {
            int desiredMoney = Mathf.RoundToInt(spawnCurve.Evaluate(GameManager.Instance.GameProgress) * TargetSpawnedMoney);
            
            
            while (spawnedMoney < desiredMoney)
            {
                Vector3 randomPoint = spawnZones.PickRandom().Sample();
                if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hitInfo))
                {
                    MoneyPickup moneyPickup = Instantiate(moneyPickupPrefab, hitInfo.point, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    
                    
                    moneyPickup.Value = Mathf.Min(GetRandomValue(), desiredMoney - spawnedMoney);
                    spawnedMoney += moneyPickup.Value;
                }
            }
            
            int GetRandomValue()
            {
                float rand = Random.value;
                if (rand < 0.05f) return 3;
                if (rand < .3) return 2;
                return 1;
            }
        }
    }
}