using System.Linq;
using UnityEngine;

namespace ZUtils
{
    public static class AnimatorExtensions
    {
        public static float CurrentStateLength(this Animator anim) => anim.GetCurrentAnimatorStateInfo(0).length;
        public static float NextStateLength(this Animator anim) => anim.GetNextAnimatorStateInfo(0).length;
        public static float ClipLength(this Animator anim, string clipName) => anim.runtimeAnimatorController
            .animationClips.First(clip => clip.name == clipName).length;
    }
}