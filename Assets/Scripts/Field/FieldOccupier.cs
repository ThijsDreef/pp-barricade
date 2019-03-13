using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldOccupierType {
    EMPTY,
    PLAYER,
    BARRICADE
}

public abstract class FieldOccupier : MonoBehaviour {

    private Field startField;

    [SerializeField]
    protected Field currentField;

    public bool selectable {get; protected set; }

    public Color originalColor {get; private set; }

    protected FieldOccupierType myType;

    private void Start() {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    public abstract void EnableHighlight(bool enabled, Color color);
    /// <summary> all fieldOccupiers must implement this function to move to fields </summary>
    public abstract void MoveToField(Field nextField, Action callback);
}