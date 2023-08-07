using System;
using Moneybag.Misc;
using UnityEngine;

namespace Moneybag
{
    public class Bag : MonoBehaviour
    {
        [SerializeField] private GameObject[] models;
        
        private int value;
        public int Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp(value, 0, models.Length);
                for(int i=0; i<models.Length; i++) models[i].SetActive(i < Value);
            }
        }

        private void Awake()
        {
            Value = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != Layers.Hero) return;
            
            other.GetComponent<Hero>().CollectBag(this);
            Destroy(gameObject);
        }
    }
}