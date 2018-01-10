using UnityEngine;
using UnityEngine.UI;

namespace NEO.NEOColorPicker {

    public class ButtonPropertyExample : MonoBehaviour {

        public ColorPicker colorPicker;
        public Image imageToChange;
            
        public void UpdateColor() {
            imageToChange.color = colorPicker.ColorRGB;
        }

        public void RandomizeColor() {
            colorPicker.ColorRGB = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }

        public void ToggleAlpha() {
            colorPicker.UsingAlphaSlider = !colorPicker.UsingAlphaSlider;
        }

        public void AdvanceModel() {
            colorPicker.AdvanceModel();
        }

    }

}