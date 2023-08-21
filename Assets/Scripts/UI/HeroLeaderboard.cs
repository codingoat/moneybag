using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZUtils;

namespace Moneybag.UI
{
    public class HeroLeaderboard : MonoBehaviour
    {
        [SerializeField] private HeroUI heroUIPrefab;
        [SerializeField] private PlayerInputManager inputManager;

        private void Awake()
        {
            transform.DestroyAllChildren();
            
            inputManager.onPlayerJoined += input =>
            {
                HeroUI heroUI = Instantiate(heroUIPrefab, transform);
                heroUI.Hero = input.GetComponent<Hero>();
            };
        }
    }
}