using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

    private Vector3 newPosition = Vector3.zero;
    private Camera mainCam;
    private Camera outsideCam;

    private void Start() {
        mainCam = GameObject.Find("Camera").GetComponent<Camera>();
        outsideCam = GameObject.Find("Outside Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    private void Update() {
        newPosition = Quaternion.Euler(-90, 0, 0) * Input.acceleration;
        transform.position += transform.forward * Time.deltaTime * (-newPosition.z) * 3f;
        if (Input.touchCount == 2 && Input.touches[0].phase == TouchPhase.Began && Input.touches[1].phase == TouchPhase.Began) {
            mainCam.enabled = !mainCam.enabled;
            outsideCam.enabled = !outsideCam.enabled;
        }
    }
}
