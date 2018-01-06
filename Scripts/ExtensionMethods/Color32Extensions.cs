using UnityEngine;

public static class Color32Extensions {

    public const int MIN = byte.MinValue;
    public const int MAX = byte.MaxValue;

    /// <summary>
    /// Returns the color in "#RRGGBBAA" format.
    ///                       012345678
    /// </summary>
    public static string GetHexCode(this Color32 rgba, bool includeAlpha = true) {
        string result = "#" + ColorUtility.ToHtmlStringRGBA(rgba);
        if (!includeAlpha) result = result.Substring(0, result.Length - 2);
        return result;
    }

    /// <summary>
    /// Check if the color has no saturation (white, black and grays).
    /// </summary>
    public static bool IsGray(this Color32 rgba) {
        return (rgba.r == rgba.g) && (rgba.g == rgba.b);
    }

}