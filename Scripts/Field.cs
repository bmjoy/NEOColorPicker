using NEO.Utils;
using System;
using UnityEngine;

namespace NEO.NEOColorPicker {

    [Serializable]
    public enum Field {
        Red, Green, Blue, Alpha,
        Hue,
        HSV_Saturation, HSV_Value,
        HSL_Saturation, HSL_Lightness
    }

    public static class FieldMethods {

        public static string Label(this Field field) {
            switch (field) {
                case Field.Red:
                    return "R";
                case Field.Green:
                    return "G";
                case Field.Blue:
                    return "B";
                case Field.Alpha:
                    return "A";
                case Field.Hue:
                    return "H";
                case Field.HSV_Saturation:
                case Field.HSL_Saturation:
                    return "S";
                case Field.HSV_Value:
                    return "V";
                case Field.HSL_Lightness:
                    return "L";
                default:
                    throw new NotSupportedException();
            }
        }


        public static int InputMin(this Field field) {
            return 0;
        }

        public static int InputMax(this Field field) {
            switch (field) {
                case Field.Hue:
                    return 359;
                case Field.HSL_Lightness:
                case Field.HSL_Saturation:
                case Field.HSV_Value:
                case Field.HSV_Saturation:
                    return 127;
                default:
                    return 255;
            }
        }

        /// <summary>
        /// Converts an input (Min-Max) to a proper 0-1f value.
        /// </summary>
        /// <returns></returns>
        public static float InputToValue(this Field field, float input) {
            float f = input.OnNewRange(field.InputMin(), field.InputMax(), 0f, 1f);
            f = Mathf.Clamp01(f);
            return f;
        }

        /// <summary>
        /// Converts a 0-1f value to an input (Min-Max).
        /// </summary>
        public static int ValueToInput(this Field field, float value) {
            value = Mathf.Clamp01(value);
            float f = (value.OnNewRange(0f, 1f, field.InputMin(), field.InputMax()));
            int i = Mathf.RoundToInt(f);
            return i;
        }
    }

}
