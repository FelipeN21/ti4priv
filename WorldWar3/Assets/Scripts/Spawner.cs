using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{ int i = 0;
    public GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(toSpawn,transform.position,transform.rotation);   
    }

    void newVoid()
    {
       // Instantiate(toSpawn, transform.position, transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {

       // Invoke("newVoid", 3);
    }
}
