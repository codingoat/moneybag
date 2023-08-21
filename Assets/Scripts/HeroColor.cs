using UnityEngine;

namespace Moneybag
{
    public class HeroColor : MonoBehaviour
    {
        [SerializeField] private Renderer[] tintedRenderers;

        public Color Color
        {
            get => tintedRenderers[0].material.color;
            set
            {
                foreach (Renderer tintedRenderer in tintedRenderers) 
                {
                    tintedRenderer.material.color = value; // TODO: this leaks memory!!!
                }
            }
        }
    }
}