using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    public List<Vector3> enemySpawns = new List<Vector3>();
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> aliveEnemies = new List<GameObject>();
    public int waveDuration;

    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    [SerializeField] GetAbilities getAbilities;

    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            getAbilities.GenerateAbilities();
        }
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], enemySpawns[Random.Range(0, enemySpawns.Count)], Quaternion.identity);
                aliveEnemies.Add(enemiesToSpawn[0]);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else if(aliveEnemies.Count <= 0)
            {
                if(currWave == 20)
                {
                    SceneManager.LoadScene("Victory");
                }
                else
                {
                    waveTimer = 0;
                    getAbilities.GenerateAbilities();
                    currWave++;
                    GenerateWave();
                }
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 8;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}