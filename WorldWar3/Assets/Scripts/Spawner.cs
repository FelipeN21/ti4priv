using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int i = 0;
    public GameObject enemyPrefab;
    public GameObject spawnerWaypoint;
    public UIManager UIMg;
    public float frequency = 5f;
    public enum SPAWN_RATE_ENUM { SLOW = 0, NORMAL = 1, FAST = 2, SUPERFAST = 3 };
    public static float spawnRate = 1f;

 


    void Start()
    {

        StartCoroutine(spawnRoutine());
      
    }

    void Update()
    {

        

       

    }

    IEnumerator spawnRoutine()
    {
        while (true)
        {
            spawn();

            yield return new WaitForSeconds(frequency / spawnRate);
        }
    }

    public static void setSpawnerRate(SPAWN_RATE_ENUM phase)
    {
        switch (phase)
        {
            case SPAWN_RATE_ENUM.SLOW:
                spawnRate = 0.3f;
                break;
            case SPAWN_RATE_ENUM.NORMAL:
                spawnRate = 1.0f;
                break;
            case SPAWN_RATE_ENUM.FAST:
                spawnRate = 1.5f;
                break;
            case SPAWN_RATE_ENUM.SUPERFAST:
                spawnRate = 2.0f;
                break;
            default:
                spawnRate = 1.0f;
                break;
        }
    }

    void spawn()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.waypointParent = spawnerWaypoint;
            enemy.UIMg = UIMg;
        }

    }
}
