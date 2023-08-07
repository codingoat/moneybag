using UnityEngine;

namespace ZUtils
{
    public static class RectTransformExtensions
    {
        public static void SetSizeWithCurrentAnchors(this RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
    }
}