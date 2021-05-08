using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneObjects : MonoBehaviour
{
    public GameObject model;
    void Start()
    {
        model.SetActive(false);
    }

    public void showModel()
    {
        model.SetActive(true);
    }
}
