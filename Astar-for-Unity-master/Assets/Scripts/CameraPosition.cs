using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Vector3 cameraPos;
    //Vector3 cameraPos = new Vector3(-2.8f, 8.0f, 2.8f);
    // Update is called once per frame
    void Update()
    {
        this.transform.position = cameraPos;

    }
}
