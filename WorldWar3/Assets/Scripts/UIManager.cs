using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject buttonsParent;
    public GameObject AIChosen;
    public GameObject removeButtonsParent;
    public GameObject rightPanelText;
    public GameObject WinLossUI;
    public static GameObject warningText;
    
    private void Start() {
        updateStats();
        warningText = GameManager.getChildWithName(rightPanelText, "warning");
    }

    private void Update() {
        updateTime();
    }

    public void displayVictoryScreen(){
        GameObject WinTab = GameManager.getChildWithName(WinLossUI, "Win");
        GameObject textScore = GameManager.getChildWithName(WinTab, "scoreText");
        WinTab.SetActive(true);
        textScore.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.playerScore;
        GameManager.pauseGame(true);
    }

    public void displayDefeatScreen(){
        GameObject LossTab = GameManager.getChildWithName(WinLossUI, "Loss");
        GameObject textScore = GameManager.getChildWithName(LossTab, "scoreText");
        LossTab.SetActive(true);
        textScore.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.playerScore;
        GameManager.pauseGame(true);
    }

    public void unclick(){
        displayTurretPlacements(false);
        displayTurretRemove(false);
    }
    public void showAI(bool djikstra){
        if (djikstra)
        {
            AIChosen.GetComponent<TextMeshProUGUI>().text = "IA: " + "Djikstra";
        }
        else
        {
            AIChosen.GetComponent<TextMeshProUGUI>().text = "IA: " + "A*";
        }
    }

    public void displayTurretPlacements(bool choice){
        int index = 0;
        foreach (Transform button in buttonsParent.transform){
            if (!TurretManager.placed[index++])
            {
                button.gameObject.SetActive(choice);
            }else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void displayTurretRemove(bool choice){
        int index = 0;
        foreach (Transform button in removeButtonsParent.transform){
            if (TurretManager.placed[index++])
            {
                button.gameObject.SetActive(choice);
            }else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void updateStats(){
        GameObject currencyText = GameManager.getChildWithName(rightPanelText, "Currency");
        GameObject scoreText =    GameManager.getChildWithName(rightPanelText,   "Points");
        GameObject livesText =    GameManager.getChildWithName(rightPanelText,    "Lives");

        if (GameManager.playerHP > 0) {
            currencyText.GetComponent<TextMeshProUGUI>().text = "Currency: " + GameManager.playerCurrency;
            scoreText.GetComponent<TextMeshProUGUI>().text =    "Score: " + GameManager.playerScore;
            livesText.GetComponent<TextMeshProUGUI>().text =    "Lives: " + GameManager.playerHP;
        }
        else {
            displayDefeatScreen();
        }
    }

    public void updateTime(){
        GameObject timerText = GameManager.getChildWithName(rightPanelText, "Timer");

        if (GameManager.playerHP > 0 && GameManager.timer > 0) {
            timerText.GetComponent<TextMeshProUGUI>().text =    "Time: " + string.Format("{0, 0:f2}", GameManager.timer);
        }
    }

    public static IEnumerator noCurrency(){
        warningText.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.SetActive(false);
    }

    //public void displayInGameSettings(bool choice){}
}
