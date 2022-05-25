using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject buttonsParent;
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
    
    //public void displayInGameSettings(bool choice){}
}
