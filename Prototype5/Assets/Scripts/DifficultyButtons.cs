using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtons : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        // This is doing the same thing as we did in inspector, but more dynamic. 
        // dynamic binding can access objects that are created/available at runtime.
        // while static binding can only access serialized data (made before runtime)
        button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        gameManager.StartGame(1);
    }
}
