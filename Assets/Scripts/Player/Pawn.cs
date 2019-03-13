using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : FieldOccupier {
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private bool moving;
    [SerializeField]
    private Field nextField;
    [SerializeField]
    private float step = 2f;
    private float yOffset = .55f;

    public int Type { get; protected set; }

    /// <summary>starts a coroutine that makes the GameObject move to nextField.</summary>
    public override void MoveToField(Field nextField, Action callback) {
        StartCoroutine(MoveStep(nextField, callback));
    }

    public override void EnableHighlight(bool enabled, Color color) {
        selectable = enabled;
        gameObject.GetComponent<Renderer>().material.color = color;
        if (!enabled) gameObject.GetComponent<Renderer>().material.color = originalColor;
    }

    /// <summary>coroutine for iterating current GameObject position and rotation to nextField position and moving direction.</summary>
    private IEnumerator MoveStep(Field nextField, Action callback) {
        Vector3 target = new Vector3(nextField.transform.position.x, nextField.transform.position.y + yOffset, nextField.transform.position.z);
        do {
            transform.position =  Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
            transform.LookAt(target);
            yield return new WaitForEndOfFrame();
        } while (transform.position != target);
        callback?.Invoke();
        currentField = nextField;
        transform.eulerAngles = Vector3.zero;
    }
}
