using System;
using UnityEngine;
using UnityEngine.UI;
using ZUtils;

namespace Moneybag.UI
{
    public class HeroCooldownBar : MonoBehaviour
    {
        [SerializeField] private Image fill;
        
        private Hero hero;
        private HeroColor heroColor;
        private float maxCooldown = 0;

        private void Awake()
        {
            hero = GetComponentInParent<Hero>();
            heroColor = hero.GetComponent<HeroColor>();
        }

        private void Update()
        {
            float fillAlpha;
            if (hero.ActionCooldownTimer == 0) // bar full
            {
                maxCooldown = 0;
                fill.fillAmount = 1;
                fillAlpha = 1;
            }
            else
            {
                if (hero.ActionCooldownTimer > maxCooldown) maxCooldown = hero.ActionCooldownTimer;
                fill.fillAmount = Easing.InSine(1 - hero.ActionCooldownTimer / maxCooldown);
                fillAlpha = .2f;
            }
            fill.color = heroColor.Color.WithAlpha(fillAlpha);
        }
    }
}