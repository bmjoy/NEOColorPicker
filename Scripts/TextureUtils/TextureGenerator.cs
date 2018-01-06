using UnityEngine;

namespace NEO {

    /// <summary>
    /// Simple tool to generate basic textures like gradients during runtime.
    /// </summary>
    public class TextureGenerator {

        /// <summary>
        /// Generates a texture consisting of a gradient between two colors.
        /// </summary>
        /// <param name="colorA">First color.</param>
        /// <param name="colorB">Second color.</param>
        /// <param name="resolution">Number of pixels for the gradient longest' side.
        /// Small values like 8 are enough for most gradients with bilinear filtering.
        /// The smallest side will always be 1 pixel long.</param>
        /// <param name="vertical">Will the texture be generated vertically?</param>
        public static Texture GenerateGradient(Color colorA, Color colorB, int resolution = 8, bool vertical = true) {
            int width = vertical ? 1 : resolution;
            int height = vertical ? resolution : 1;

            Texture2D texture = new Texture2D(width, height);
            texture.hideFlags = HideFlags.DontSave;
            texture.wrapMode = TextureWrapMode.Clamp;

            //TODO: Use SetPixels instead of SetPixel, for better performance.
            for (int i = 0; i < resolution; i++) {
                float pos = i / (float)(resolution - 1);
                Color color = Color.Lerp(colorA, colorB, pos);
                if (vertical) {
                    texture.SetPixel(0, i, color);
                } else {
                    texture.SetPixel(i, 0, color);
                }
            }

            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Generates a texture consisting of a gradient between all hues (a rainbow) in the HSV
        /// color system, with maximum saturation and value.
        /// </summary>
        /// <param name="resolution">Number of pixels for the gradient longest' side.
        /// Small values like 16 are usually precise enough with bilinear filtering.
        /// The smallest side will always be 1 pixel long.</param>
        /// <param name="vertical">Will the texture be generated vertically?</param>
        /// <returns></returns>
        public static Texture GenerateRainbowGradient(int resolution = 16, bool vertical = true) {
            int width = vertical ? 1 : resolution;
            int height = vertical ? resolution : 1;

            Texture2D texture = new Texture2D(width, height);
            texture.hideFlags = HideFlags.DontSave;
            texture.wrapMode = TextureWrapMode.Clamp;

            //TODO: Use SetPixels instead of SetPixel, for better performance.
            for (int i = 0; i < resolution; i++) {
                int hue = Mathf.CeilToInt((i / (float)resolution) * HSVColor.MAX_H);
                Color color = new HSVColor(hue, HSVColor.MAX_S, HSVColor.MAX_V).ToRGB();
                if (vertical) {
                    texture.SetPixel(0, i, color);
                } else {
                    texture.SetPixel(i, 0, color);
                }
            }
            
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Generates a two-dimensional gradient for visualizing all color possibilities
        /// in a specific HSV hue. Similar to those used in color pickers.
        /// </summary>
        /// <param name="hue">The hue for the colors.</param>
        /// <param name="resolution">Number of pixels for the gradient sides.
        /// Small values like 8 are usually precise enough with bilinear filtering.
        /// Be careful, this increases the complexity exponentially.</param>
        /// <returns></returns>
        public static Texture2D GenerateHSVBox(int hue = 0, int resolution = 8) {
            Texture2D texture = new Texture2D(resolution, resolution);
            texture.hideFlags = HideFlags.DontSave;
            texture.wrapMode = TextureWrapMode.Clamp;

            //TODO: Use SetPixels instead of SetPixel, for better performance.
            for (int x = 0; x < resolution; x++) {
                for (int y = 0; y < resolution; y++) {
                    int saturation = Mathf.CeilToInt(HSVColor.MAX_S * x / (float)(resolution - 1));
                    int value = Mathf.CeilToInt(HSVColor.MAX_V * y / (float)(resolution - 1));
                    texture.SetPixel(x, y, new HSVColor(hue, saturation, value).ToRGB());
                }
            }

            texture.Apply();
            return texture;
        }
    }

}