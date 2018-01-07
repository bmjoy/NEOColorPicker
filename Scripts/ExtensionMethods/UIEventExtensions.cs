using UnityEngine.UI;

public static class UIEventExtensions {

    //The methods below where originally created by "_Daniel_" and "floky"
    //on thread https://forum.unity.com/threads/change-the-value-of-a-toggle-without-triggering-onvaluechanged.275056/.

    //Those methods allow to change the values of some UI elements without
    //triggering events like onValueChanged. Useful for avoiding infinite loops.
    //They work by switching the events into empty events before doing the value changes
    //(so when they are triggered nothing happens), and then restoring the original events.

    //TODO: Add more UI types.

    static Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();
    public static void SetValue(this Slider instance, float value,
        bool ignoreOnValueChanged = false) {

        var originalOnValueChanged = instance.onValueChanged;
        if (ignoreOnValueChanged) instance.onValueChanged = emptySliderEvent;
        instance.value = value;
        instance.onValueChanged = originalOnValueChanged;
    }
   
    static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();
    public static void SetValue(this Toggle instance, bool value,
        bool ignoreOnValueChanged = false) {

        var originalOnValueChanged = instance.onValueChanged;
        if (ignoreOnValueChanged) instance.onValueChanged = emptyToggleEvent;
        instance.isOn = value;
        instance.onValueChanged = originalOnValueChanged;
    }

    static InputField.OnChangeEvent emptyInputFieldOnChangeEvent = new InputField.OnChangeEvent();
    static InputField.SubmitEvent emptyInputFieldSubmitEvent = new InputField.SubmitEvent();
    public static void SetValue(this InputField instance, string value,
        bool ignoreOnValueChanged = false, bool ignoreOnEndEdit = true) {

        var originalOnValueChanged = instance.onValueChanged;
        var originalOnEndEdit = instance.onEndEdit;
        if (ignoreOnValueChanged) instance.onValueChanged = emptyInputFieldOnChangeEvent;
        if (ignoreOnEndEdit) instance.onEndEdit = emptyInputFieldSubmitEvent;
        instance.text = value;
        instance.onValueChanged = originalOnValueChanged;
        instance.onEndEdit = originalOnEndEdit;
    }

    static Text.CullStateChangedEvent emptyCullStateChangedEvent = new Text.CullStateChangedEvent();
    public static void SetValue(this Text instance, object value,
        bool ignoreCullStateChanged = false) {

        var originalCullStateChanged = instance.onCullStateChanged;
        if (ignoreCullStateChanged) instance.onCullStateChanged = emptyCullStateChangedEvent;
        instance.text = value.ToString();
        instance.onCullStateChanged = originalCullStateChanged;
    }
}