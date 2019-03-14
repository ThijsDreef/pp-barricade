using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    [SerializeField]
    private List<Field> neighbours;
    public FieldOccupier onField;
    [SerializeField]
    private GameObject selectionParticle;

    public void HighLight(bool on) {

    }
    public List<Field> Neighbours { get {return neighbours; } set { Neighbours = value; } }
}
