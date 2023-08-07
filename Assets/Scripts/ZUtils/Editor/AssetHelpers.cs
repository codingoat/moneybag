using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ZUtils.Editor
{
    public static class AssetHelpers
    {
        /// Get the project-relative path of the folder the asset is in
        public static string GetPathToContainingFolder(Object asset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return assetPath.Substring(0, assetPath.LastIndexOf('/'));
        }

        public static string GetPathToFolderOfFirstElement<T>(this T[] prefabs) where T : MonoBehaviour =>
            GetPathToContainingFolder(prefabs.FirstOrDefault());

        public static DefaultAsset GetFolderOfFirstElement<T>(this T[] prefabs) where T : MonoBehaviour =>
            AssetDatabase.LoadAssetAtPath<DefaultAsset>(prefabs.GetPathToFolderOfFirstElement());

        public static T LoadAssetByGuid<T>(string guid) where T : UnityEngine.Object => 
            AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));

        public static T FindScriptableObject<T>(string folder) where T : ScriptableObject => 
            LoadAssetByGuid<T>(
                AssetDatabase.FindAssets($"t: {typeof(T).Name}", new[]{ folder }).First());
    }
}