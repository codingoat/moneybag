using UnityEngine;

namespace ZUtils
{
    public enum UnityMode
    {
        Both,
        EditMode,
        PlayMode,
        None
    };
    
    public static class UnityModeExtensions
    {
        public static bool IsTrue(this UnityMode mode) => mode == UnityMode.Both || 
                                                          mode == UnityMode.EditMode && !Application.isPlaying ||
                                                          mode == UnityMode.PlayMode && Application.isPlaying;
    }
}

