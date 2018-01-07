using UnityEngine;

namespace NEO {
    public static class ColorExtensions {

        public const float MIN = 0f;
        public const float MAX = 1f;

        /// <summary>
        /// Returns the color in "#RRGGBBAA" format.
        /// </summary>
        public static string GetHexCode(this Color rgba) {
            return "#" + ColorUtility.ToHtmlStringRGBA(rgba);
        }

        /// <summary>
        /// Check if the color has no saturation (white, black and grays).
        /// </summary>
        public static bool IsGray(this Color rgba) {
            return (rgba.r == rgba.g) && (rgba.g == rgba.b);
        }
        
    }
}