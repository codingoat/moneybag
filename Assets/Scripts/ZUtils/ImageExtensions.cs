using UnityEngine.UI;

namespace ZUtils
{
    public static class ImageExtensions
    {
        public static void SetColor(this Image image, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            image.color = image.color.WithComponents(r, g, b, a);
        }

        public static void SetAlpha(this Image image, float a)
        {
            image.color = image.color.WithAlpha(a);
        }
    }
}