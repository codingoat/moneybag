using UnityEngine;

namespace ZUtils
{
    public enum UnityContext {Both, Editor, Build, None}
    
    public static class UnityContextExtensions
    {
        public static bool IsTrue(this UnityContext requiredCtx)
        {
            return requiredCtx == UnityContext.Both ||
                   requiredCtx == UnityContext.Editor && Application.isEditor ||
                   requiredCtx == UnityContext.Build && !Application.isEditor;
        }
    }
}