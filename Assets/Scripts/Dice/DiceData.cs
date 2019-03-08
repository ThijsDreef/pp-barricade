using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceData", menuName = "Data/Dice")]
public class DiceData : ScriptableObject{
    public int[] diceNumbers;

    // Constructor for the default value's for the dice data.
    public DiceData(){
        diceNumbers = new int[6];
        for(int i = 0; i < diceNumbers.Length; i++){
            diceNumbers[i] = i + 1;
        }
    }
}
