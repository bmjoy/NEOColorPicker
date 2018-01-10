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
        private InputField inputField;
        [SerializeField]
        private RawImage sliderBackground;

        private ColorPicker colorPicker;
        private Field field;
        private bool useFixedTexture;

        public void Initialize(Field field, ColorPicker colorPicker) {
            this.field = field;
            this.colorPicker = colorPicker;

            //Changes the value to the initial value
            Refresh(ignoreEvents: false);

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

            //Setups others
            if (label != null) label.text = field.Label();
            if (inputField != null) {
                inputField.text = slider.value.ToString();
                inputField.onEndEdit.AddListener(OnInputChanged);
            }
        }

        private void OnInputChanged(string input) {
            float floatInput;
            if (!float.TryParse(input, out floatInput)) {
                inputField.text = slider.value.ToString();
            } else {
                OnSliderChanged(floatInput);
            }
        }

        private void OnSliderChanged(float input) {
            float value = field.InputToValue(input);
            colorPicker.SetColorField(field, value);
        }

        /// <summary>
        /// Update the values based on the color picker's current color.
        /// </summary>
        public void Refresh(bool ignoreEvents = false) {
            float value = colorPicker.GetColorField(field);
            float input = field.ValueToInput(value);
            slider.SetValue(input, ignoreOnValueChanged: true);
            if (inputField != null) inputField.text = slider.value.ToString();
            if (!useFixedTexture) RegenerateTexture();
        }

        private void RegenerateTexture() {
            sliderBackground.texture = TextureGenerator.GenerateFieldGradient(colorPicker.ColorRGB, field, vertical: vertical);
        }

        private void OnDestroy() {
            slider.onValueChanged.RemoveListener(OnSliderChanged);
            if (inputField != null) inputField.onEndEdit.RemoveListener(OnInputChanged);
        }
    }
}
