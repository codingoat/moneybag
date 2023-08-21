using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Moneybag
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PartyManager : MonoBehaviour
    {
        [SerializeField] private Color[] playerColors;

        private List<Hero> heroes = new();

        private void Awake()
        {
            GetComponent<PlayerInputManager>().onPlayerJoined += input =>
            {
                Hero hero = input.GetComponent<Hero>(); 
                heroes.Add(hero);
                hero.GetComponent<HeroColor>().Color = playerColors[heroes.Count - 1];
            };
        }
    }
}