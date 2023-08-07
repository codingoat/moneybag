using UnityEngine.Events;
using UnityEngine.UI;

namespace ZUtils
{
    public static class UIExtensions
    {
        public static void Bind(this Toggle toggle, UnityAction<bool> setter, bool? defaultValue = null)
        {
            toggle.isOn = defaultValue.GetValueOrDefault(toggle.isOn);
            toggle.onValueChanged.AddListener(setter);
        }

        public static void Bind(this Slider slider, UnityAction<float> setter, float? defaultValue = null)
        {
            slider.value = defaultValue.GetValueOrDefault(slider.value); 
            slider.onValueChanged.AddListener(setter);
        }
    }
}