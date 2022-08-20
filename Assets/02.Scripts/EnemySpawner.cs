using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float spawnDelay;
    private float spawnTime;
    private float spawnPositionX = 5f;
    private float spawnPositionY = 12f;

    public float enemyIncreaseValueTime;

    public bool isSingleSpawn = true;
    private bool isPlaying = false;

    private void Start()
    {
        EventManager.StartListening("GameOver", StopSpawn);
        EventManager.StartListening("GameStart", StartSpawn);
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        spawnTime += Time.deltaTime;
        enemyIncreaseValueTime += Time.deltaTime;

        if (spawnTime > spawnDelay)
        {
            Spawn();
            spawnTime = 0;
        }

        if (enemyIncreaseValueTime >= 10f)
        {
            foreach (GameObject enemy in enemyPrefabs)
            {
                enemy.GetComponent<Enemy>().IncreaseValue();
            }
            enemyIncreaseValueTime = 0;
        }
    }

    private void Spawn()
    {
        if (isSingleSpawn)
        {
            Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)], 
                new Vector3(Random.Range(-spawnPositionX, spawnPositionX), spawnPositionY, 0), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], 
                    new Vector3(Random.Range(-spawnPositionX, spawnPositionX), spawnPositionY, 0), Quaternion.identity);
            }
        }
    }

    private void StartSpawn()
    {
        foreach (GameObject enemy in enemyPrefabs)
        {
            enemy.GetComponent<Enemy>().InitValue();
        }

        isPlaying = true;
    }

    private void StopSpawn()
    {
        isPlaying = false;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameOver", StopSpawn);
        EventManager.StopListening("GameStart", StartSpawn);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameOver", StopSpawn);
        EventManager.StopListening("GameStart", StartSpawn);
    }
}
