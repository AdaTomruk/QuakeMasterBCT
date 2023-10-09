using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.Rendering;
using System;
using Unity.VisualScripting;

public class jsonRead : MonoBehaviour
{
    public TextAsset landingjson;
    public landingSiteList langingSites = new landingSiteList();
    public GameObject langingMarkerPrefab;
    public Transform langingMarkerPrefabParent;
    public GameObject langingMarkerPrefabToggle;
    public Transform langingMarkerPrefabToggleParent;
    public GameObject langingMarkerText;



    [System.Serializable]
    public class landingSites
    {
        public string label;
        public float lat;
        public float lng;
       
    }

    [System.Serializable]
    public class landingSiteList
    {
        public landingSites[] landingSites;
    }

    // Start is called before the first frame update
    void Start()
    {
        langingSites = JsonUtility.FromJson<landingSiteList>(landingjson.text);
        
        for (int i = 0; i < langingSites.landingSites.Length; i++)
        {
            var landingMarker = (GameObject)Instantiate(langingMarkerPrefab, ConvertToCartesian(langingSites.landingSites[i].lat, langingSites.landingSites[i].lng), Quaternion.identity);
            landingMarker.transform.LookAt(Vector3.zero);
            landingMarker.name = (langingSites.landingSites[i].label);
            landingMarker.transform.SetParent(langingMarkerPrefabParent);
            landingMarker.tag = i + 28.ToString();
            var landingMarkerToggle = (GameObject)Instantiate(langingMarkerPrefabToggle, new Vector3(0, 0, 0), Quaternion.identity);
            landingMarkerToggle.transform.SetParent(langingMarkerPrefabToggleParent);
            landingMarkerToggle.name = i+28.ToString();
            landingMarkerToggle.GetComponent<itemToggle>().LabelText = langingSites.landingSites[i].label;
            landingMarkerToggle.tag = "39";

            var landingMarkerText = (GameObject)Instantiate(langingMarkerText, ConvertToCartesian(langingSites.landingSites[i].lat, langingSites.landingSites[i].lng), Quaternion.identity);
            landingMarkerText.transform.LookAt(Vector3.zero);
            landingMarkerText.transform.SetParent(landingMarker.transform);
            landingMarkerText.GetComponent<textNamer>().Text = langingSites.landingSites[i].label;
        }
    }

    static Vector3 ConvertToCartesian(double lat, double lon)
    {
        double a = 25; // equatorial radius
        double b = 25; // polar radius

        double radLat = (Math.PI / 180) * lat;
        double radLon = (Math.PI / 180) * lon;

        double n = (a * a) / (Math.Sqrt(a * a * Math.Cos(radLat) * Math.Cos(radLat) + b * b * Math.Sin(radLat) * Math.Sin(radLat)));

        double x = n * Math.Cos(radLat) * Math.Cos(radLon);
        double z = n * Math.Cos(radLat) * Math.Sin(radLon);
        double y = a * a / (b * b) * n * Math.Sin(radLat);

        Console.WriteLine($"{x}, {y}, {z}");
        return new Vector3((float)x, (float)y, (float)z);

       

    }
}
