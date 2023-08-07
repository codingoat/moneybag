using UnityEngine;

namespace ZUtils
{
    public static class LineRendererExtensions
    {
        public static void SetColor(this LineRenderer lineRenderer, Color color) => 
            lineRenderer.colorGradient = new Gradient { colorKeys = new[] { new GradientColorKey(color, 0) } };

        public static void SetWidth(this LineRenderer lineRenderer, float width) =>
            lineRenderer.startWidth = lineRenderer.endWidth = width;
    }
}