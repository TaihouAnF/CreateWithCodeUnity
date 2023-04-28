using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int lives = 3;
    private int scores = 0;
    private int scoresExchangeLife = 0;
    private int missThreshold = 25;

    public void UpdateLives(int newLives)
    {
        lives += newLives;
        lives = Mathf.Clamp(lives, 0, 20);
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
        }
        else
        {
            Debug.Log("Current lives: " + lives);
        }
    }

    public void UpdateScore(int newScores) 
    {
        scores += newScores;
        scoresExchangeLife += newScores;
        if (scoresExchangeLife >= 2)
        {
            UpdateLives(scoresExchangeLife / 2);
            scoresExchangeLife %= 2;
        }
        Debug.Log("Score: " + scores);
    }

    public void MissFood ()
    {
        missThreshold--;
        missThreshold = Mathf.Clamp(missThreshold, 0, 25);
        if (missThreshold <= 0)
        {
            Debug.Log("You Missed too many!");
            UpdateLives(-1);
            missThreshold = 25;
        }
    }
}
