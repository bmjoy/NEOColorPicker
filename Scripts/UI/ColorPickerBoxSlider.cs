using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NEO {

    public class ColorPickerBoxSlider : MonoBehaviour {

        //TODO: Support multiple fields in both axis, kinda like Photoshop.
    
        [Header("Components")]
        public ColorPicker colorPicker;
        public BoxSlider boxSlider;
        public RawImage sliderBackground;

        private ColorField fieldX;
        private ColorField fieldY;
        
        public void Initialize(ColorField fieldX, ColorField fieldY) {
            this.fieldX = fieldX;
            this.fieldY = fieldY;

            //Setups the slider
            boxSlider = GetComponentInChildren<BoxSlider>();
            boxSlider.minValue = fieldX.Min(); //BoxSlider currently only supports the same min and
            boxSlider.maxValue = fieldX.Max(); //max values for both axes
            boxSlider.wholeNumbers = true;
            boxSlider.onValueChanged.AddListener(OnSliderChanged);

            //Changes the value to the initial value
            UpdateValues();
        }

        /// <summary>
        /// Called when the slider is changed by the user.
        /// </summary>
        public void OnSliderChanged(float valueX, float valueY) {
            int intX = Mathf.CeilToInt(valueX);
            int intY = Mathf.CeilToInt(valueY);
            colorPicker.SetColorField(fieldX, intX);
            colorPicker.SetColorField(fieldY, intY);
        }

        /// <summary>
        /// Sets the values of the slider and input fields without triggering the onChanged events.
        /// </summary>
        public void UpdateValues() {
            int valueX = colorPicker.MultiColor.GetField(fieldX);
            int valueY = colorPicker.MultiColor.GetField(fieldY);

            boxSlider.SetValue(valueX, valueY, ignoreOnValueChanged: true);

            //Currently only supports Saturation x Value
            if (fieldX == ColorField.Saturation && fieldY == ColorField.Value) {
                sliderBackground.texture = TextureGenerator.GenerateHSVBox(colorPicker.MultiColor.HSV.h);
            }
        }

        private void OnDestroy() {
            boxSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }

    }
}
