using System;
using UnityEngine;

namespace ZUtils
{
    // https://easings.net/
    public static class Easing
    {
        public static float Ease(float t, EasingType type)
        {
            return type switch
            {
                EasingType.Linear => t,
                EasingType.InQuad => InQuad(t),
                EasingType.OutQuad => OutQuad(t),
                EasingType.InOutQuad => InOutQuad(t),
                EasingType.InSine => InSine(t),
                EasingType.OutSine => OutSine(t),
                EasingType.InOutSine => InOutSine(t),
                EasingType.InQuint => InQuint(t),
                EasingType.OutQuint => OutQuint(t),
                EasingType.InOutQuint => InOutQuint(t),
                EasingType.OutBack => OutBack(t),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static float EaseClamped(float t, EasingType type) => Ease(Mathf.Clamp01(t), type);

        public enum EasingType
        {
            Linear = 0,
            InQuad = 101,
            OutQuad = 102,
            InOutQuad = 103,
            InSine = 201,
            OutSine = 202,
            InOutSine = 203,
            InQuint = 301,
            OutQuint = 302,
            InOutQuint = 303,
            OutBack = 402,
        }
        
        public static float InQuad(float t) => t * t;
        public static float OutQuad(float t) => 1 - (1 - t) * (1 - t);
        public static float InOutQuad(float t) => t < 0.5 ? 2 * t * t: 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        
        public static float InSine(float t) => 1 - Mathf.Cos((t * Mathf.PI) / 2);
        public static float OutSine(float t) => Mathf.Sin((t * Mathf.PI) / 2);
        public static float InOutSine(float t) => -(Mathf.Cos(t * Mathf.PI) - 1) / 2;

        public static float InQuint(float t) => Mathf.Pow(t, 5);
        public static float OutQuint(float t) => 1 - Mathf.Pow(1 - t, 5);
        public static float InOutQuint(float t) => t < 0.5 ? 16 * Mathf.Pow(t, 5) : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;

        public static float OutBack(float t)
        {
            const float c1  = 1.70158f;
            const float c3  = c1 + 1;
            return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
        }
    }
}