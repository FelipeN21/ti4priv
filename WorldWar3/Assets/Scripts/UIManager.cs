using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject buttonsParent;
    public GameObject currencyText;
    public GameObject scoreText;
    public GameObject livesText;
    
    private void Start() {
        updateStats();
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

    public void updateStats(){
        currencyText.GetComponent<TextMeshProUGUI>().text = "Currency: " + GameManager.playerCurrency;
        Debug.Log(GameManager.playerCurrency);
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.playerScore;
        Debug.Log(GameManager.playerScore);
        livesText.GetComponent<TextMeshProUGUI>().text = "Lives: " + GameManager.playerHP;
        Debug.Log(GameManager.playerHP);
    }

    //public void displayInGameSettings(bool choice){}
}
