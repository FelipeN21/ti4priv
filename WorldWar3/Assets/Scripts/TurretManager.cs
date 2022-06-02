using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public UIManager UIMg;
    public static bool[] placed;
    public static Dictionary<int, GameObject> placedTurrets = new Dictionary<int, GameObject>();
    public GameObject[] turretPrefabs;
    public GameObject foundationsParents;
    public static Transform[] foundations;
    public static Turret turret = Turret.C75;
    public enum Turret {C75 = 0, KATYUSHA = 1, SHILKA = 2, T64 = 3};


    private void Start() {
        placed = new bool[20];
        for (int i = 0; i < placed.Length; i++){
            placed[i] = false;
        }
                
        int index = 0;
        foundations = new Transform[foundationsParents.transform.childCount];
        foreach (Transform child in foundationsParents.transform)
        {
            foundations[index++] = child;
        }
    }

    public void setTurret(int t){
        switch (t)
        {
            case 0:
                turret = Turret.C75;
            break;
            case 1:
                turret = Turret.KATYUSHA;
            break;
            case 2:
                turret = Turret.SHILKA;
            break;
            case 3:
                turret = Turret.T64;
            break;
        }
    }

    public int returnCost(Turret t){
        switch (t)
        {
            case Turret.C75:
                return 100;
            case Turret.KATYUSHA:
                return 500;
            case Turret.SHILKA:
                return 40;
            case Turret.T64:
                return 200;
        }
        return 99999;
    }

    public void tryPlacingTurret(int num){
        if (GameManager.playerCurrency - returnCost(turret) > 0)
        {
            GameManager.playerCurrency -= returnCost(turret);
            UIMg.updateStats();
            InstanceTurret(num);
        }
        else
        {            
            StartCoroutine(UIManager.noCurrency());
        }
    }
    public void deleteTurret(int num) {
        GameObject removedTurret = placedTurrets[num];
        placedTurrets.Remove(num);
        placed[num] = false;
        Object.Destroy(removedTurret);
    }

    public void InstanceTurret(int num) {
        placed[num] = true;
        Vector3 pos = foundations[num].position + new Vector3(0f,1f,0f);
        Quaternion rot = Quaternion.Euler(new Vector3(0f,-90f,0f));
        GameObject turretMap = (GameObject)Instantiate(turretPrefabs[(int)turret], pos, rot);
        placedTurrets.Add(num, turretMap);
    }

}
