using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float spawnDelay;
    private float spawnTime;

    public float enemyIncreaseValueTime;
    public float delayDecreaseValueTime;

    public bool isSingleSpawn = true;
    private bool isPlaying = false;

    private float _defaultSpawnDelay;

    private void Start()
    {
        _defaultSpawnDelay = spawnDelay;
        EventManager.StartListening("GameOver", StopSpawn);
        EventManager.StartListening("GameStart", StartSpawn);
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        spawnTime += Time.deltaTime;
        enemyIncreaseValueTime += Time.deltaTime;
        delayDecreaseValueTime += Time.deltaTime;

        if (spawnTime > spawnDelay)
        {
            Spawn();
            spawnTime = 0;
        }

        if (enemyIncreaseValueTime >= 60f)
        {
            foreach (GameObject enemy in enemyPrefabs)
            {
                enemy.GetComponent<Enemy>().IncreaseValue();
            }
            enemyIncreaseValueTime = 0;
        }

        if(spawnDelay > 0.1f && delayDecreaseValueTime >= 120f)
        {
            spawnDelay -= 0.1f;
        }


    }

    private void Spawn()
    {
        Vector2 minPos = Utils.MainCam.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 maxPos = Utils.MainCam.ViewportToWorldPoint(new Vector2(1f, 1f));

        if (isSingleSpawn)
        {
            Vector2 pos = new Vector3(Random.Range(minPos.x, maxPos.x), maxPos.y, 0);
            var enemy = PoolManager.Inst.Pop(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name) as Enemy;
            enemy.transform.position = pos;

            enemy.InitValue();
        }
        else
        {
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                Vector2 pos = new Vector3(Random.Range(minPos.x, maxPos.x), maxPos.y, 0);
                var enemy = PoolManager.Inst.Pop(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name) as Enemy;
                enemy.transform.position = pos;

                enemy.InitValue();
            }
        }
    }

    private void StartSpawn()
    {
        spawnDelay = _defaultSpawnDelay;

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
