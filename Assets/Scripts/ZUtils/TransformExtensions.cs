using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZUtils
{
    public static class TransformExtensions
    {
        public static void SetLocalPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localPosition = transform.localPosition.WithComponents(x, y, z);   
        }
        
        public static void SetLocalScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localScale = transform.localScale.WithComponents(x, y, z);   
        }
        
        public static void SetRotation(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithComponents(x, y, z));
        }
        
        public static void SetLocalRotation(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.WithComponents(x, y, z));
        }

        public static void ForEachChild(this Transform transform, Action<Transform> function)
        {
            for (int i = transform.childCount-1; i >= 0; i--) function(transform.GetChild(i));
        }

        public static void DestroyAllChildren(this Transform transform) =>
            transform.ForEachChild(c => Object.Destroy(c.gameObject));
        
        public static void DestroyAllChildrenImmediate(this Transform transform) =>
            transform.ForEachChild(c => Object.DestroyImmediate(c.gameObject));
        
    }
}