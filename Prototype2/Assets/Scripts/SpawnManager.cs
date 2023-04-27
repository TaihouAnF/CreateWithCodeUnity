using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20f;
    private float spawnMaxZ = 15f;
    private float spawnMinZ = -2f;
    private float spawnPosX = 22f;
    private float spawnPosZ = 20f;
    private float startDelay = 2f;
    private float spawnInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int spawnCase = Random.Range(0, 3);
        switch (spawnCase)
        {
            case 0:
                SpawnRandomAnimalsAbove();
                break;
            case 1:
                SpawnRandomAnimalsRight();
                break;
            case 2: 
                SpawnRandomAnimalsLeft();
                break;
        }
    }

    void SpawnRandomAnimalsAbove()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPos,
                    animalPrefabs[animalIndex].transform.rotation);
    }

    void SpawnRandomAnimalsRight()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, Random.Range(spawnMinZ, spawnMaxZ));
        Vector3 rotation = new Vector3(0, -90f, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }

    void SpawnRandomAnimalsLeft()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(-spawnPosX, 0, Random.Range(spawnMinZ, spawnMaxZ));
        Vector3 rotation = new Vector3(0, 90f, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }
}
