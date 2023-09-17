using System;
using UnityEngine;

namespace Moneybag.UI
{
    public class ThrowAimArrow : MonoBehaviour
    {
        private Hero hero;
        private HeroColor heroColor;
        private SpriteRenderer spriteRenderer;
        
        private static readonly int MAT_FILL_COLOR = Shader.PropertyToID("_FillColor"),
            MAT_FILL_AMOUNT = Shader.PropertyToID("_FillAmount");

        private void Awake()
        {
            hero = GetComponentInParent<Hero>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            heroColor = hero.GetComponent<HeroColor>();
        }

        private void Update()
        {
            spriteRenderer.enabled = hero.ChargingThrow;
            
            if (hero.ChargingThrow)
            {
                spriteRenderer.material.SetColor(MAT_FILL_COLOR, heroColor.Color);
                spriteRenderer.material.SetFloat(MAT_FILL_AMOUNT, hero.ThrowChargeProgress);
            }
        }
    }
}