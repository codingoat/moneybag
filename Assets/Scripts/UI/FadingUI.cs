using UnityEngine;
using ZUtils;

namespace Moneybag.UI
{
    /// A UI that fades to show and hide itself via a CanvasGroup
    [RequireComponent(typeof(CanvasGroup))]
    public class FadingUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private CanvasGroup CanvasGroup => canvasGroup ? canvasGroup : canvasGroup = GetComponent<CanvasGroup>();
        
        public bool visible = true;

        private const float AnimLength = .3f;

#region Interface

        public void Show() => visible = true;
        public void Hide() => visible = false;


        public void Fade(bool visible) => Fade(visible, true);
        public void FadeInstant(bool visible) => Fade(visible, false);
        
        public void Fade(bool visible, bool animate)
        {
            this.visible = visible;
            if(!animate) CanvasGroup.alpha = visible ? 1 : 0;
        }


#endregion

#region Lifecycle

        private void OnValidate()
        {
            if (!Application.isPlaying) 
                this.DelayFrame(() => { CanvasGroup.alpha = visible ? 1 : 0; });
        }

        private void Update()
        {
            if ((visible && CanvasGroup.alpha == 1) || (!visible && CanvasGroup.alpha == 0))
                return; // no need to animate

            float deltaProgress = Time.deltaTime / AnimLength,
                fadeDirection = visible ? 1 : -1;
            CanvasGroup.alpha =
                Mathf.Clamp01(CanvasGroup.alpha + fadeDirection * deltaProgress); // linear, maybe easing support later?
        }

#endregion
    }
}