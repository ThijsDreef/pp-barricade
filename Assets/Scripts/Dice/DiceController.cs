using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceController : MonoBehaviour
{
    private int diceRoll;
    private bool isThrown = false;
    private DiceTrigger[] diceTriggers;
    [SerializeField] 
    private int upForce = 600, forwardForce = 600;
    private bool queue = false;
    private const float START_DISSOLVE_POINT = 0f;
    private float currentDissolve;
    public DiceData diceData;
    public Rigidbody rb;
    public Action<int> onDiceRollFinish;
    public new MeshRenderer renderer;
    public Material dissolveMaterial;
    public Transform spawnLocation;

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

    /// <summary> Respawns and dissolves the dice </summary>
    public void Respawn() {
        currentDissolve = START_DISSOLVE_POINT;
        isThrown = false;
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve() {
         if(currentDissolve > -0.75f && !isThrown) {
            yield return new WaitForEndOfFrame();
            currentDissolve -= 0.01f;
            dissolveMaterial.SetFloat("_DissolveY", currentDissolve);
            StartCoroutine(Dissolve());
         } else {
            if(queue) {
                queue = false;
                Roll(diceData);
            }
         }
    }

    // Checks on wich side the dice lands and what value is coppled with it.
    private void RollCheck() {
        rb.useGravity = false;
        diceRoll = diceData.diceNumbers[diceSideNumber];
        onDiceRollFinish?.Invoke(diceRoll);
        isThrown = false;
    }

    private void setRoll(int i) {
        diceSideNumber = i;
    }

    /// <summary> Activate to roll the dice with the given dice data, if still in progress it will stay in a queue. </summary>
    public void Roll(DiceData diceData) {
        renderer.material = diceData.material;
        dissolveMaterial = diceData.material;
        this.diceData = diceData;
        if(!isThrown) {
            isThrown = true;
            Move();
        } else { queue = true; }        
    }

    // Moves the game object to the field and rotates it randomly.
    private void Move() {
        StopAllCoroutines();
        transform.position = spawnLocation.position;
        rb.useGravity = true;
        rb.AddTorque(UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0,500), UnityEngine.Random.Range(0,500));
        rb.AddForce(Vector3.up * upForce + Vector3.back * forwardForce);
        dissolveMaterial.SetFloat("_DissolveY", START_DISSOLVE_POINT);
    }
}