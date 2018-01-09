using UnityEngine;

namespace NEO.NEOColorPicker {

    public class CubeEventExample : MonoBehaviour {

        public ColorPicker picker;
        private Renderer rend;

        private void Awake() {
            rend = GetComponent<Renderer>();
        }

        private void Start() {
            picker.onColorChanged.AddListener(SetColor);
            SetColor(picker.ColorRGB);
        }

        private void Update() {
            transform.Rotate((0.5f + Mathf.PerlinNoise(Time.time / 2f, Time.time / 2f)) * Vector3.forward * Time.deltaTime * 80f);
            transform.Rotate((0.5f + Mathf.PerlinNoise(Time.time / 2f, Time.time / 2f)) * Vector3.back * Time.deltaTime * 40f);
        }

        public void SetColor(Color color) {
            rend.material.color = color;
        }

        private void OnDestroy() {
            picker.onColorChanged.RemoveListener(SetColor);
        }

    }

}
