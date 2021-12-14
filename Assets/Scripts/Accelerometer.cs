using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

    private Vector3 newPosition = Vector3.zero;

    // Update is called once per frame
    private void Update() {
        newPosition = Quaternion.Euler(-90, 0, 0) * Input.acceleration;
        transform.position += transform.forward * Time.deltaTime * (-newPosition.z) * 3f;
        Debug.DrawRay(transform.position + Vector3.up, newPosition, Color.red);
    }
}
