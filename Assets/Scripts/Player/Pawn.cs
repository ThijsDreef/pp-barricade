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
    private ParticleSystem highlighter = null;

    public int Type { get; protected set; }
    private const float EPESILON = .001f;

    public Action<Pawn> onSelect;
    public Action StartMoving;
    public Action StartAttack;
    public Action StopMoving;
        
    private void Start() {
        myType = FieldOccupierType.PLAYER;
    }

    /// <summary>starts a coroutine that makes the GameObject move to nextField.</summary>
    public override void MoveToField(Field nextField, Action callback) {
        StartCoroutine(MoveStep(nextField, callback));
    }

    public override void EnableHighlight(bool enabled, Color color) {
        selectable = enabled;
        if(enabled == true) highlighter.Play();
        else highlighter.Stop();
    }

    private void OnMouseDown() {
        if (selectable) onSelect?.Invoke(this);
    }
    
    /// <summary>coroutine for iterating current GameObject position and rotation to nextField position and moving direction.</summary>
    private IEnumerator MoveStep(Field nextField, Action callback) {
        StartMoving?.Invoke();
        Vector3 target = new Vector3(nextField.transform.position.x, nextField.transform.position.y + yOffset, nextField.transform.position.z);
        do {
            transform.position =  Vector3.MoveTowards(transform.position, target, (movementSpeed * Time.deltaTime));
            transform.LookAt(target);
            yield return new WaitForEndOfFrame();
        } while (Vector3.Distance(transform.position,target) >= EPESILON);
        currentField = nextField;
        transform.eulerAngles = Vector3.zero;
        callback?.Invoke();
        StopMoving?.Invoke();
    }
}