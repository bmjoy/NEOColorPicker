using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExample : MonoBehaviour {

    private Renderer rend;

    private void Awake() {
        rend = GetComponent<Renderer>();
    }

    private void Update() {
        transform.Rotate(Mathf.PerlinNoise(Time.time / 2f, Time.time / 2f) * Vector3.forward * Time.deltaTime * 80f);
        transform.Rotate(Mathf.PerlinNoise(Time.time, Time.time) * Vector3.back * Time.deltaTime * 40f);
    }

    public void SetColor(Color color) {
        rend.material.color = color;
    }

}
