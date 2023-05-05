using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPosition = new(25, 0 , 0);
    private float startDelay = 2f;
    private float spawnInterval = 2f;
    private PlayerController playerControllerScripts;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnInterval);
        playerControllerScripts = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnObstacle ()
    {
        if (!playerControllerScripts.isGameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
        else
        {
            CancelInvoke(nameof(SpawnObstacle));
        }
        
    }
}
