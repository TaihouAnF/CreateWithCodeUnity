using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemiesWaves(waveNumber);
        Instantiate(powerUpPrefab, RandomSpawningPos(), enemyPrefab.transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemiesWaves(waveNumber);
            Instantiate(powerUpPrefab, RandomSpawningPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 RandomSpawningPos() {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new(spawnRangeX, 0, spawnRangeZ);
        return spawnPos;
    }

    private void SpawnEnemiesWaves(int waves)
    {
        for(int i = 0; i < waves; i++)
        {
            Instantiate(enemyPrefab, RandomSpawningPos(), enemyPrefab.transform.rotation);
        }
    }
}
