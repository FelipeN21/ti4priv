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

    private void Start() {
        obtainPath(waypointParent);
    }

    private void Update() {
        executePath();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish"))
        {
            GameManager.reducePoints();
            UIMg.updateStats();
            Destroy(gameObject);
        }
    }

    public void obtainPath(GameObject parent){
        int index = 0;
        waypoints = new Vector3[parent.transform.childCount];
        foreach (Transform child in parent.transform)
        {
            waypoints[index++] = child.transform.position;
        }
    }

    public void executePath() {
        Vector3 waypoint = waypoints[waypointIndex];
        if ((transform.position - waypoint).magnitude > 0.1f){
            Vector3 movementDirection = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * speed);
            Vector3 turnDirection = new Vector3(movementDirection.x - transform.position.x, movementDirection.y - transform.position.y, movementDirection.z - transform.position.z);
            transform.position = movementDirection;
            Quaternion toRotation = Quaternion.LookRotation(turnDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);   
        }else if(waypointIndex < waypoints.Length-1){
            waypointIndex++;
        }
    }

    public void takeDamage(float amount)
    {
        healthPoints -= amount;

        if (healthPoints <= 0){
            GameManager.addScore(points);
            UIMg.updateStats();
            Destroy(gameObject);
        }
    }
}
