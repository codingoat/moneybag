using System;
using UnityEngine;
using UnityEngine.Events;

namespace Moneybag
{
    public class Zone : MonoBehaviour
    {
        public Vector3 center = new Vector3(0, .5f, 0);
        public Vector3 size = Vector3.one;
        [SerializeField] private Color color = new Color(0, 1, 1, .5f);

        public Bounds Bounds => new(transform.position + center, size);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + center, size);
        }
    }
}