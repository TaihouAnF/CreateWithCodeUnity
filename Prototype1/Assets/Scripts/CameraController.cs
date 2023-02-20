using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera_0;
    public GameObject camera_1;
    private Boolean seat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            seat = !seat;
        }

        if (seat)
        {
            camera_0.SetActive(false);
            camera_1.SetActive(true);
        }
        else
        {
            camera_0.SetActive(true);
            camera_1.SetActive(false);
        }
    }
}
