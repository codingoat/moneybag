using System;
using System.Collections.Generic;
using System.Linq;
using Moneybag.Misc;
using UnityEngine;

namespace Moneybag
{
    [RequireComponent(typeof(Collider))]
    public class HeroDetector : MonoBehaviour
    {
        private Dictionary<Hero, int> collidingHeroes = new();
        
        public Hero[] Heroes => collidingHeroes.Where(ch => ch.Value > 0)
            .Select(ch => ch.Key)
            .ToArray();
        public bool HasHeroes => collidingHeroes.Any(ch => ch.Value > 0);

        private void Awake()
        {
            gameObject.layer = Layers.HeroDetector;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                int value = collidingHeroes.GetValueOrDefault(hero);
                collidingHeroes[hero] = value + 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                int value = collidingHeroes.GetValueOrDefault(hero);
                collidingHeroes[hero] = value - 1;
            }
        }
    }
}