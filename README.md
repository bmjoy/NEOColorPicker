# NEO Color Picker 1.3
This is a simple color picker to be used in Unity.

![NEOColorPicker](https://user-images.githubusercontent.com/6721656/34644375-a5a6964c-f31c-11e7-880c-21e592988ce6.png)

Currently, it supports RGB, HSV, HSL and the HTML/hexadecimal codes.

## Installation
All you have to do is download the latest NEO Color Picker .unitypackage from the [Releases](https://github.com/NelsonWilliam/NEOColorPicker/releases/) page and drag it into your project.

*Note.: This includes [NEO Utils](https://github.com/NelsonWilliam/NEOUtils/) 1.1.1 and may overwrite any previously installed version. If you want to use a more recent version of NEO Utils, install it after installing NEO Color Picker.*

## Usage
To add the color picker to a scene, just drag the Color Picker prefab (found in the Prefabs folder) into a Canvas. The color picker will appear white and without the slides in the Editor – that's fine, the sliders are created at runtime. You can easily modify the UI elements to suit your needs.

![2018-01-06_20-06-44](https://user-images.githubusercontent.com/6721656/34644408-260e4de8-f31d-11e7-843d-570d53ca9f89.png)

### Code

The ColorPicker class allows you to:

* Use ``ColorRGB``, ``ColorHSL`` and ``ColorHSV`` to get and set the currently selected color, using any model supported. Values are between 0 and 1.
* Use ``SetColorField`` and ``GetColorField`` to get and set a single color field (e.g. Hue) of the currently selected color. Values are between 0 and 1.
* Use ``CurrentModel`` and ``AdvanceModel`` to get, set or advance the color model (e.g. HSL) being used by the color picker.
* Use ``UsingAlphaSlider`` to get and set the visibility of the alpha slider.
* Add listeners to `onColorChanged` to automatically invoke them when the color picker's current color changes. This can also be done through the Inspector:

![Inspector](https://user-images.githubusercontent.com/6721656/34712037-9bcbf200-f508-11e7-8dbd-057d1cb79fd2.png)

An example scene can be found in the Example folder.

## License
[MIT](/LICENSE.md)
