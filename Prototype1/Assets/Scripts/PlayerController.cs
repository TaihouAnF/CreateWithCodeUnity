using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string inputId;
    public Camera mainCamera;       // Main Cam behind and above 
    public Camera hoodCamera;       // Hood cam inside the vehicle 
    public KeyCode switchKey;       // Key to switch cams

    private float speed = 20.0f;
    private float turnSpeed = 60.0f;
    private float forwardInput;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obtain player control on direction
        horizontalInput = Input.GetAxis("Horizontal" + inputId);
        forwardInput = Input.GetAxis("Vertical" + inputId);

        // Move the vehicle forward
        transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward);

        // Change the vehicle's orientation
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

        if (Input.GetKeyDown(switchKey)) {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
