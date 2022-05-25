using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurret : MonoBehaviour
{
    public GameObject[] turretPrefabs;
    public Transform[] foundations;
    public static TurretManager.Turret turret = 0;
    public int foundationNum;
    public void InstanceTurret() {
        Vector3 pos = foundations[foundationNum].position - new Vector3(0f,0.5f,0f);
        Quaternion rot = Quaternion.Euler(new Vector3(0f,-90f,0f));
        GameObject turretMap = (GameObject)Instantiate(turretPrefabs[(int)turret], pos, rot);
    }
}
