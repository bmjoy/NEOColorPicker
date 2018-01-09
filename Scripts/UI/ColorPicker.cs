using NEO.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NEO.NEOColorPicker {

    public class ColorPicker : MonoBehaviour {

        #region Settings
        [SerializeField]
        private Color initialColor = Color.red;
        [SerializeField]
        private Model initialModel = Model.RGB;
        [SerializeField]
        private bool useAlphaSlider = false;

        [Header("UI")]
        [SerializeField]
        private GameObject prefabSlider;
        [SerializeField]
        private Transform slidersParent;
        [SerializeField]
        private Button buttonAdvanceModel;
        [SerializeField]
        private Text textCurrentModel;
        [SerializeField]
        private Image imageCurrentColor;
        [SerializeField]
        private InputField fieldHtmlCode;
        [SerializeField]
        private FieldBoxSlider mainBoxSlider;
        [SerializeField]
        private FieldSlider mainSlider;

        /// <summary>
        /// Called whenever the current color is modified. Either manually (through code)
        /// or through the sliders.
        /// </summary>
        [Header("Events")]
        public ColorChangedEvent onColorChanged = new ColorChangedEvent();
        #endregion

        private List<FieldSlider> sliders;
        private FieldSlider redSlider;
        private FieldSlider greenSlider;
        private FieldSlider blueSlider;
        private FieldSlider alphaSlider;
        private FieldSlider hueSlider;
        private FieldSlider hsvSatSlider;
        private FieldSlider hsvValueSlider;
        private FieldSlider hslSatSlider;
        private FieldSlider hslLightSlider;

        private Model currentModel = Model.RGB;
        private PickerColor currentColor;
        private Color lastColor;

        #region Properties
        /// <summary>
        /// The current color model (RGB, HSV etc.).
        /// This will determine the sliders being shown.
        /// </summary>
        public Model CurrentModel {
            get {
                return currentModel;
            }
            set {
                currentModel = value;
                SetEnabledSliders();
            }
        }

        /// <summary>
        /// The RGB values of the current color.
        /// </summary>
        public Color ColorRGB {
            get {
                return currentColor.RGB;
            }
            set {
                currentColor.RGB = value;
                Refresh(forceEvent: true);
            }
        }

        /// <summary>
        /// The HSV values of the current color.
        /// </summary>
        public HSVValues ColorHSV {
            get {
                return currentColor.HSV;
            }
            set {
                currentColor.HSV = value;
                Refresh(forceEvent: true);
            }
        }

        /// <summary>
        /// The HSL values of the current color.
        /// </summary>
        public HSLValues ColorHSL {
            get {
                return currentColor.HSL;
            }
            set {
                currentColor.HSL = value;
                Refresh(forceEvent: true);
            }
        }
        #endregion

        protected ColorPicker() { }

        #region Main
        private void Start() {
            currentColor = new PickerColor(initialColor);
            InitializeSliders();
            currentModel = initialModel;
            InitializeBottomBar();

            SetEnabledSliders();
            Refresh(forceEvent: true);
        }

        private void OnDestroy() {
            buttonAdvanceModel.onClick.RemoveListener(AdvanceModel);
            fieldHtmlCode.onEndEdit.RemoveListener(SetHTMLCode);
        }

        private void Refresh(bool forceEvent = false) {
            RefreshSliders();
            RefreshBottomBar();

            bool changedColor = (ColorRGB != lastColor);
            if (changedColor) {
                lastColor = ColorRGB;
            }
            if (forceEvent || changedColor) {
                onColorChanged.Invoke(ColorRGB);
            }
        }

        /// <summary>
        /// Changes the value of a field (Red, Alpha, Hue etc.) from the current color.
        /// </summary>
        /// <param name="value">The value of the field (0f to 1f).</param>
        public void SetColorField(Field field, float value) {
            currentColor.SetField(field, value);
            Refresh();
        }

        /// <summary>
        /// Gets a color field (Red, Alpha, Hue etc.) from the current color.
        /// </summary>
        /// <returns>The value of the field (0f to 1f).</returns>
        public float GetColorField(Field field) {
            return currentColor.GetField(field);
        }
        #endregion

        #region Sliders
        private void InitializeSliders() {
            mainBoxSlider.Initialize(Field.HSV_Saturation, Field.HSV_Value, this);
            mainSlider.Initialize(Field.Hue, this);

            DeleteSliders();
            redSlider = CreateSlider(Field.Red);
            greenSlider = CreateSlider(Field.Green);
            blueSlider = CreateSlider(Field.Blue);
            hueSlider = CreateSlider(Field.Hue);
            hsvSatSlider = CreateSlider(Field.HSV_Saturation);
            hsvValueSlider = CreateSlider(Field.HSV_Value);
            hslSatSlider = CreateSlider(Field.HSL_Saturation);
            hslLightSlider = CreateSlider(Field.HSL_Lightness);
            alphaSlider = CreateSlider(Field.Alpha);

            alphaSlider.gameObject.SetActive(alphaSlider);
            if (!alphaSlider) {
                Color newColor = currentColor.RGB;
                newColor.a = 1f;
                currentColor.RGB = newColor;
            }
        }

        private void DeleteSliders() {
            int childCount = slidersParent.childCount;
            for (int i = 0; i < childCount; i++) {
                Destroy(slidersParent.GetChild(i).gameObject);
            }
            sliders = new List<FieldSlider>();
        }

        private FieldSlider CreateSlider(Field field) {
            GameObject newObj = Instantiate(prefabSlider, slidersParent);
            FieldSlider newSlider = newObj.GetComponent<FieldSlider>();
            newSlider.Initialize(field, this);
            sliders.Add(newSlider);
            return newSlider;
        }

        private void RefreshSliders() {
            mainBoxSlider.Refresh();
            mainSlider.Refresh();
            foreach (FieldSlider slider in sliders) slider.Refresh();
        }
        #endregion

        #region Bottom Bar
        /// <summary>
        /// Sets the current color based on a HTML-style hexadecimal code.
        /// </summary>
        public void SetHTMLCode(string htmlCode) {
            ColorRGB = ColorConvert.HTMLtoRGB(htmlCode);
        }

        /// <summary>
        /// Gets the HTML-style hexadecimal representation of the current color.
        /// </summary>
        public string GetHTMLCode() {
            return ColorConvert.RGBtoHTML(ColorRGB, includeAlpha: alphaSlider);
        }

        /// <summary>
        /// Advances the current color model (RGB, HSV etc.) to the next model possible.
        /// </summary>
        public void AdvanceModel() {
            CurrentModel = CurrentModel.Next();
        }

        private void SetEnabledSliders() {
            switch (currentModel) {
                case Model.RGB:
                    textCurrentModel.text = "RGB";
                    redSlider.gameObject.SetActive(true);
                    greenSlider.gameObject.SetActive(true);
                    blueSlider.gameObject.SetActive(true);
                    hueSlider.gameObject.SetActive(false);
                    hsvSatSlider.gameObject.SetActive(false);
                    hsvValueSlider.gameObject.SetActive(false);
                    hslSatSlider.gameObject.SetActive(false);
                    hslLightSlider.gameObject.SetActive(false);
                    break;
                case Model.HSV:
                    textCurrentModel.text = "HSV";
                    redSlider.gameObject.SetActive(false);
                    greenSlider.gameObject.SetActive(false);
                    blueSlider.gameObject.SetActive(false);
                    hueSlider.gameObject.SetActive(true);
                    hsvSatSlider.gameObject.SetActive(true);
                    hsvValueSlider.gameObject.SetActive(true);
                    hslSatSlider.gameObject.SetActive(false);
                    hslLightSlider.gameObject.SetActive(false);
                    break;
                case Model.HSL:
                    textCurrentModel.text = "HSL";
                    redSlider.gameObject.SetActive(false);
                    greenSlider.gameObject.SetActive(false);
                    blueSlider.gameObject.SetActive(false);
                    hueSlider.gameObject.SetActive(true);
                    hsvSatSlider.gameObject.SetActive(false);
                    hsvValueSlider.gameObject.SetActive(false);
                    hslSatSlider.gameObject.SetActive(true);
                    hslLightSlider.gameObject.SetActive(true);
                    break;
            }
        }

        private void InitializeBottomBar() {
            buttonAdvanceModel.onClick.AddListener(AdvanceModel);
            fieldHtmlCode.characterLimit = (alphaSlider ? "#RRGGBBAA".Length : "#RRGGBB".Length);
            fieldHtmlCode.onEndEdit.AddListener(SetHTMLCode);
        }

        private void RefreshBottomBar() {
            imageCurrentColor.color = ColorRGB;
            fieldHtmlCode.SetValue(GetHTMLCode(), ignoreOnEndEdit: true, ignoreOnValueChanged: true);
        }
        #endregion

    }

}