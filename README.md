# NEO Color Picker
This is a simple color picker to be used in Unity.

![NEOColorPicker](https://user-images.githubusercontent.com/6721656/34644375-a5a6964c-f31c-11e7-880c-21e592988ce6.png)

Currently it supports RGB, HSV and the hexadecimal codes.

## How to add
Currently there are no packages, but you can just clone this repository into your project.

To add the color picker to a scene, just drag the Color Picker prefab (found in the Prefabs folder) into a Canvas. You can easily modify the UI elements to suit your needs.

![2018-01-06_20-06-44](https://user-images.githubusercontent.com/6721656/34644408-260e4de8-f31d-11e7-843d-570d53ca9f89.png)

Don't worry that the color picker will appear white and without all the sliders in the Editor – they are created at runtime. Those sliders are just prefabs in the Prefabs folder and can also be edited to suit your needs.

## How to use
To get or set the currently selected color, just use the color picker's `CurrentColor` property. Alternatively, you can add listeners to the `onColorChanged` event (via code or directly through the Inspector) to be called whenever the color changes.

![Inspector](https://user-images.githubusercontent.com/6721656/34644379-b04ab15a-f31c-11e7-85fe-c09f3eeea9cd.png)

An exemple scene can be found in the Example folder.

## Credits
* The BoxSlider was originally created by Judah Perez for his [HSV-Color-Picker-Unity](https://github.com/judah4/HSV-Color-Picker-Unity) and is also licensed under the MIT License.
* The extensions for bypassing UI element's events like `onValueChanged` where originally made by "floky" and "_Daniel_" in [this Unity Forum thread](https://forum.unity.com/threads/change-the-value-of-a-toggle-without-triggering-onvaluechanged.275056/).

## License
[MIT](/LICENSE.md)
