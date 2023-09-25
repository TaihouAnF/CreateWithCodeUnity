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
            // Instead of destroying the projectile when it collides with an animal
            //Destroy(other.gameObject); 

            // Just deactivate the food and destroy the animal
            other.gameObject.SetActive(false);
        } 
    }

}
