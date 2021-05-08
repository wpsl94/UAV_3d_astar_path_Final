using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LatLonAlt : MonoBehaviour
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
    public static Vector3 GetUnityPosition(Vector2 latLonPosition, Vector2 northWestLatLon, Vector2 southEastLatLon, Vector3 northWestUnity, Vector3 southEastUnity, float altitude)
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

    public static float getUnityHeight(float altitude, float plane, double latlonxdistance)
    {
        float latlondistance = (float)latlonxdistance;
        float height = (altitude * plane) / latlondistance;
        return height;
    }

    string getRealVertices()
    {
        string vertices_position = "";
        for (int i = 0; i < 8; i++)
        {
            Vector3 vertex = model.GetComponent<MeshFilter>().mesh.vertices[i];
            // Vector2 vertex_latlon = new Vector2(model.transform.TransformPoint(vertex).x, model.transform.TransformPoint(vertex).z);
            Vector2 vertex_latlon_position = GetLatLonPosition(vertex, NorthWestlatlon, SouthEastlatlon, NorthWestUnity, SouthEastUnity);
            float vertex_altitude = (float)((model.transform.TransformPoint(vertex).y + 0.1) * latlonxdistance_p) / planeDistance;
            //여기 윗줄 +0.1 로 수정함 플레인이 -0.1 이므로
            //vertices_position += "vertex" + i.ToString() + ": lat, lon = " + vertex_latlon_position.ToString() + ", altitude = " + vertex_altitude + "m\n";
            vertices_position += "vertex" + i.ToString() + ": lat, lon = " + vertex_latlon_position.ToString() + ", altitude = " + Math.Round(vertex_altitude, 4) + "m\n";

        }
        return vertices_position;
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

    private string tmp;
    private double latlonxdistance;
    private double latlonxdistance_p;
    private float planeDistance;
    private string vertexInfo;
    private float unityHeight; //빌딩의 높이(m)를 유니티 좌표로 변환한 값
    private Vector3 tmpScale;
    //In each latlon (vector2), when you give NW or SE latitude/longitude value, x means latitude, y means longitude....! please do not confuse it

    private void Start()
    {

        model.transform.position = GetUnityPosition(latlonposition, NorthWestlatlon, SouthEastlatlon, NorthWestUnity, SouthEastUnity, altitude);
        unityposition = model.transform.position;

        planeDistance = NorthWestUnity.x - SouthEastUnity.x;
        if (planeDistance < 0) planeDistance *= -1;

        Vector2 SouthEastlatlon2 = SouthEastlatlon;
        SouthEastlatlon2.x = NorthWestlatlon.x; //x means latitude which is actually y value in unity
        //we just need to calculate distance between SE's x and NW'x (horizontal)

        //latlonxdistance = DistanceTo(NorthWestlatlon, SouthEastlatlon2, 0) * 1000;//kilometer to meter is x1000
        //latlonxdistance_p = DistanceTo(37.32123113781, 127.126145876769, 37.3212252836855, 127.126123344933, 0)*1000;
        //latlonxdistance_p = DistanceTo(37.3212252836855, 127.126145876769, 37.3212252836855, 127.126123344933, 0) * 1000;
        latlonxdistance_p = DistanceTo(37.527294, 126.932499, 37.527294, 126.953519, 0) * 1000;
        //unityposition.y = getUnityHeight(altitude, planeDistance, latlonxdistance_p);
        tmpScale = model.transform.localScale;
        tmpScale.y = getUnityHeight(altitude, planeDistance, latlonxdistance_p);
        //유니티 높이로 변환하려면 물체에 중력 작용을 빼야함 높이로 변환해서 위치시켜봤자 중력이 있으면 밑으로 떨어지니까 주의
        //model.transform.position = unityposition;
        model.transform.localScale = tmpScale;
        unityposition.y = 0.5091218f;
        model.transform.position = unityposition;
        Debug.Log("latlonxdistance = " + latlonxdistance_p);
        Debug.Log("planeDistance= " + planeDistance);
    }

    private void Update()
    {
        unityposition = model.transform.position;

        //change unity's y position to real altitude(m)
        altitude = (float)(model.transform.position.y * latlonxdistance_p) / planeDistance;

        vertexInfo = getRealVertices();

        //Vector3 pos = model.transform.position;
        //change unity's x,z position to real latitude and longitude coordinates
        latlonposition = GetLatLonPosition(unityposition, NorthWestlatlon, SouthEastlatlon, NorthWestUnity, SouthEastUnity);
        // tmp = "latitude : " + latlonposition.x + System.Environment.NewLine + "longitude : " + latlonposition.y + System.Environment.NewLine + "altitude : " + altitude + " m";
        tmp = "cube position: " + latlonposition.x + ", " + latlonposition.y + "\n" + vertexInfo;
        myText.text = tmp;
    }
}
