using NEO.Utils;
using System;
using UnityEngine;

namespace NEO.NEOColorPicker {

    /// <summary>
    /// Represents the color of a color picker,
    /// simplifying handling of multiples models/fields.
    /// </summary>
    public class PickerColor {

        private Color rgb;

        public Color RGB {
            get {
                return GetRGB();
            }
            set {
                SetRGB(value);
            }
        }

        public HSVValues HSV {
            get {
                return GetHSV();
            }
            set {
                SetHSV(value);
            }
        }

        public HSLValues HSL {
            get {
                return GetHSL();
            }
            set {
                SetHSL(value);
            }
        }


        public PickerColor(Color rgb) {
            SetRGB(rgb);
        }


        private void SetRGB(Color rgb, bool checkValues = true) {
            if (checkValues) {
                //Avoids true internal blacks, whites and grays.
                //This prevents those colors from "locking" saturation and hue.
                //The change won't be enough to modify HTML/255/359 values.
                rgb.r = Mathf.Clamp(rgb.r, 0.0010f, 0.9990f);
                rgb.g = Mathf.Clamp(rgb.g, 0.0010f, 0.9990f);
                rgb.b = Mathf.Clamp(rgb.b, 0.0010f, 0.9990f);
                if (rgb.IsGray()) {
                    rgb.r += 0.0001f;
                }
            }

            this.rgb = rgb;
        }

        private void SetHSV(HSVValues hsv) {
            SetRGB(ColorConvert.HSVtoRGB(hsv));
        }

        private void SetHSL(HSLValues hsl) {
            SetRGB(ColorConvert.HSLtoRGB(hsl));
        }


        private Color GetRGB() {
            return rgb;
        }

        private HSVValues GetHSV() {
            return ColorConvert.RGBtoHSV(rgb);
        }

        private HSLValues GetHSL() {
            return ColorConvert.RGBtoHSL(rgb);
        }


        public void SetField(Field field, float value) {
            Color newRgb = rgb;
            //Avoids true internal blacks, whites and grays.
            //This prevents those colors from "locking" saturation and hue.
            //The change won't be enough to modify HTML/255/359 values.
            if (value < 0.0010f) value = 0.0010f;
            if (value > 0.9990f) value = 0.9990f;

            switch (field) {
                case Field.Red:
                    //This prevents internal grays through RGB. Check note above.
                    if (value.EqualsAll(RGB.g, RGB.b)) value += 0.0001f;
                    newRgb = ColorConvert.ChangeRGB(RGB, newR: value);
                    break;
                case Field.Green:
                    //This prevents internal grays through RGB. Check note above.
                    if (value.EqualsAll(RGB.r, RGB.b)) value += 0.0001f;
                    newRgb = ColorConvert.ChangeRGB(RGB, newG: value);
                    break;
                case Field.Blue:
                    //This prevents internal grays through RGB. Check note above.
                    if (value.EqualsAll(RGB.g, RGB.r)) value += 0.0001f;
                    newRgb = ColorConvert.ChangeRGB(RGB, newB: value);
                    break;
                case Field.Alpha:
                    newRgb = ColorConvert.ChangeRGB(RGB, newA: value);
                    break;
                case Field.Hue:
                    newRgb = ColorConvert.ChangeHSV(RGB, newH: value);
                    break;
                case Field.HSV_Saturation:
                    newRgb = ColorConvert.ChangeHSV(RGB, newS: value);
                    break;
                case Field.HSV_Value:
                    newRgb = ColorConvert.ChangeHSV(RGB, newV: value);
                    break;
                case Field.HSL_Saturation:
                    newRgb = ColorConvert.ChangeHSL(RGB, newS: value);
                    break;
                case Field.HSL_Lightness:
                    newRgb = ColorConvert.ChangeHSL(RGB, newL: value);
                    break;
            }

            SetRGB(newRgb, checkValues: false);
        }

        public float GetField(Field field) {
            switch (field) {
                case Field.Red: return RGB.r;
                case Field.Green: return RGB.g;
                case Field.Blue: return RGB.b;
                case Field.Alpha: return RGB.a;
                case Field.Hue: return HSV.h;
                case Field.HSV_Saturation: return HSV.s;
                case Field.HSV_Value: return HSV.v;
                case Field.HSL_Saturation: return HSL.s;
                case Field.HSL_Lightness: return HSL.l;
                default: throw new NotImplementedException();
            }
        }

    }

}
