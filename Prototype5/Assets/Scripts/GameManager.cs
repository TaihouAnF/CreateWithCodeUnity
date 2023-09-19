using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1.0f;
    public GameObject[] targets; // Since we only have four types of objects and we don't modify frequently in game so use array
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public bool isGameAlive;
    public GameObject titleScreen;
    public AudioClip goodBeClicked;
    public AudioClip badBeClicked;
    public AudioSource audioSource;
    public GameObject pausePanel;
    private bool shouldPause;
    private int score;
    private int lives;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePaused();
        }
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

    public void StartGame(int difficulty)
    {
        score = 0;
        lives = 3;
        isGameAlive = true;
        titleScreen.SetActive(false);
        spawnRate /= difficulty;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        shouldPause = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        if (scoreToAdd < 0)
        {
            audioSource.PlayOneShot(badBeClicked, 1.0f);
            lives -= 1;
        }
        else
        {
            audioSource.PlayOneShot(goodBeClicked, 1.0f);
        }
       
        if (score < 0 || lives <= 0)
        {
            GameOver();
        }

        if (!isGameAlive)
        {
            scoreText.gameObject.SetActive(false);
            livesText.gameObject.SetActive(false);
        }
        else
        {
            scoreText.text = "Score: " + score;
            livesText.text = "Lives: " + lives;
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameAlive = false;
    }

    private void ChangePaused()
    {
        shouldPause = !shouldPause;
        pausePanel.gameObject.SetActive(shouldPause);
        Time.timeScale = shouldPause ? 0.0f : 1.0f;
    }
}
