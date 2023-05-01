using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HungerBarManager : MonoBehaviour
{
    public Slider hungerSlider;
    public int feedAmount;  // Amount for each animal to be feed to full
    private int currentAmount = 0;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = feedAmount;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Feed animal with food, destroy them here if they are fed.
    public void FeedAnimal(int amount)
    {
        currentAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentAmount;
        

        if (currentAmount >= feedAmount)
        {
            scoreManager.UpdateScore(feedAmount);
            Destroy(gameObject, 0.1f);
        }
    }
}
