using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    public Transform spawner;

    Transform[] spawners;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        spawners = new Transform[spawner.childCount];
        int i = 0;

        foreach (Transform spawn in spawner)
        {
            Debug.Log(spawn.position);
            spawners[i++] = spawn;  
        }

        foreach (Transform spawn in spawners)
        {
            CreateEnemy(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy(Transform spawn)
    {
        Instantiate(enemy, spawn.position, spawn.rotation);
    }
}
