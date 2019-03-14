using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Text winText;

    private void Start() {
        SoundManager.Instance.PlaySound("Victory");
        print("Thijs connect deze functie even met de game controller");
        //winText.text = "Player " + GameController.Instance.currentPlayer + " Wins!";
    }
}
