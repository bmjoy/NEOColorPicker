# NEO Color Picker 1.2
This is a simple color picker to be used in Unity.

![NEOColorPicker](https://user-images.githubusercontent.com/6721656/34644375-a5a6964c-f31c-11e7-880c-21e592988ce6.png)

Currently, it supports RGB, HSV, HSL and the HTML/hexadecimal codes.

## Installation
**This project requires the [NEO Utils](https://github.com/NelsonWilliam/NEOUtils/) to be previously installed in the Unity Project.** 

With NEO Utils already installed, all you have to do is download the latest NEO Color Picker .unitypackage from the [Releases](https://github.com/NelsonWilliam/NEOColorPicker/releases/) page and drag it into your project.

## Usage
To add the color picker to a scene, just drag the Color Picker prefab (found in the Prefabs folder) into a Canvas. The color picker will appear white and without the slides in the Editor – that's fine, the sliders are created at runtime. You can easily modify the UI elements to suit your needs.

![2018-01-06_20-06-44](https://user-images.githubusercontent.com/6721656/34644408-260e4de8-f31d-11e7-843d-570d53ca9f89.png)

### Code

The ColorPicker class allows you to:

* Get and set the currently selected color in any color model supported, using the properties ``ColorRGB``, ``ColorHSL`` and ``ColorHSV``.
* Get and set a single color field (like Red, Alpha or Hue) for the currently selected color, using the methods ``SetColorField`` and ``GetColorField``. Values are between 0 and 1.
* Get and set the color model (RGB, HSV etc.) being currently used, using the property ``CurrentModel``. This is what sets what sliders will be shown.
* Advance the current color model to the next one, using the method ``AdvanceModel``.
* Add a listener to the ``onColorChanged`` event. The event is invoked whenever the color is changed (using the sliders or code). You can also add listeners through the Inspector:

![Inspector](https://user-images.githubusercontent.com/6721656/34644379-b04ab15a-f31c-11e7-85fe-c09f3eeea9cd.png)

An example scene can be found in the Example folder.

## License
[MIT](/LICENSE.md)
