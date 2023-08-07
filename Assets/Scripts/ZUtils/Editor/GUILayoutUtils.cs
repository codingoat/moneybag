using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace ZUtils.Editor
{
    public static class GUILayoutUtils
    {
        public static void Header(string label)
        {
            EditorGUILayout.Space();
            GUILayout.Label(label, EditorStyles.boldLabel);
        }
        
        public static bool Button(Action function, bool enabled = true) =>
            Button(Regex.Replace(function.Method.Name, "(\\B[A-Z])", " $1"), function, enabled); // I am so sorry for using regex
        
        public static bool Button(Action function, UnityMode mode) =>
            Button(function, mode.IsTrue());

        public static bool Button(string label, Action function, UnityMode mode = UnityMode.Both) =>
            Button(label, function, mode.IsTrue());

        public static bool Button(string label, bool enabled) => Button(label, null, enabled);

        public static bool Button(string label, Action function, bool enabled)
        {
            bool guiEnabled = GUI.enabled;
            GUI.enabled = enabled;
            bool pressed = GUILayout.Button(label);
            GUI.enabled = guiEnabled;
            
            if (pressed && function != null) function();
            return pressed;
        }

        public static bool PrefabArray<T>(ref T[] prefabs, SerializedProperty arrayProperty, ref DefaultAsset folder, bool showArray = true) where T : MonoBehaviour
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{arrayProperty.displayName} folder");
            folder = (DefaultAsset)EditorGUILayout.ObjectField(folder, typeof(DefaultAsset), false);
            bool scanned = Button("Scan for prefabs", folder != null); 
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(arrayProperty);

            if (scanned)
            {
                Undo.RecordObject(arrayProperty.serializedObject.targetObject, "Scanned for prefabs");
                prefabs = EditorUtils.ScanPrefabs<T>(folder);
            }
            return scanned;
        }
    }
}