using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Platform.Cache;
using Mapbox.Unity.Map.Interfaces;
using Mapbox.Unity.Map.Strategies;
using Mapbox.Unity.Map.TileProviders;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class GPS : MonoBehaviour {

    public float latitude = 0f;
    public float longitude = 0f;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(isLocationActivated()); 
    }

    private IEnumerator isLocationActivated() {
        yield return new WaitForSeconds(3);
        GameObject compass = GameObject.Find("Compass");
        while (!compass.GetComponent<Compass>().startTracking) {
            yield return new WaitForSeconds(1);
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        float altitude = Input.location.lastData.altitude;
        Text button =  GameObject.Find("Go Home Button/location").GetComponent<Text>();
        button.text = "Long:" + longitude + "        Lat:" + latitude + "        Alt:" + altitude;
    }

    public void ChangeLocation() {
        Text locText =  GameObject.Find("InputField/Text").GetComponent<Text>();
        string[] loc = locText.text.Split(',');
        Vector2d localization = new Vector2d(float.Parse(loc[0], CultureInfo.InvariantCulture), float.Parse(loc[1], CultureInfo.InvariantCulture));
        AbstractMap map = FindObjectOfType<AbstractMap>();
        map.UpdateMap(localization);
        InputField input = GameObject.Find("InputField").GetComponent<InputField>();
        input.Select();
        input.text = "";
    }

    public void GoHome() {
        Vector2d localization = new Vector2d(latitude, longitude);
        AbstractMap map = FindObjectOfType<AbstractMap>();
        map.UpdateMap(localization);
    }
}
