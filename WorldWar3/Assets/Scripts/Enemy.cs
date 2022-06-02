using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthPoints;
    public int points;
    public float speed;
    public float turnSpeed;
    public GameObject waypointParent;
    private Vector3[] waypoints;
    private int waypointIndex = 0;
    public UIManager UIMg;

    private static int[,] Base = new int[,]{{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },
                                            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },
                                            {0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
                                            { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, };
    public static int[,] Matrix = new int[,]{{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },
                                            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },
                                            {0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
                                            { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                            {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, };


    private void Start()
    {
        obtainPath(waypointParent);


        int[] AC = Dijkstra.GFG.M(Matrix);


        Matrix = Dijkstra.MudarMatriz(Matrix);



        AC = Dijkstra.GFG.M(Matrix);

        int C2 = AC[4] + AC[5] + AC[6] + AC[7] + AC[8] + AC[9];
        int C1 = AC[11] + AC[12];

        // Debug.Log(AC[11]);
        // Debug.Log(AC[12]);
        //  Debug.Log(AC[12]+AC[11]);
            Debug.Log(C1 + " CAMINHO CURTO");
             Debug.Log(C2 + " CAMINHO LONGO eu tambem nao ");
    

        for (int i = 0; i < 12; i++)
        {

            for (int z = 0; z < 12; z++)
            {
                Matrix[i, z] = Base[i, z];
            }

        }

        //Matrix = Base;
        //Debug.Log(AC[3]);
    }

    private void Update()
    {
        executePath();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.reducePoints();
            UIMg.updateStats();
            Destroy(gameObject);
        }
    }

    public void obtainPath(GameObject parent)
    {
        int index = 0;
        waypoints = new Vector3[parent.transform.childCount];
        foreach (Transform child in parent.transform)
        {
            waypoints[index++] = child.transform.position;
        }
    }

    public void executePath()
    {
        Vector3 waypoint = waypoints[waypointIndex];
        if ((transform.position - waypoint).magnitude > 0.1f)
        {
            Vector3 movementDirection = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * speed);
            Vector3 turnDirection = new Vector3(movementDirection.x - transform.position.x, movementDirection.y - transform.position.y, movementDirection.z - transform.position.z);
            transform.position = movementDirection;
            Quaternion toRotation = Quaternion.LookRotation(turnDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else if (waypointIndex < waypoints.Length - 1)
        {
            waypointIndex++;
        }
    }

    public void takeDamage(float amount)
    {
        healthPoints -= amount;

        if (healthPoints <= 0)
        {
            GameManager.addScore(points);
            UIMg.updateStats();
            Destroy(gameObject);
        }
    }


     float resultado(int c, int torre)
    {
        float rangeTor = 32f;
        float turnSpeed = 10f;
        float fireRate = 5f;
        float fireCoutdown = 1f;
        return c * (torre * ((rangeTor / turnSpeed) * (fireRate / fireCoutdown)));
    }
}
