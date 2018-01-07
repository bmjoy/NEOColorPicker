using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NEO {

    public class ColorPickerSlider : MonoBehaviour {

        public bool vertical = true;

        [Header("Components")]
        public Text label;
        public InputField inputField;
        public Slider slider;
        public RawImage sliderBackground;

        [HideInInspector]
        public ColorPicker colorPicker;

        private ColorField field;
        private bool hasInputField;
        private bool hasFixedTexture;

        public void Initialize(ColorField field) {
            this.field = field;

            //Setups the slider
            slider.minValue = field.Min();
            slider.maxValue = field.Max();
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(OnSliderChanged);

            //Generate textures that won't change
            switch (field) {
                case ColorField.Hue:
                    hasFixedTexture = true;
                    sliderBackground.texture = TextureGenerator.GenerateRainbowGradient(vertical: vertical);
                    break;
            }

            //Setups label and input field
            if (label != null) label.text = field.Label();

            hasInputField = (inputField != null);
            if (hasInputField) {
                inputField.characterLimit = slider.maxValue.ToString().Length;
                inputField.onEndEdit.AddListener(OnFieldChanged);
            }

            //Changes the value to the initial value
            UpdateValues();
        }

        /// <summary>
        /// Called when the inputField is changed by the user.
        /// </summary>
        private void OnFieldChanged(string fieldValue) {
            int intValue = Mathf.CeilToInt(float.Parse(fieldValue));
            colorPicker.SetColorField(field, intValue);
        }

        /// <summary>
        /// Called when the slider is changed by the user.
        /// </summary>
        public void OnSliderChanged(float sliderValue) {
            int intValue = Mathf.CeilToInt(sliderValue);
            colorPicker.SetColorField(field, intValue);
        }

        /// <summary>
        /// Sets the values of the slider and input fields without triggering the onChanged events.
        /// </summary>
        public void UpdateValues() {
            int value = colorPicker.MultiColor.GetField(field);
            slider.SetValue(value, ignoreOnValueChanged: true);
            if (hasInputField) inputField.SetValue(value.ToString(), ignoreOnValueChanged: true, ignoreOnEndEdit: true);

            if (!hasFixedTexture) {
                HSVColor currentHSV = colorPicker.MultiColor.HSV;
                HSVColor tempHSV = currentHSV;
                Color currentRGBA = colorPicker.MultiColor.RGBA32;
                Color32 rgba32A = currentRGBA;
                rgba32A.a = (byte)ColorField.Alpha32.Max();
                Color32 rgba32B = currentRGBA;
                rgba32B.a = (byte)ColorField.Alpha32.Max();
                switch (field) {
                    case ColorField.Red32:
                        rgba32A.r = (byte)ColorField.Red32.Min();
                        rgba32B.r = (byte)ColorField.Red32.Max();
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                    case ColorField.Green32:
                        rgba32A.g = (byte)ColorField.Green32.Min();
                        rgba32B.g = (byte)ColorField.Green32.Max();
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                    case ColorField.Blue32:
                        rgba32A.b = (byte)ColorField.Blue32.Min();
                        rgba32B.b = (byte)ColorField.Blue32.Max();
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                    case ColorField.Alpha32:
                        rgba32A.a = (byte)ColorField.Alpha32.Min();
                        rgba32B.a = (byte)ColorField.Alpha32.Max();
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                    case ColorField.Saturation:
                        tempHSV = currentHSV;
                        tempHSV.S = ColorField.Saturation.Min();
                        rgba32A = tempHSV.ToRGB(rgba32A.a);
                        tempHSV = currentHSV;
                        tempHSV.S = ColorField.Saturation.Max();
                        rgba32B = tempHSV.ToRGB(rgba32A.a);
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                    case ColorField.Value:
                        tempHSV = currentHSV;
                        tempHSV.V = ColorField.Value.Min();
                        rgba32A = tempHSV.ToRGB(rgba32A.a);
                        tempHSV = currentHSV;
                        tempHSV.V = ColorField.Value.Max();
                        rgba32B = tempHSV.ToRGB(rgba32A.a);
                        sliderBackground.texture = TextureGenerator.GenerateGradient(rgba32A, rgba32B, vertical: vertical);
                        break;
                }
            }
        }

        private void OnDestroy() {
            slider.onValueChanged.RemoveListener(OnSliderChanged);
            if (hasInputField) {
                inputField.onEndEdit.RemoveListener(OnFieldChanged);
            }
        }
    }
}
