using UnityEngine;

namespace ZUtils
{
    public static class BoundsExtensions
    {
        public static Vector3 Sample(this Bounds bounds) =>
            bounds.Sample(new Vector3(Random.value, Random.value, Random.value));
        public static Vector3 Sample(this Bounds bounds, Vector3 coords) =>
            bounds.min + Vector3.Scale(bounds.size, coords);
    }
}