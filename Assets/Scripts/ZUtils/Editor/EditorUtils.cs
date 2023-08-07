using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ZUtils.Editor
{
    public static class EditorUtils
    {
        /// <summary>Finds and assigns all prefabs with the same component in the folder</summary>
        public static T[] ScanPrefabs<T>(DefaultAsset folder) where T : MonoBehaviour => 
            AssetDatabase.FindAssets($"t: GameObject", new[] { AssetDatabase.GetAssetPath(folder) }) // find the gameobjects... (NOTE: "t: {typeof(T).Name}" does not work!)
                .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)) // load the prefabs...
                    .GetComponent<T>()) // get the component...
                .Where(component => component != null) // that have the required component...
                .ToArray(); // and assign the variable our new array
    }
}