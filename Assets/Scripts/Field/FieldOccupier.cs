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

    public Field startField;

    [SerializeField]
    public Field currentField {get; protected set; }

    public bool selectable {get; protected set; }

    public Color originalColor {get; private set; }

    public FieldOccupierType myType {get; protected set; }

    private void Start() {
        originalColor = gameObject.GetComponentInChildren<Renderer>().material.color;
    }

    public abstract void EnableHighlight(bool enabled, Color color);
    /// <summary> all fieldOccupiers must implement this function to move to fields </summary>
    public abstract void MoveToField(Field nextField, Action callback);
}