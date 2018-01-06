using UnityEngine;
using UnityEngine.UI;

namespace NEO {

    public class ButtonPropertyExample : MonoBehaviour {

        public ColorPicker colorPicker;
        public Image imageToChange;
            
        public void UpdateColor() {
            imageToChange.color = colorPicker.CurrentColor;
        }

        public void RandomizeColor() {
            colorPicker.CurrentColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),
                Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

    }
}