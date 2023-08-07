using System.Diagnostics.Contracts;
using UnityEngine;

namespace ZUtils
{
    public static class VectorExtensions
    {
        [Pure]
        public static Vector3 WithComponents(this Vector3 vec, float? x = null, float? y = null, float? z = null)
            => new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z);

        [Pure]
        public static Vector2 WithComponents(this Vector2 vec, float? x = null, float? y = null)
            => new Vector2(x ?? vec.x, y ?? vec.y);
        
        
        [Pure]
        public static Vector3 Abs(this Vector3 vec) => 
            new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
      
        [Pure]
        public static Vector2 Abs(this Vector2 vec) => 
            new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
        
        
        [Pure]
        public static float SumComponents(this Vector3 vec) => vec.x + vec.y + vec.z;
        
        [Pure]
        public static float SumComponents(this Vector2 vec) => vec.x + vec.y;


        [Pure]
        static float MinComponent(this Vector3 vec) => Mathf.Min(vec.x, vec.y, vec.z);
        
        [Pure]
        public static float MinComponent(this Vector2 vec) => Mathf.Min(vec.x, vec.y);

        [Pure]
        static float MaxComponent(this Vector3 vec) => Mathf.Max(vec.x, vec.y, vec.z);
        
        [Pure]
        public static float MaxComponent(this Vector2 vec) => Mathf.Max(vec.x, vec.y);
        
        [Pure]
        public static float RandomBetweenComponents(this Vector2 vec) => Random.Range(vec.x, vec.y);
    }
}