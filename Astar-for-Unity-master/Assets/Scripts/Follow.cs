using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Drone;
    public GameObject Drone2;
    public GameObject Plane;
    public GameObject Plane2;
    public GameObject Building;
    public GameObject Building2;
    private Vector3 tmp, tmp2;
    public float offsetY;
    private void Start()
    {
        tmp2 = Plane.transform.position;
        tmp2.y =tmp2.y- offsetY;
        Plane2.transform.position = tmp2;

        tmp2 = Building.transform.position;
        tmp2.y =tmp2.y- (offsetY);
        Building2.transform.position = tmp2;
    }
    // Update is called once per frame
    void Update()
    {
        tmp= Drone.transform.position;
        tmp.y =tmp.y- (offsetY);
        Drone2.transform.position = tmp;

    }
}
