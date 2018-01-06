using System;
using UnityEngine;

namespace NEO {

    /// <summary>
    /// Representation of a color in multiple modes.
    /// Please notice that this isn't a struct!
    /// </summary>
    [Serializable]
    public class MultiColor {
        
        [Serializable]
        public enum Mode {
            RGBA32, HSV
        }

        public Color32 RGBA32 = Color.red;

        public HSVColor HSV = new HSVColor(Color.red);

        /// <summary>
        /// Determines which mode's data will be used to calculate the other modes's values.
        /// </summary>
        private Mode currentMode;

        protected MultiColor() { }

        public MultiColor(Color32 rgba32) {
            RGBA32 = rgba32;
            currentMode = Mode.RGBA32;
            Recalculate();
        }
        
        ///<summary>Recalculate the values of every mode
        ///based on the current mode's values.</summary>
        public void Recalculate() {
            switch (currentMode) {
                case Mode.HSV:
                    RGBA32 = HSV.ToRGB32(RGBA32.a);
                    break;
                case Mode.RGBA32:
                    HSV = new HSVColor(RGBA32, previousHue: RGBA32.a);
                    break;
            }
        }


        /// <summary>
        /// Sets the value of a field, recalculating the
        /// all other values as needed. 
        /// </summary>
        /// <param name="recalculate">If false, won't recalculate the other values.</param>
        public void SetField(ColorField field, int value, bool recalculate = true) {
            value = (value < field.Min() ? field.Min() : value);
            value = (value > field.Max() ? field.Max() : value);

            switch (field) {
                case ColorField.Red32:
                    RGBA32.r = (byte)value; currentMode = Mode.RGBA32; if (recalculate) Recalculate();
                    break;
                case ColorField.Green32:
                    RGBA32.g = (byte)value; currentMode = Mode.RGBA32; if (recalculate) Recalculate();
                    break;
                case ColorField.Blue32:
                    RGBA32.b = (byte)value; currentMode = Mode.RGBA32; if (recalculate) Recalculate();
                    break;
                case ColorField.Alpha32:
                    RGBA32.a = (byte)value; currentMode = Mode.RGBA32; if (recalculate) Recalculate();
                    break;
                case ColorField.Hue:
                    HSV.h = value; currentMode = Mode.HSV; if (recalculate) Recalculate();
                    break;
                case ColorField.Saturation:
                    HSV.s = value; currentMode = Mode.HSV; if (recalculate) Recalculate();
                    break;
                case ColorField.Value:
                    HSV.v = value; currentMode = Mode.HSV; if (recalculate) Recalculate();
                    break;
            }
        }

        public int GetField(ColorField field) {
            switch (field) {
                case ColorField.Red32:
                    return RGBA32.r;
                case ColorField.Green32:
                    return RGBA32.g;
                case ColorField.Blue32:
                    return RGBA32.b;
                case ColorField.Alpha32:
                    return RGBA32.a;
                case ColorField.Hue:
                    return HSV.h;
                case ColorField.Saturation:
                    return HSV.s;
                case ColorField.Value:
                    return HSV.v; ;
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
