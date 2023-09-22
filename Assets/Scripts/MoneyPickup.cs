using System;
using System.Collections;
using Moneybag.Misc;
using UnityEngine;
using ZUtils;
using Random = UnityEngine.Random;

namespace Moneybag
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class MoneyPickup : MonoBehaviour
    {
        [SerializeField] private GameObject[] models;

        private int value;

        public int Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp(value, 0, models.Length);
                for (int i = 0; i < models.Length; i++) models[i].SetActive(i < Value);
            }
        }

        public bool Collectable
        {
            get => trigger.enabled;
            set => trigger.enabled = value;
        }
        
        private Collider trigger;
        private new Rigidbody rigidbody;

        private void Awake()
        {
            trigger = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            Value = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Collectable || other.gameObject.layer != Layers.Hero) return;

            other.GetComponent<Hero>().CollectBag(this);
            Destroy(gameObject);
        }

        private const float throwSpinSpeed = 2f; 
        public void Throw(Vector3 velocity)
        {
            rigidbody.angularVelocity = new Vector3(Random.value, Random.value, Random.value) * velocity.magnitude * throwSpinSpeed;
            rigidbody.velocity = velocity;
            Collectable = false;
            StartCoroutine(AnimateScaleUp(.1f));
            this.Delay(1, () => Collectable = true);
        }

        private IEnumerator AnimateScaleUp(float length)
        {
            float startTime = Time.time;
            float t;

            while ((t = (Time.time - startTime) / length) < 1)
            {
                transform.localScale = Easing.Ease(t, Easing.EasingType.OutSine) * Vector3.one;
                yield return null;
            }
        }
    }
}