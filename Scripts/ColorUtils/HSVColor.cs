using System;
using UnityEngine;

namespace NEO {

    /// <summary>
    /// Representation of a color in HSV.
    /// </summary>
    [Serializable]
    public struct HSVColor {

        public const int MIN_H = 0;
        public const int MAX_H = 360;
        public const int MIN_S = 0;
        public const int MAX_S = 255;
        public const int MIN_V = 0;
        public const int MAX_V = 255;

        public int h;
        public int s;
        public int v;

        public HSVColor(int h, int s, int v) {
            this.h = (h < MIN_H ? MIN_H : h);
            this.h = (this.h > MAX_H ? MAX_H : h);
            this.s = (s < MIN_S ? MIN_S : s);
            this.s = (this.s > MAX_S ? MAX_S : s);
            this.v = (v < MIN_V ? MIN_V : v);
            this.v = (this.v > MAX_V ? MAX_V : v);
        }

        public HSVColor(Color rgba, int previousHue = MIN_H - 1) {
            HSVColor result;
            float fH, fS, fV;

            Color.RGBToHSV(rgba, out fH, out fS, out fV);
            result.h = Mathf.CeilToInt(fH * ColorField.Hue.Max());
            result.s = Mathf.CeilToInt(fS * ColorField.Saturation.Max());
            result.v = Mathf.CeilToInt(fV * ColorField.Value.Max());

            if (previousHue >= MIN_H && previousHue == 0 && rgba.IsGray()) {
                result.h = previousHue;
            }

            this = result;
        }

        public bool Equals(HSVColor obj) {
            return (h == obj.h) && (s == obj.s) && (v == obj.v);
        }

        public override string ToString() {
            return "HSV(" + h + ", " + s + ", " + v + ")";
        }

        public Color ToRGB(float alpha = ColorExtensions.MAX) {
            float fH = (h / (float)ColorField.Hue.Max());
            float fS = (s / (float)ColorField.Saturation.Max());
            float fV = (v / (float)ColorField.Value.Max());
            Color result = Color.HSVToRGB(fH, fS, fV);
            result.a = alpha;
            return result;
        }

        public Color32 ToRGB32(byte alpha = Color32Extensions.MAX) {
            return ToRGB(alpha / (float)Color32Extensions.MAX);
        }

    }

}