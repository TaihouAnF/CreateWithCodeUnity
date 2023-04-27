using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            
        }
        // Doesn't have to destroy other since when they collide,
        // those destroyable object will also destroy themselves.
        // And both cases are the same, destroy itself.
        Destroy(gameObject);

    }
}
