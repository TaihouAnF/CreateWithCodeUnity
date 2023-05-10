using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private GameObject backgound;
    private GameObject spawnManager;
    private Animator playerAnim;
    private PlayerController playerControllerScripts;
    private int score;
    public Transform startingLocation;
    public float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        backgound = GameObject.Find("Background");
        spawnManager = GameObject.Find("SpawnManager");
        playerAnim = player.GetComponent<Animator>();
        playerControllerScripts = player.GetComponent<PlayerController>();
        score = 0;

        StartCoroutine(nameof(PlayerIntroLerping));   // Using Coroutine, split the task into multiple frames.
                                                      // As most of Func will finish in one single frames/instantly; And here is animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayerIntroLerping()
    {
        Vector3 startPos = playerControllerScripts.transform.position;
        Vector3 endPos = startingLocation.position;
        float distance = Vector3.Distance(startPos, endPos);
        float currentTime = 0;

        float completedJourney = currentTime * lerpSpeed;
        float completeness = completedJourney / distance;

        playerAnim.SetFloat("Speed_Multiplier", 0.5f);
        playerControllerScripts.enabled = false;                // Preventing player to move/obj to move left or spawn yet, not showing game over
        backgound.GetComponent<MoveLeft>().enabled = false;     // Can't use isGameOver here since it's linked to spawning and if it's over it stops entirely, not pause
        spawnManager.GetComponent<SpawnManager>().enabled = false;

        while (completeness < 1)
        {
            currentTime += Time.deltaTime;
            completedJourney = currentTime * lerpSpeed;
            completeness = completedJourney / distance;
            playerControllerScripts.transform.position = Vector3.Lerp(startPos, endPos, completeness);
            yield return null;
        }

        playerControllerScripts.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1f);
        playerControllerScripts.enabled = true;
        backgound.GetComponent<MoveLeft>().enabled = true;
        spawnManager.GetComponent<SpawnManager>().enabled = true;
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
