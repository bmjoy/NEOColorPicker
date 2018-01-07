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

        private int h;

        /// <summary>
        /// Hue value. Will get clamped between the allowed values MIN_H and MAX_H.
        /// </summary>
        public int H {
            get {
                return h;
            }
            set {
                h = value;
                h = (h < MIN_H ? MIN_H : h);
                h = (h > MAX_H ? MAX_H : h);
            }
        }

        private int s;

        /// <summary>
        /// Saturation value. Will get clamped between the allowed values MIN_S and MAX_S.
        /// </summary>
        public int S {
            get {
                return s;
            }
            set {
                s = value;
                s = (s < MIN_S ? MIN_S : s);
                s = (s > MAX_S ? MAX_S : s);
            }
        }

        private int v;

        /// <summary>
        /// Value. Will get clamped between the allowed values MIN_V and MAX_V.
        /// </summary>
        public int V {
            get {
                return v;
            }
            set {
                v = value;
                v = (v < MIN_V ? MIN_V : v);
                v = (v > MAX_V ? MAX_V : v);
            }
        }

        public HSVColor(int h, int s, int v) {
            this.h = h;
            this.s = s;
            this.v = v;
            H = h;
            S = s;
            V = v;
        }

        public HSVColor(Color rgba, int previousHue = MIN_H - 1) {
            HSVColor result = new HSVColor();
            float fH, fS, fV;

            Color.RGBToHSV(rgba, out fH, out fS, out fV);
            result.H = Mathf.CeilToInt(fH * ColorField.Hue.Max());
            result.S = Mathf.CeilToInt(fS * ColorField.Saturation.Max());
            result.V = Mathf.CeilToInt(fV * ColorField.Value.Max());

            if (previousHue >= MIN_H && previousHue == 0 && rgba.IsGray()) {
                result.H = previousHue;
            }

            this = result;
        }

        public bool Equals(HSVColor obj) {
            return (H == obj.H) && (S == obj.S) && (V == obj.V);
        }

        public override string ToString() {
            return "HSV(" + H + ", " + S + ", " + V + ")";
        }

        public Color ToRGB(float alpha = ColorExtensions.MAX) {
            float fH = (H / (float)ColorField.Hue.Max());
            float fS = (S / (float)ColorField.Saturation.Max());
            float fV = (V / (float)ColorField.Value.Max());
            Color result = Color.HSVToRGB(fH, fS, fV);
            result.a = alpha;
            return result;
        }

        public Color32 ToRGB32(byte alpha = Color32Extensions.MAX) {
            return ToRGB(alpha / (float)Color32Extensions.MAX);
        }
        
    }

}