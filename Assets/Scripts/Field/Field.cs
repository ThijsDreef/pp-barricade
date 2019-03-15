﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    [SerializeField]
    private List<Field> neighbours = new List<Field>();

    [SerializeField]
    public int cost {get; private set; } = 1;
    public FieldOccupier onField;
    [SerializeField]
    private GameObject selectionParticle = null;

    private bool selected ;

    public void HighLight(bool on, Color highlightColor) {
        if (on) { 
            selectionParticle.SetActive(on);
            selected  = on;
        }
        else  {
            selectionParticle.SetActive(on);
            selected = on;
        }
    }

    private void OnMouseDown() {
        if (selected ) GameController.Instance.SelectField(this);
    }

    public FieldOccupierType GetOnFieldType() {
        if (!onField) return FieldOccupierType.EMPTY;
        else return onField.myType;
    }
    public List<Field> Neighbours { get {return neighbours; } set { Neighbours = value; } }
}
