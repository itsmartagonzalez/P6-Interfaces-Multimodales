using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI; //required for Input.compass

public class Compass : MonoBehaviour {
    private float currentMagneticHeading = 0f;
    private float lastMagneticHeading = 0f;
    public bool startTracking = false;
    private GameObject Player;
    void Start() {
        Player = GameObject.Find("Capsule");
        StartCoroutine(InitializeLocation());
    }

    IEnumerator InitializeLocation() {
        yield return new WaitForSeconds(2);
        if (!Input.location.isEnabledByUser) {
            Debug.Log("User did not enable the location");
            yield break;
        }

        Input.location.Start();

        int maxSeconds = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxSeconds > 0) {
            Debug.Log("Waiting for location service to initialize: " + maxSeconds);
            yield return new WaitForSeconds(1);
            maxSeconds--;
        }

        if (maxSeconds < 1) {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed) {
            print("Unable to determine device location");
            yield break;
        }
        Input.compass.enabled = true;
        startTracking = true;
        Vector3 initialPosition = Vector3.zero;
        initialPosition.y  = (float)Math.Round(Input.compass.magneticHeading, 2);
        Player.transform.localEulerAngles = initialPosition;
    }

    // Update is called once per frame
    void Update() {
        if (Input.location.status == LocationServiceStatus.Running) { 
            currentMagneticHeading = (float)Math.Round(Input.compass.magneticHeading, 2);
            if (Mathf.Abs(lastMagneticHeading - currentMagneticHeading) > 5f && Input.touchCount > 0 && Input.touches[0].phase != TouchPhase.Ended) {
                lastMagneticHeading = currentMagneticHeading;
                Player.transform.localEulerAngles = new Vector3(0, lastMagneticHeading, 0);
                transform.localEulerAngles = new Vector3(0, 0, lastMagneticHeading);
            }
        }
    }
    
}
