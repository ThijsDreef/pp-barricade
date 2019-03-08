using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : FieldOccupier {

    public override void MoveToField(Field nextField, Action callback) {
        currentField = nextField;
        nextField.onField = this;
        this.transform.position = nextField.transform.position;
        callback();
    }
}
