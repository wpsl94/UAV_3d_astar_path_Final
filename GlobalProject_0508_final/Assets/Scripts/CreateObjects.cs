using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    public GameObject RandomCube;
    public GameObject NWposition;
    public GameObject SEposition;
    public float height;
    int num = 0;
    float max_x, min_x, max_z, min_z, ran_x, ran_z;

    public void Start()
    {
        RandomCube.SetActive(false);
    }
    public void ShowObjects()
    {
        if (RandomCube.activeSelf == false) RandomCube.SetActive(true);

        Vector3 NW = NWposition.transform.position;
        Vector3 SE = SEposition.transform.position;
        if (NW.x > SE.x)
        {
            max_x = NW.x;
            min_x = SE.x;
        }
        else { 
            max_x = SE.x;
            min_x = NW.x;
        }

        if (NW.z > SE.z) { 
            max_z = NW.z;
            min_z = SE.z;
        }
        else
        {
            max_z = SE.z;
            min_z = NW.z;
        }

        ran_x = Random.Range(min_x, max_x);
        ran_z = Random.Range(min_z, max_z);
        


        RandomCube.transform.localScale = new Vector3(1, 1, 1);
        RandomCube.transform.position = new Vector3(ran_x, height,ran_z);

    }
}
