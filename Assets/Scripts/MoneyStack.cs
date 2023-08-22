using System.Collections;
using UnityEngine;
using ZUtils;

namespace Moneybag
{
    public class MoneyStack : MonoBehaviour
    {
        private const float riseLength = .3f,
            floatLength = .1f,
            trackLength = .4f;

        private const float floatHeight = 3,
            scale = 1.5f;

        private bool animating = false;

        public void Animate(Vector3 startPosition, Transform target)
        {
            if (animating) return;
            
            animating = true;
            StartCoroutine(AnimateInternal(startPosition, target));
        } 

        private IEnumerator AnimateInternal(Vector3 startPosition, Transform target)
        {
            float startTime = Time.time;
            Vector3 floatPosition = startPosition + Vector3.up * floatHeight;

            while (RiseProgress() < 1)
            {
                AnimateRise(RiseProgress());
                yield return null;
            }

            yield return new WaitForSeconds(floatLength);

            while (TrackProgress() < 1)
            {
                AnimateTrack(TrackProgress());
                yield return null;
            }
            
            Destroy(gameObject);
            

            float RiseProgress() => (Time.time - startTime) / riseLength;
            // float FloatProgress() => (Time.time - riseLength - startTime) / floatLength;
            float TrackProgress() => (Time.time - floatLength - riseLength - startTime) / trackLength;

            void AnimateRise(float t)
            {
                transform.position =
                    Vector3.Lerp(startPosition, floatPosition, Easing.Ease(t, Easing.EasingType.OutQuad));
                transform.localScale = Mathf.Min(1, Easing.Ease(t * 2, Easing.EasingType.InSine)) * scale * Vector3.one;
            }

            void AnimateTrack(float t)
            {
                transform.position = Vector3.Lerp(floatPosition, target.position, Easing.Ease(t, Easing.EasingType.InSine));
                transform.localScale = Easing.Ease(Mathf.Clamp(8f * (1 - t),0, 1), Easing.EasingType.InSine) * scale * Vector3.one;
            }
        }
        
        
    }
}