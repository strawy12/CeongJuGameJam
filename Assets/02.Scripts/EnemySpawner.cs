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
    private bool isGameOver = false;
    private void Start()
    {
        EventManager.StartListening("GameOver", StopSpawn);
    }

    private void Update()
    {
        if (isGameOver)
            return;

        spawnTime += Time.deltaTime;
        enemyIncreaseValueTime += Time.deltaTime;

        if (spawnTime > spawnDelay)
        {
            Spawn();
            spawnTime = 0;
        }

        if (enemyIncreaseValueTime >= 600f)
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
            Vector2 pos = new Vector3(Random.Range(-spawnPositionX, spawnPositionX), spawnPositionY, 0);
            var enemy = PoolManager.Inst.Pop(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name) as Enemy;
            enemy.transform.position = pos;

            enemy.InitValue();
        }
        else
        {
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                Vector2 pos = new Vector3(Random.Range(-spawnPositionX, spawnPositionX), spawnPositionY, 0);
                var enemy = PoolManager.Inst.Pop(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name) as Enemy;
                enemy.transform.position = pos;

                enemy.InitValue();
            }
        }
    }

    private void StopSpawn()
    {
        isGameOver = true;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("GameOver", StopSpawn);
    }

    private void OnApplicationQuit()
    {
        EventManager.StopListening("GameOver", StopSpawn);
    }
}
