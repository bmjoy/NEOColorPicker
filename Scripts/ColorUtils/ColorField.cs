using System;

namespace NEO {

    [Serializable]
    public enum ColorField {
        Red32, Green32, Blue32, Alpha32, Hue, Saturation, Value
    }

    public static class ColorFieldMethods {

        public static int Min(this ColorField field) {
            switch (field) {
                case ColorField.Red32:
                case ColorField.Green32: 
                case ColorField.Blue32:
                case ColorField.Alpha32:
                    return Color32Extensions.MIN;
                case ColorField.Hue:
                    return HSVColor.MIN_H;
                case ColorField.Saturation:
                    return HSVColor.MIN_S;
                case ColorField.Value:
                    return HSVColor.MIN_V;
                default:
                    throw new NotSupportedException();
            }
        }

        public static int Max(this ColorField field) {
            switch (field) {
                case ColorField.Red32:
                case ColorField.Green32:
                case ColorField.Blue32:
                case ColorField.Alpha32:
                    return Color32Extensions.MAX;
                case ColorField.Hue:
                    return HSVColor.MAX_H;
                case ColorField.Saturation:
                    return HSVColor.MAX_S;
                case ColorField.Value:
                    return HSVColor.MAX_V;
                default:
                    throw new NotSupportedException();
            }
        }

        public static string Label(this ColorField field) {
            switch (field) {
                case ColorField.Red32:
                    return "R";
                case ColorField.Green32:
                    return "G";
                case ColorField.Blue32:
                    return "B";
                case ColorField.Alpha32:
                    return "A";
                case ColorField.Hue:
                    return "H";
                case ColorField.Saturation:
                    return "S";
                case ColorField.Value:
                    return "V";
                default:
                    throw new NotSupportedException();
            }
        }
    }

}
