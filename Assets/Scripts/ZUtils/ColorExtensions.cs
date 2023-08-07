using UnityEngine;

namespace ZUtils
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float a) => new Color(color.r, color.g, color.b, a);
        
        public static Color WithComponents(this Color color, float? r = null, float? g = null, float? b = null, float? a = null) 
            => new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
    }
}