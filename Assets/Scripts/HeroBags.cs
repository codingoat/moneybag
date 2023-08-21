using System;
using UnityEngine;
using ZUtils;

namespace Moneybag
{
    public class HeroBags : MonoBehaviour
    {
        private GameObject[] bags; 
        
        private Hero hero;

        private void Awake()
        {
            hero = GetComponentInParent<Hero>();
            
            bags = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                bags[i] = transform.GetChild(i).gameObject;
        }

        private void Update()
        {
            for (int i = 0; i < bags.Length; i++) 
                bags[i].SetActive(i < hero.Bags);
        }
    }
}