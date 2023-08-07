using UnityEngine;

namespace ZUtils
{
    public static class RectExtensions
    {
        public static Rect WithPadding(this Rect rect, float padding) =>
            new Rect(rect.position + Vector2.one * padding, rect.size - Vector2.one * padding * 2);

        public static Vector2 Clamp(this Rect rect, Vector2 vec) =>
            new Vector2(Mathf.Clamp(vec.x, rect.xMin, rect.xMax), Mathf.Clamp(vec.y, rect.yMin, rect.yMax));

        public static Vector2 ClampDirectional(this Rect rect, Vector2 vec)
        {
            if (vec.y < rect.yMin) vec *= rect.yMin / vec.y;
            else if (vec.y > rect.yMax) vec *= rect.yMax / vec.y;
            
            if (vec.x < rect.xMin) vec *= rect.xMin / vec.x;
            else if (vec.x > rect.xMax) vec *= rect.xMax / vec.x;
            
            return vec;
        }

        /// <summary>Converts a normalized coordinate (<paramref name="samplePoint"/>) of the rect to regular a coordinate.</summary>
        public static Vector2 Sample(this Rect rect, Vector2 samplePoint) => rect.min + rect.size * samplePoint;
        /// <summary>Converts a normalized coordinate (<paramref name="x"/>, <paramref name="y"/>) of the rect to regular a coordinate.</summary>
        public static Vector2 Sample(this Rect rect, float x, float y) => rect.Sample(new Vector2(x, y));
        /// <summary>Picks a random point inside the Rect.</summary>
        public static Vector2 Sample(this Rect rect) => rect.Sample(new Vector2(Random.value, Random.value));
    }
}