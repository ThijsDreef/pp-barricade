using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Text winText;

    private void Start() {
        SoundManager.Instance.PlaySound("Victory");
        winText.text = "Player " + GameController.Instance.currentPlayer + " Wins!";
    }
}
