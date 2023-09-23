using System.Collections;
using UnityEngine;

namespace Moneybag
{
    public class ItemSpawner : MonoBehaviour
    {
        private static ItemSpawner instance;
        public static ItemSpawner Instance => instance ? instance : instance = FindFirstObjectByType<ItemSpawner>();

        [SerializeField] private float stackSpawnStagger = .1f;
        
        [SerializeField] private MoneyStack moneyStackPrefab;

        public void SpawnStacks(Hero from, Hero to, int count = 1) => 
            StartCoroutine(SpawnStacks_Internal(from, to, count));
        
        private IEnumerator SpawnStacks_Internal(Hero from, Hero to, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                MoneyStack stack = Instantiate(moneyStackPrefab);
                stack.Animate(from.transform.position, to.transform, to);
                yield return new WaitForSeconds(stackSpawnStagger);
            }
        }
    }
}