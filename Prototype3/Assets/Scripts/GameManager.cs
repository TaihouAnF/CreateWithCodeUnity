using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScripts;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScripts = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        if (playerControllerScripts.dashing)
        {
            score += 2;
        } 
        else
        {
            score++;
        }
        Debug.Log("Current score:" + score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over! And your score is:" + score);
    }
}
