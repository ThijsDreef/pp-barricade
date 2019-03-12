using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceController : MonoBehaviour
{
    private int diceRoll;
    private bool isThrown = false;
    [SerializeField] private int upForce = 600, forwardForce = 600;

    public DiceData diceData;
    public Rigidbody rb;
    public DiceTrigger[] diceTriggers;
    public Action<int> onDiceRollFinish;

    [HideInInspector] public int diceSideNumber;

    private void Start() {
        diceTriggers = GetComponentsInChildren<DiceTrigger>();
        for (int i = 0; i < diceTriggers.Length; i++) {
            diceTriggers[i].onDiceNumberSet += setRoll;
        }
    }

    private void FixedUpdate() {
        if(rb.IsSleeping() && isThrown) {
            RollCheck();
        }
    }

    // Checks on wich side the dice lands and what value is coppled with it.
    private void RollCheck() {
        diceRoll = diceData.diceNumbers[diceSideNumber];
        onDiceRollFinish?.Invoke(diceRoll);
        isThrown = false;
        rb.useGravity = false;
    }

    private void setRoll(int i) {
        diceSideNumber = i;
    }

    /// <summary> Activate to roll the dice with the given dice data. </summary>
    public void Roll(DiceData diceData) {
        if(!isThrown) {
            rb.useGravity = true;
            isThrown = true;
            this.diceData = diceData;
            Move();
        }
        
    }

    // Moves the game object to the field and rotates it randomly.
    private void Move() {
        rb.AddTorque(UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0,500));
        rb.AddForce(Vector3.up * upForce + Vector3.back * forwardForce);
    }
}
