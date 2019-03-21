using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : FieldOccupier {
    [SerializeField]
    private Field nextField;
    [SerializeField]
    private float movementSpeed = 0;
    [SerializeField]
    private float yOffset = .55f;
    [SerializeField]
    private AnimationController animController;
    public AnimationController AnimController { get { return animController; } set { AnimController = value; } }

    [SerializeField]
    private GameObject highlighterObject;
    [SerializeField] private ParticleSystemRenderer[] childParticleSystem;

    public int Type { get; protected set; }
    private const float EPESILON = .00001f;

    [SerializeField]
    private Material[] selectionMaterialBase;
    [SerializeField]
    private Material[] selectionMaterialUpper;
    [SerializeField]
    private Material[] selectionMaterialBlob;

    public Action<Pawn> onSelect;
    public Action StartMoving;
    public Action StartAttack;
    public Action StopMoving;
    
    //TODO make action
    public bool lastmove = false;
    private void Start() {
        myType = FieldOccupierType.PLAYER;
        animController = GetComponent<AnimationController>();
    }

    /// <summary>starts a coroutine that makes the GameObject move to nextField.</summary>
    public override void MoveToField(Field nextField, Action callback) {
        StartCoroutine(MoveStep(nextField, callback));
    }

    public override void EnableHighlight(bool enabled, int color) {
        SetHighLightColors(color);
        selectable = enabled;
        if(enabled == true) highlighterObject.gameObject.SetActive(true);
        else highlighterObject.gameObject.SetActive(false);
    }

    public void SetHighLightColors(int currentColor) {
        childParticleSystem[0].material = selectionMaterialBase[currentColor];
        childParticleSystem[1].material = selectionMaterialUpper[currentColor];
        childParticleSystem[2].material = selectionMaterialBlob[currentColor]; 
    }

    private void OnMouseDown() {
        if (selectable) {
            onSelect?.Invoke(this);
            SoundManager.Instance.PlaySound("Select");
        } else if (currentField) {
            SoundManager.Instance.PlaySound("Select");
            currentField.OnMouseDown();
        }
    }
    
    /// <summary>coroutine for iterating current GameObject position and rotation to nextField position and moving direction.</summary>
    private IEnumerator MoveStep(Field nextField, Action callback) {
        StartMoving?.Invoke();
        Vector3 target = new Vector3(nextField.transform.position.x, nextField.transform.position.y + yOffset, nextField.transform.position.z);
        do {
            transform.position =  Vector3.MoveTowards(transform.position, target, (movementSpeed * Time.deltaTime));
            transform.LookAt(target);
            yield return new WaitForEndOfFrame();
            SoundManager.Instance.PlaySound("Walk");
        } while (Vector3.Distance(target, transform.position) >= EPESILON);
        if (currentField && currentField.onField == this) currentField.onField = null;
        currentField = nextField;
        transform.eulerAngles = Vector3.zero;
        callback?.Invoke();
        if (lastmove) {
            StopMoving();
        }
    }
}