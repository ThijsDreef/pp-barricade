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

    public FieldOccupierType myType {get; protected set; }

    public abstract void EnableHighlight(bool enabled, int color);
    /// <summary> all fieldOccupiers must implement this function to move to fields </summary>
    public abstract void MoveToField(Field nextField, Action callback);
}