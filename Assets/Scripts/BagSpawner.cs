using System;
using System.Linq;
using UnityEngine;
using ZUtils;
using Random = UnityEngine.Random;

namespace Moneybag
{
    public class BagSpawner : MonoBehaviour
    {
        [SerializeField] private Bag bagPrefab;
        [SerializeField] private float spawnDelay = 8;
        [SerializeField] private Transform spawnZonesParent;
        
        private Bounds[] spawnZones;

        private float spawnTimer;

        private void Awake()
        {
            spawnZones = spawnZonesParent.GetComponentsInChildren<Zone>().Select(zone => zone.Bounds).ToArray();
        }

        private void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                spawnTimer -= spawnDelay;
                Vector3 randomPoint = spawnZones.PickRandom().Sample();
                if (Physics.Raycast(randomPoint, Vector3.down, out RaycastHit hitInfo))
                {
                    Bag bag = Instantiate(bagPrefab, hitInfo.point, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    bag.Value = Random.Range(1, 3);
                }
            }
        }
    }
}