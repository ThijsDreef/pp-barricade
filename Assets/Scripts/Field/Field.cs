using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    [SerializeField]
    private List<Field> neighbours;

    [SerializeField]
    public int cost {get; private set; } = 1;
    public FieldOccupier onField;
    [SerializeField]
    private GameObject selectionParticle;

    private bool selected ;
    private Color originalColor;

  
 
    private void Start() {

        originalColor = GetComponentInChildren<Renderer>().material.color;
    }

    public void HighLight(bool on, Color highlightColor) {
        if (on) { 
            GetComponentInChildren<Renderer>().material.color = highlightColor;
            selected  = on;
        }
        else  {
            GetComponentInChildren<Renderer>().material.color = originalColor;
            selected = false;
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
