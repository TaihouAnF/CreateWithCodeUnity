using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private ScoreManager scoreManager;
    public int animalScores;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.UpdateLives(-animalScores);
        }
        else if (other.CompareTag("Projectile"))
        {
            scoreManager.UpdateScore(animalScores);
        }
        // Doesn't have to destroy other since when they collide,
        // those destroyable object will also destroy themselves.
        // And both cases are the same, destroy itself.
        Destroy(gameObject);
    }

}
