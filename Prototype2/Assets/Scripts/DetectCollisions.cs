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
            Destroy(gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {
            GetComponent<HungerBarManager>().FeedAnimal(1);
            Destroy(other.gameObject);
        } 
    }

}
