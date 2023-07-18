using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1.0f;
    public GameObject[] targets; // Since we only have four types of objects and we don't modify frequently in game so use array
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameAlive;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isGameAlive = true;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget()
    {
        while (isGameAlive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score < 0)
        {
            GameOver();
        }
        
        if (!isGameAlive)
        {
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameAlive = false;
    }
}
