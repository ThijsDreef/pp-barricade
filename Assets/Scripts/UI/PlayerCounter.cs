using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    private int playerAmount = 2;
    public Text amountTextField;
    [SerializeField]
    private GameController gameController = null;
    
    public void ModifyPlayerAmount(int amount) {
        
        playerAmount += amount;

        if(playerAmount > 4) {
            playerAmount = 4;
        }  
        else if(playerAmount < 2) {
            playerAmount = 2;
        }
        amountTextField.text = playerAmount.ToString();
    }

    public void confirm() {
        gameController.StartGame(playerAmount);
    }
}
