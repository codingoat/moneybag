using UnityEngine;

namespace ZUtils
{
    public static class MaterialUtils
    {
        private static MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();

        public static MaterialPropertyBlock GetPropertyBlock(this Renderer renderer)
        {
            materialPropertyBlock.Clear();
            renderer.GetPropertyBlock(materialPropertyBlock);
            return materialPropertyBlock;
        }
    }
}