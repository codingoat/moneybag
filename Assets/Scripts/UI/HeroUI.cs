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
        [SerializeField] private Image heroIcon, cooldownBar;
        
        private List<BagIcon> bags = new();

        private Hero hero;
        private HeroColor heroColor;
        public Hero Hero
        {
            get => hero;
            set
            {
                hero = value;
                heroColor = value.GetComponent<HeroColor>();
            }
        }

        private float maxCooldown = 0;

        private void Awake()
        {
            bagsParent.DestroyAllChildren();
        }

        private void Update()
        {
            if (!heroColor) return;

            cooldownBar.color = heroIcon.color = heroColor.Color;
            
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
            
            
            // update cooldown bar
            if (Hero.ActionCooldownTimer == 0) // bar full
            {
                maxCooldown = 0;
                cooldownBar.fillAmount = 1;
                cooldownBar.SetAlpha(1);
            }
            else
            {
                if (Hero.ActionCooldownTimer > maxCooldown) maxCooldown = hero.ActionCooldownTimer;
                cooldownBar.fillAmount = Easing.OutSine(1 - Hero.ActionCooldownTimer / maxCooldown);
                cooldownBar.SetAlpha(.2f);
            }
        }
    }
}