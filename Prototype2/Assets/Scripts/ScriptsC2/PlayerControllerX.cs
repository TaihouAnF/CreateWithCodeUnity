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
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTimeLeft <= 0f) 
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                currentTimeLeft = pressInterval;
            }
        }
        currentTimeLeft -= Time.deltaTime;
        currentTimeLeft = Mathf.Clamp(currentTimeLeft, 0f, pressInterval);
    }

    
}
