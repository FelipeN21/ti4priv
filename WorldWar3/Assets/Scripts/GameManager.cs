using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float globalClock = 0;
    public static float timer = 180f;
    public static int playerCurrency;
    public static int playerHP;
    public static int playerScore;
    public UIManager UIMg;

    private void Start() {
        resetGame();
        StartCoroutine(money());
    }

    private void Update() {
        globalClock += Time.deltaTime;
        timer -= Time.deltaTime;

        if (globalClock < 60f) {
            Spawner.setSpawnerRate(Spawner.SPAWN_RATE_ENUM.SLOW);
        }else if(globalClock < 120f){
            Spawner.setSpawnerRate(Spawner.SPAWN_RATE_ENUM.NORMAL);
        }else if(globalClock < 150f){
            Spawner.setSpawnerRate(Spawner.SPAWN_RATE_ENUM.FAST);
        }else if(globalClock < 180f){
            Spawner.setSpawnerRate(Spawner.SPAWN_RATE_ENUM.SUPERFAST);
        }else{
            UIMg.displayVictoryScreen();
        }
    }

    public static void resetGame(){
        playerCurrency = 100;
        playerHP = 100;
        playerScore = 0;
        timer = 180f;
        globalClock = 0;
        pauseGame(true);          
    }
    
    IEnumerator money()
    {
        while (true)
        {
            playerCurrency += 100;
            UIMg.updateStats();
            yield return new WaitForSeconds(5f);
        }
    }

    public static void pauseGame(bool pause){
        if (pause) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public static void flipPause(){
        if (Time.timeScale == 1) Time.timeScale = 0; else Time.timeScale = 1;
    }

    public static void setGameSpeed(float speed){
        Time.timeScale = speed;
    }

    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public static void ExitApplication()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public static GameObject getChildWithName(GameObject parent, string name) {
         Transform parentTranform = parent.transform;
         foreach (Transform t in parentTranform) if (t.gameObject.name == name) return t.gameObject;
         return null;
     }

    public static void reducePoints(){
        playerHP = playerHP - 1;
    }

    public static void addScore(int amount){
        playerScore = playerScore + amount;
    }
}
