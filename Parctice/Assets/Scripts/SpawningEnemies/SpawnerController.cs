using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Transform spawner;
    //
    PlayerMovement player;
    //
    Transform[] spawners;
    public GameObject enemy;
    public int maxEnemies;
    public int enemiesInWave;
    int killedEnemies;
    int currentEnemies;
    public int waveCooldown;
    bool enemyIsSpawning;

    public int maxWaves;
    int currentWave;
    bool inWave;
    int timeToWave;
    bool waveTimer;
    bool changewave;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spawners = new Transform[spawner.childCount];
        int i = 0;
        timeToWave = waveCooldown;
        currentWave = 1;
        inWave = true;

        foreach (Transform spawn in spawner)
        {
            spawners[i++] = spawn;  
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!inWave && !waveTimer && currentWave <= maxWaves)
            StartCoroutine(StartWaveCooldown());

        if (maxEnemies > currentEnemies && !enemyIsSpawning && enemiesInWave - killedEnemies > currentEnemies && inWave)
            StartCoroutine(EnemySpawner());

        if (killedEnemies == enemiesInWave)
        {
            if (inWave) currentWave++;
            inWave = false; 
            
        }
        if (timeToWave == 0)
        {
            NewWave();
        }
    }

    void CreateEnemy(Transform spawn)
    {
        Instantiate(enemy, spawn.position, spawn.rotation);
    }

    IEnumerator EnemySpawner()
    {
        enemyIsSpawning = true;
        yield return new WaitForSeconds(2f);
        int index = Random.Range(0, spawners.Length);
        CreateEnemy(spawners[index]);
        currentEnemies++;
        enemyIsSpawning = false;
    }

    public void MinusCurEnemy()
    {
        currentEnemies--;
    }

    public void AddKilledEnemy()
    {
        killedEnemies++;
        // Debug.Log(killedEnemies);
    }

    IEnumerator StartWaveCooldown()
    {
        waveTimer = true;
        Debug.Log(timeToWave);
        timeToWave--;
        waveTimer = false;
        changewave = true;
        yield return new WaitForSeconds(1f);
    }
    
    void NewWave()
    {
        if (changewave)
        {
            enemiesInWave *= 2;
            killedEnemies = 0;
            timeToWave = waveCooldown;
            inWave = true;
            player.waveSum += 1;
            changewave = false;
        }

    }
}
