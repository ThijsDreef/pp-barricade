using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    [SerializeField]
    private List<Field> neighbours;
    public FieldOccupier onField;

    public void HighLight(bool on) {

    }
}
