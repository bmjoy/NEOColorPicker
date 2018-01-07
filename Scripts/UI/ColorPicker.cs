using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NEO {

    public class ColorPicker : MonoBehaviour {

        #region Settings
        public Color32 initialColor = Color.red;

        public bool showAlphaSlider = false;

        [Space(8)]
        [Tooltip("This event is sent when user selects a different color in the "
            + "slides, or when the the color is changed through code.")]
        public ColorChangedEvent onColorChanged = new ColorChangedEvent();

        [Header("Components")]
        public Button toggleModeButton;
        public Text toggleModeText;

        [Space(4)]
        public Image currentColorImage;
        public InputField hexField;

        [Space(4)]
        public ColorPickerBoxSlider mainBoxSlider;
        public ColorPickerSlider mainSlider;

        [Space(4)]
        public Transform slidersContainer;
        public GameObject sliderPrefab;
        #endregion

        #region Sliders
        private List<ColorPickerSlider> allSliders;
        private ColorPickerSlider rSlider;
        private ColorPickerSlider gSlider;
        private ColorPickerSlider bSlider;
        private ColorPickerSlider aSlider;
        private ColorPickerSlider hSlider;
        private ColorPickerSlider sSlider;
        private ColorPickerSlider vSlider;
        #endregion

        private bool onRGBMode = false;

        private MultiColor multiColor = null;
        private Color? lastColor = null;

        /// <summary>
        /// The multi-mode representation of the currently
        /// selected color.
        /// </summary>
        public MultiColor MultiColor {
            get {
                return multiColor;
            }
        }

        /// <summary>
        /// The currently selected color.
        /// Changing it manually will always trigger onColorChanged.
        /// </summary>
        public Color CurrentColor {
            get {
                return MultiColor.RGBA32;
            }
            set {
                multiColor = new MultiColor(value);
                UpdateAll(forceEvent: true);
            }
        }

        protected ColorPicker() { }

        public void ToggleMode() {
            onRGBMode = !onRGBMode;

            if (onRGBMode) {
                toggleModeText.text = "RGB";
                rSlider.gameObject.SetActive(true);
                gSlider.gameObject.SetActive(true);
                bSlider.gameObject.SetActive(true);
                hSlider.gameObject.SetActive(false);
                sSlider.gameObject.SetActive(false);
                vSlider.gameObject.SetActive(false);
            } else {
                toggleModeText.text = "HSV";
                rSlider.gameObject.SetActive(false);
                gSlider.gameObject.SetActive(false);
                bSlider.gameObject.SetActive(false);
                hSlider.gameObject.SetActive(true);
                sSlider.gameObject.SetActive(true);
                vSlider.gameObject.SetActive(true);
            }
        }

        public void ReadHexCode(string hexCode) {
            Color newColor = new Color();
            if (ColorUtility.TryParseHtmlString(hexCode, out newColor)) {
                multiColor = new MultiColor(newColor);
                UpdateAll();
            }
        }

        private void Start() {
            multiColor = new MultiColor(initialColor);

            //Main sliders
            mainBoxSlider.Initialize(ColorField.Saturation, ColorField.Value);
            mainSlider.Initialize(ColorField.Hue);

            //Subsliders
            DeleteSliders();
            allSliders = new List<ColorPickerSlider>();
            rSlider = CreateSlider(ColorField.Red32);
            gSlider = CreateSlider(ColorField.Green32);
            bSlider = CreateSlider(ColorField.Blue32);
            hSlider = CreateSlider(ColorField.Hue);
            sSlider = CreateSlider(ColorField.Saturation);
            vSlider = CreateSlider(ColorField.Value);
            aSlider = CreateSlider(ColorField.Alpha32);

            aSlider.gameObject.SetActive(showAlphaSlider);
            if (!showAlphaSlider) {
                multiColor.SetField(ColorField.Alpha32, ColorField.Alpha32.Max(), recalculate: false);
            }

            //Bottom bar
            onRGBMode = !onRGBMode; ToggleMode();
            toggleModeButton.onClick.AddListener(ToggleMode);
            hexField.characterLimit = (showAlphaSlider ? "#RRGGBBAA".Length : "#RRGGBB".Length);
            hexField.onEndEdit.AddListener(ReadHexCode);

            UpdateAll();
        }

        private void OnDestroy() {
            toggleModeButton.onClick.RemoveListener(ToggleMode);
            hexField.onEndEdit.RemoveListener(ReadHexCode);
        }

        private void DeleteSliders() {
            int childCount = slidersContainer.childCount;
            for (int i = 0; i < childCount; i++) {
                Destroy(slidersContainer.GetChild(i).gameObject);
            }
        }

        private ColorPickerSlider CreateSlider(ColorField field) {
            GameObject newObj = Instantiate(sliderPrefab, slidersContainer);
            ColorPickerSlider newSlider = newObj.GetComponent<ColorPickerSlider>();
            newSlider.colorPicker = this;
            newSlider.Initialize(field);
            allSliders.Add(newSlider);
            return newSlider;
        }

        public void SetColorField(ColorField field, int value, bool recalculate = true) {
            MultiColor.SetField(field, value);
            if (recalculate) UpdateAll();
        }

        private void UpdateAll(bool forceEvent = false) {
            UpdateSliders();
            UpdateBottomBar();

            bool changedColor = (!lastColor.HasValue || MultiColor.RGBA32 != lastColor);
            if (forceEvent || changedColor) {
                onColorChanged.Invoke(MultiColor.RGBA32);
            }

            lastColor = MultiColor.RGBA32;
        }

        private void UpdateSliders() {
            mainBoxSlider.UpdateValues();
            mainSlider.UpdateValues();
            foreach (ColorPickerSlider slider in allSliders) slider.UpdateValues();
        }

        private void UpdateBottomBar() {
            currentColorImage.color = MultiColor.RGBA32;
            hexField.SetValue(MultiColor.RGBA32.GetHexCode(includeAlpha: showAlphaSlider),
                ignoreOnEndEdit: true, ignoreOnValueChanged: true);
        }

    }

}