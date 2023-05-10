using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10f;
    private float leftBound = -15f;
    private PlayerController playerControllerScript;
    private GameObject player;
    private GameManager gameManager;
    private bool passed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        passed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.isGameOver)
        {
            if (playerControllerScript.dashing)
            {
                transform.Translate((2 * speed) * Time.deltaTime * Vector3.left);
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
            }
            
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < player.transform.position.x - 1.0f && !passed)
        {
            gameManager.AddScore();
            passed = true;
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
