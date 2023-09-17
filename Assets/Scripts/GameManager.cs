using System;
using System.Collections.Generic;
using System.Linq;
using Moneybag.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using ZUtils;

namespace Moneybag
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance ? instance : instance = FindFirstObjectByType<GameManager>();
        
        private PlayerInputManager playerInputManager;
        
        private List<Hero> heroes = new();
        public int HeroCount => heroes.Count;

        private bool isGameRunning, isGameOver;
        private float gameTimer;
        public float GameProgress => gameTimer / Params.gameLength;
        
        private void Awake()
        {
            playerInputManager = GetComponent<PlayerInputManager>();
            playerInputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerJoined(PlayerInput input)
        {
            Hero hero = input.GetComponent<Hero>();
            heroes.Add(hero);
            hero.Smacked += OnHeroSmacked;
        }

        private void OnHeroSmacked(Hero target, Hero attacker)
        {
            if (!isGameRunning) isGameRunning = true;
        }

        private void Update()
        {
            if (!isGameRunning || isGameOver) return;

            gameTimer = Mathf.Min(gameTimer + Time.deltaTime, Params.gameLength);

            if (heroes.Any(hero => hero.Bags >= Params.bagsToWin))
            {
                isGameOver = true;
                MessageUI.Instance.AddMessage("GAME OVER", 3);
                this.Delay(4, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            }
        }
    }
}