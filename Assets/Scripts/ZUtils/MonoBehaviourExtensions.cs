using System;
using System.Collections;
using UnityEngine;

namespace ZUtils
{
    public static class MonoBehaviourExtensions
    {
        public static void Delay(this MonoBehaviour mb, float seconds, Action function) => 
            mb.StartCoroutine(DelayInternal(seconds, function));

        private static IEnumerator DelayInternal(float seconds, Action function)
        {
            yield return new WaitForSeconds(seconds);
            function();
        }

        public static void DelayFrame(this MonoBehaviour mb, Action function) => DelayFrames(mb, 1, function);
        public static void DelayFrames(this MonoBehaviour mb, int frames, Action function) => 
            mb.StartCoroutine(DelayFramesInternal(frames, function));

        private static IEnumerator DelayFramesInternal(int frames, Action function)
        {
            for (int i = 0; i < frames; i++) yield return null;
            function();
        }
    }
}