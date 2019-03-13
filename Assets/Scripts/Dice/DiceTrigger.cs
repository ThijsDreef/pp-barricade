using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceTrigger : MonoBehaviour
{
    public int diceSideNumber;
    public Action<int> onDiceNumberSet;

    /// Sends through data on wich side it landed. 
    public void OnTriggerEnter() {
        onDiceNumberSet(diceSideNumber);
        SoundManager.Instance.PlaySound("Dice");
    }
}
