using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.Rendering;
using System;
using Unity.VisualScripting;

public class seismicEventJsonReader : MonoBehaviour
{
    public TextAsset seismicEventJson;
    public seismicEventLocationsList seismicEventLocations = new seismicEventLocationsList();

    public GameObject eventMarkerPrefab;
    public GameObject eventMarkerPrefabToogle;
    public GameObject eventMarkerTextPrefab;


    public Transform eventMarkerPrefabParent;
    public Transform eventMarkerPrefabToogleParent;




    [System.Serializable]
    public class seismicEventLocation
    {   
        public int Year;
        public int Day;
        public int H;
        public int M;
        public int S;
        public float Lat;
        public float Long;
        public float Magnitude;
        public int Delta_a;
    }

    [System.Serializable]
    public class seismicEventLocationsList
    {
        public seismicEventLocation[] seismicEventLocationArray;
    }


    void Start()
    {
        seismicEventLocations = JsonUtility.FromJson<seismicEventLocationsList>(seismicEventJson.text);

        Debug.Log($"Event location count:{seismicEventLocations.seismicEventLocationArray.Length}");

        for (   int i = 0; i < seismicEventLocations.seismicEventLocationArray.Length; i++)
        {
            var seismicEventMarker = (GameObject)Instantiate(eventMarkerPrefab, ConvertToCartesian(seismicEventLocations.seismicEventLocationArray[i].Lat, seismicEventLocations.seismicEventLocationArray[i].Long), Quaternion.identity);
            seismicEventMarker.name = (seismicEventLocations.seismicEventLocationArray[i].Magnitude.ToString());
            //seismicEventMarker.name = ((seismicEventLocations.seismicEventLocationArray[i].Year.ToString())+" "+(seismicEventLocations.seismicEventLocationArray[i].Day.ToString()));
            seismicEventMarker.transform.SetParent(eventMarkerPrefabParent);
            seismicEventMarker.transform.localScale = new Vector3((float)seismicEventLocations.seismicEventLocationArray[i].Magnitude, (float)seismicEventLocations.seismicEventLocationArray[i].Magnitude, (float)seismicEventLocations.seismicEventLocationArray[i].Magnitude);
            seismicEventMarker.transform.LookAt(Vector3.zero);
            seismicEventMarker.tag = i.ToString();

            var seismicEventMarkerText = (GameObject)Instantiate(eventMarkerTextPrefab, ConvertToCartesian(seismicEventLocations.seismicEventLocationArray[i].Lat, seismicEventLocations.seismicEventLocationArray[i].Long), Quaternion.identity);
            seismicEventMarkerText.transform.LookAt(Vector3.zero);
            seismicEventMarkerText.transform.SetParent(seismicEventMarker.transform);
            seismicEventMarkerText.GetComponent<textNamer>().Text = CalculateDate(seismicEventLocations.seismicEventLocationArray[i].Year, seismicEventLocations.seismicEventLocationArray[i].Day);
            seismicEventMarkerText.GetComponent<textNamer>().magnitudeText = seismicEventLocations.seismicEventLocationArray[i].Magnitude.ToString();


            var seismicEventMarkerToggle = (GameObject)Instantiate(eventMarkerPrefabToogle, new Vector3(0,0,0), Quaternion.identity);
            seismicEventMarkerToggle.transform.SetParent(eventMarkerPrefabToogleParent);
            seismicEventMarkerToggle.tag = "40";
            seismicEventMarkerToggle.name = i.ToString();
            seismicEventMarkerToggle.GetComponent<itemToggle>().LabelText = (CalculateDate(seismicEventLocations.seismicEventLocationArray[i].Year, seismicEventLocations.seismicEventLocationArray[i].Day));
            seismicEventMarkerToggle.GetComponent<itemToggle>().MagnitudeLableText = (seismicEventLocations.seismicEventLocationArray[i].Magnitude.ToString());


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

    static string CalculateDate(int year, int day)
    {
        DateTime date = new DateTime(year, 1, 1).AddDays(day - 1);
        string month = date.ToString("MMMM");
        int dateDay = date.Day;
        int dateYear = date.Year;

        return $"{month} {dateDay}, {dateYear}";
    }

}
