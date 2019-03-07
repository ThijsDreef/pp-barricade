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
    private Field currentField;

    private FieldOccupierType myType;

    /// <summary> all fieldOccupiers must implement this function to move to fields </summary>
    public abstract void MoveToField(Field nextField, Action callback);
}