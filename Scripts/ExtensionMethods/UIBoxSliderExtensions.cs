using UnityEngine.UI;

public static class UIBoxSliderExtensions {

    static BoxSlider.BoxSliderEvent emptyBoxSliderEvent = new BoxSlider.BoxSliderEvent();
    public static void SetValue(this BoxSlider instance, float valueX, float valueY,
        bool ignoreOnValueChanged = false) {

        var originalOnValueChanged = instance.onValueChanged;
        if (ignoreOnValueChanged) instance.onValueChanged = emptyBoxSliderEvent;
        instance.value = valueX;
        instance.valueY = valueY;
        instance.onValueChanged = originalOnValueChanged;
    }

}
