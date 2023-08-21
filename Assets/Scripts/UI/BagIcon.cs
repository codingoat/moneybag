using UnityEngine;
using UnityEngine.UI;

namespace Moneybag.UI
{
    public class BagIcon : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private Color incompleteColor, completeColor;

        public float Progress
        {
            get => fill.fillAmount;
            set
            {
                fill.fillAmount = value;
                fill.color = value < 1 ? incompleteColor : completeColor;
            }
        }
    }
}