using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingVehicleX : MonoBehaviour
{
    private float speed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
