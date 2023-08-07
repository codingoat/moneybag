using UnityEditor;
using UnityEngine;

namespace ZUtils.Editor
{
    public static class GUIUtils
    {
        public const float indentWidth = 15;

        public static float NextGUILine(this ref Rect rect) // this ref because Rect is a struct
        {
            rect.y = rect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            return rect.y;
        }

        public static void AddVerticalSpacing(this ref Rect rect, float units = 1) =>
            rect.y += EditorGUIUtility.standardVerticalSpacing * units;

        /// Splits the rect into columns.
        /// <returns>The 0-indexed <paramref name="column"/> of <paramref name="columnCount"/> sub-rect.</returns>
        public static Rect Column(this Rect rect, int column, int columnCount, int columnSpan = 1)
        {
            Rect subrect = rect;
            subrect.width *= (float)columnSpan / columnCount;
            subrect.x += rect.width / columnCount * column;
            return subrect;
        }
    }
}