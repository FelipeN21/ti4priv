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
    // Start is called before the first frame update
    void Start()
    {
       
            InvokeRepeating("spawn", 5f, frequency);
        
        //Instantiate(toSpawn,transform.position,transform.rotation);   
    }

    void spawn()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, transform.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();

        if (enemy != null){
            enemy.waypointParent = spawnerWaypoint;
            enemy.UIMg = UIMg;
        }
        
    }
    // Update is called once per frame
    void Update()
    {

       // Invoke("newVoid", 3);
    }
}
