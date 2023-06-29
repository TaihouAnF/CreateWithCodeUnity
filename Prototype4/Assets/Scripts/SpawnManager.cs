using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject[] minionPrefab;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    public int bossRound;
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
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemiesWaves(waveNumber);
            }
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

    private void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn = bossRound != 0 ? currentRound / bossRound : 1;
        var boss = Instantiate(bossPrefab, RandomSpawningPos(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().minionCount = miniEnemysToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMiniIndex = Random.Range(0, minionPrefab.Length);
            Instantiate(minionPrefab[randomMiniIndex], RandomSpawningPos(), minionPrefab[randomMiniIndex].transform.rotation);
        }
    }

}
