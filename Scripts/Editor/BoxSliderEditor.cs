/* This script's copyrights belong to Judah Perez under the MIT License
and was originally found on: https://github.com/judah4/HSV-Color-Picker-Unity/.
The full license can be found below:

"The MIT License(MIT)

Copyright(c) 2016 Judah Perez

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE." */


using UnityEngine.UI;

namespace UnityEditor.UI {

    [CustomEditor(typeof(BoxSlider), true)]
    [CanEditMultipleObjects]
    public class BoxSliderEditor : SelectableEditor {

        SerializedProperty m_HandleRect;
        SerializedProperty m_MinValue;
        SerializedProperty m_MaxValue;
        SerializedProperty m_WholeNumbers;
        SerializedProperty m_Value;
        SerializedProperty m_ValueY;
        SerializedProperty m_OnValueChanged;

        protected override void OnEnable() {
            base.OnEnable();
            m_HandleRect = serializedObject.FindProperty("m_HandleRect");

            m_MinValue = serializedObject.FindProperty("m_MinValue");
            m_MaxValue = serializedObject.FindProperty("m_MaxValue");
            m_WholeNumbers = serializedObject.FindProperty("m_WholeNumbers");
            m_Value = serializedObject.FindProperty("m_Value");
            m_ValueY = serializedObject.FindProperty("m_ValueY");
            m_OnValueChanged = serializedObject.FindProperty("m_OnValueChanged");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(m_HandleRect);

            if (m_HandleRect.objectReferenceValue != null) {
                EditorGUI.BeginChangeCheck();


                EditorGUILayout.PropertyField(m_MinValue);
                EditorGUILayout.PropertyField(m_MaxValue);
                EditorGUILayout.PropertyField(m_WholeNumbers);
                EditorGUILayout.Slider(m_Value, m_MinValue.floatValue, m_MaxValue.floatValue);
                EditorGUILayout.Slider(m_ValueY, m_MinValue.floatValue, m_MaxValue.floatValue);

                // Draw the event notification options
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(m_OnValueChanged);
            } else {
                EditorGUILayout.HelpBox("Specify a RectTransform for the slider fill or the slider handle or both. Each must have a parent RectTransform that it can slide within.", MessageType.Info);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
