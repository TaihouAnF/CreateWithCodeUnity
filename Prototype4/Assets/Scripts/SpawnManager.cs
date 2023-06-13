using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    private float spawnRange = 9f;
    // Start is called before the first frame update
    void Start()
    {
        int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);

        SpawnEnemiesWaves(waveNumber);
        Instantiate(powerUpPrefabs[randomPowerUpIndex], RandomSpawningPos(), powerUpPrefabs[randomPowerUpIndex].transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemiesWaves(waveNumber);
            int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerUpIndex], RandomSpawningPos(), powerUpPrefabs[randomPowerUpIndex].transform.rotation);
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
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], RandomSpawningPos(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}
