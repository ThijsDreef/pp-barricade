using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    [SerializeField]
    private List<Field> nextFields;
    [SerializeField]
    private List<Field> previousFields;  
    public FieldOccupier onField;

    public void HighLight(bool on) {

    }
}
