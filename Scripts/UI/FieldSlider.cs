using NEO.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace NEO.NEOColorPicker {

    public class FieldSlider : MonoBehaviour {

        public bool vertical = true;

        [Header("UI Components")]
        [SerializeField]
        private Text label;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private RawImage sliderBackground;

        private ColorPicker colorPicker;
        private Field field;
        private bool useFixedTexture;

        public void Initialize(Field field, ColorPicker colorPicker) {
            this.field = field;
            this.colorPicker = colorPicker;

            //Setups the slider
            slider.minValue = field.InputMin();
            slider.maxValue = field.InputMax();
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(OnSliderChanged);

            //Generate textures that won't change
            useFixedTexture = true;
            switch (field) {
                case Field.Hue:
                    sliderBackground.texture = TextureGenerator.GenerateRainbowGradient(vertical: vertical);
                    break;
                default:
                    useFixedTexture = false;
                    break;
            }

            //Setups label
            if (label != null) label.text = field.Label();

            //Changes the value to the initial value
            Refresh();
        }
               
        private void OnSliderChanged(float input) {
            float value = field.InputToValue(input);
            colorPicker.SetColorField(field, value);
        }

        /// <summary>
        /// Update the values based on the color picker's current color.
        /// </summary>
        public void Refresh() {
            float value = colorPicker.GetColorField(field);
            float input = field.ValueToInput(value);
            slider.SetValue(input, ignoreOnValueChanged: true);
            if (!useFixedTexture) RegenerateTexture();
        }

        private void RegenerateTexture() {
            sliderBackground.texture = TextureGenerator.GenerateFieldGradient(colorPicker.ColorRGB, field, vertical: vertical);
        }

        private void OnDestroy() {
            slider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }
}
