using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float pressInterval = 1f;
    private float currentTimeLeft = 0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog, here we directly use time as the T/F.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If time interval has been passed.
            if (currentTimeLeft <= 0f) 
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                // Reset the interval to wait.
                currentTimeLeft = pressInterval;
            }
        }
        // Update the time, it should not be in the last "if", otherwise it would only update when spacebar is pressed
        currentTimeLeft -= Time.deltaTime;
        currentTimeLeft = Mathf.Clamp(currentTimeLeft, 0f, pressInterval);
    }

    
}
