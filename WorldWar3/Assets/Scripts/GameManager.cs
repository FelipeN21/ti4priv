using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int playerCurrency = 100;
    public static int playerHP = 100;
    public static int playerScore = 0;

    public static void reducePoints(){
        playerHP = playerHP - 1;
    }

    public static void addScore(int amount){
        playerScore = playerScore + amount;
    }
}
