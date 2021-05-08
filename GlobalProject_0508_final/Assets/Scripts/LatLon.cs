using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LatLon : MonoBehaviour
{
    //class that converts latitude / longitude to Unity position and the reverse

    //convert a coordinate from one set of ranges to another set of ranges
    private static float convertCoordinate(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float returnValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return returnValue;
    }

    //A LatLon Vector2 includes Latitude as the x value and Longitude as the y value
    //A Unity world coordinate has x as the west/east (longitude) and z as the north/south (latitude)

    //This method takes a LatLon Vector2 and translates it into this zone's game world coordinates
    //It does this by taking two points, a Noth West point and South East point in both LatLon and Unity world space positions to do the translation
    public static Vector3 GetUnityPosition(Vector2 latLonPosition, Vector2 northWestLatLon, Vector2 southEastLatLon, Vector3 northWestUnity, Vector3 southEastUnity,float altitude)
    {
        //check if this zone covers the antimeridian (where 180 and -180 degress longitude meet)
        if (southEastLatLon.y < northWestLatLon.y)
        {
            //Add 360 to any negative longitude positions so that longitude values are lower the further west
            southEastLatLon = new Vector2(southEastLatLon.x, southEastLatLon.y + 360f);
            if (latLonPosition.y < 0f)
            {
                latLonPosition = new Vector2(latLonPosition.x, latLonPosition.y + 360f);
            }
        }
        float newUnityLat = convertCoordinate(latLonPosition.x, southEastLatLon.x, northWestLatLon.x, southEastUnity.z, northWestUnity.z);
        float newUnityLon = convertCoordinate(latLonPosition.y, southEastLatLon.y, northWestLatLon.y, southEastUnity.x, northWestUnity.x);
        Vector3 unityWorldPosition = new Vector3(newUnityLon, altitude, newUnityLat);
        return unityWorldPosition;
    }

    public static Vector2 GetLatLonPosition(Vector3 unityPosition, Vector2 northWestLatLon, Vector2 southEastLatLon, Vector3 northWestUnity, Vector3 southEastUnity)
    {
        bool antimeridian = false;
        //check if this zone covers the antimeridian (where 180 and -180 degress longitude meet)
        if (southEastLatLon.y < northWestLatLon.y)
        {
            antimeridian = true;
            //Add 360 to any negative longitude positions so that longitude values are lower the further west
            southEastLatLon = new Vector2(southEastLatLon.x, southEastLatLon.y + 360f);
        }
        float newlat = convertCoordinate(unityPosition.z, southEastUnity.z, northWestUnity.z, southEastLatLon.x, northWestLatLon.x);
        float newlon = convertCoordinate(unityPosition.x, southEastUnity.x, northWestUnity.x, southEastLatLon.y, northWestLatLon.y);
        if (antimeridian)
        {
            if (newlon > 180f)
            {
                newlon = newlon - 360f;
            }
        }
        Vector2 latLonPosition = new Vector2(newlat, newlon);
        return latLonPosition;
    }

    public enum DistanceUnits { Kilometers, NauticalMiles, Miles };

    //Calculate distance between two latitude/longitude coordinates
    //Takes LatLon vector2's and returns a distance between them
    //location.x is lat, location.y is longitude
    
    //Returns the distance from one location to another
    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, DistanceUnits units = DistanceUnits.NauticalMiles)
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        switch (units)
        {
            case DistanceUnits.Kilometers: //Kilometers
                return dist * 1.609344;
            case DistanceUnits.NauticalMiles: //Nautical Miles
                return dist * 0.8684;
            case DistanceUnits.Miles: //Miles
                return dist;
        }
        return dist;
    }
    public static double DistanceTo(Vector2 location1, Vector2 location2, DistanceUnits units = DistanceUnits.NauticalMiles)
    {
        return DistanceTo(location1.x, location1.y, location2.x, location2.y, units);
    }

    public TextMeshProUGUI myText;
    public GameObject model;
    public Vector3 unityposition;
    public Vector3 NorthWestUnity;
    public Vector3 SouthEastUnity;

    public float altitude;
    public Vector2 latlonposition;
    public Vector2 NorthWestlatlon;
    public Vector2 SouthEastlatlon;

    //In each latlon (vector2), when you give NW or SE latitude/longitude value, x means latitude, y means longitude....! please do not confuse it

    private void Start()
    {   
        model.transform.position = GetUnityPosition(latlonposition, NorthWestlatlon, SouthEastlatlon, NorthWestUnity, SouthEastUnity, altitude);
        unityposition = model.transform.position;
        
    }

    private void Update()
    {
        //Vector3 pos = model.transform.position;
        latlonposition = GetLatLonPosition(unityposition, NorthWestlatlon, SouthEastlatlon, NorthWestUnity, SouthEastUnity);
        myText.text = "latitude : " + latlonposition.x + System.Environment.NewLine + "longitude : " + latlonposition.y;
    }
}
