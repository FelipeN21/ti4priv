using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static bool[] placed;
    public PlaceTurret.Turret selectedTurret;

    private void Start() {
        for (int i = 0; i < placed.Length; i++){
            placed[i] = false;
        }
    }


}
