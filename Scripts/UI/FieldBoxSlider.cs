using NEO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace NEO.NEOColorPicker {

    public class FieldBoxSlider : MonoBehaviour {

        //TODO: Support multiple fields in both axis, kinda like Photoshop.
    
        [Header("Components")]
        [SerializeField]
        private BoxSlider boxSlider;
        [SerializeField]
        private RawImage sliderBackground;

        private ColorPicker colorPicker;
        private Field fieldX;
        private Field fieldY;
        
        public void Initialize(Field fieldX, Field fieldY, ColorPicker colorPicker) {
            this.fieldX = fieldX;
            this.fieldY = fieldY;
            this.colorPicker = colorPicker;

            //Setups the slider
            boxSlider.minValue = 0f; //BoxSlider currently only supports the same min and
            boxSlider.maxValue = 1f; //max values for both axes
            boxSlider.wholeNumbers = false;
            boxSlider.onValueChanged.AddListener(OnSliderChanged);

            //Changes the value to the initial value
            Refresh();
        }
        
        private void OnSliderChanged(float valueX, float valueY) {
            float value01X = Mathf.Clamp01(valueX);
            float value01Y = Mathf.Clamp01(valueY);
            colorPicker.SetColorField(fieldX, value01X);
            colorPicker.SetColorField(fieldY, value01Y);
        }

        /// <summary>
        /// Updates the current values based on the color picker's current color.
        /// </summary>
        public void Refresh() {
            float valueX = colorPicker.GetColorField(fieldX);
            float valueY = colorPicker.GetColorField(fieldY);
            boxSlider.SetValue(valueX, valueY, ignoreOnValueChanged: true);
            RegenerateTexture();
        }

        private void RegenerateTexture() {
            Color currentColor = colorPicker.ColorRGB;

            //Currently only supports Saturation x Value
            if (fieldX == Field.HSV_Saturation && fieldY == Field.HSV_Value) {
                HSLValues hsla = ColorConvert.RGBtoHSL(currentColor);
                sliderBackground.texture = TextureGenerator.GenerateHSVBox(hsla.h);
            }
        }

        private void OnDestroy() {
            boxSlider.onValueChanged.RemoveListener(OnSliderChanged);
        }

    }
}
