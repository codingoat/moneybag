using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ZUtils;

namespace Moneybag.UI
{
    public class HeroUI : MonoBehaviour
    {
        [SerializeField] private Transform bagsParent;
        [SerializeField] private BagIcon bagPrefab;
        [SerializeField] private Image heroIcon;

        private List<BagIcon> bags = new();

        private Hero hero;
        private HeroColor heroColor;
        public Hero Hero
        {
            set
            {
                hero = value;
                heroColor = value.GetComponent<HeroColor>();
            }
        }

        private void Awake()
        {
            bagsParent.DestroyAllChildren();
        }

        private void Update()
        {
            if (!heroColor) return;

            heroIcon.color = heroColor.Color;
            
            // update bag count
            int desiredBags = hero.Money / Params.bagValue + 1;
            if (desiredBags > bags.Count) // add more bags
            {
                if (bags.Count > 0) bags[^1].Progress = 1; // non-last bag should be full
                
                for (int i = 0; i < desiredBags - bags.Count; i++)
                {
                    bags.Add(Instantiate(bagPrefab, bagsParent));
                    bags[^1].Progress = 1;
                }
            }
            else if (desiredBags < bags.Count) // remove bags
            {
                for (int i = 0; i < bags.Count - desiredBags; i++)
                {
                    Destroy(bags[^1].gameObject);
                    bags.RemoveAt(bags.Count-1);
                }
            }
            
            // update bag progress
            bags[^1].Progress = (hero.Money % Params.bagValue) / (float)Params.bagValue;
        }
    }
}