using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If an object goes pass player's view, destroy it. Doesn't make sense to say game over
        // in current amount of animals. Thus removed "game over" and make it into one line.
        if (transform.position.z > topBound || transform.position.z < lowerBound || 
            transform.position.x < -sideBound || transform.position.x > sideBound)
        {
            Destroy(gameObject);
        }
    }
}
