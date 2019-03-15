using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : FieldOccupier {

    private ParticleSystem highlighter;

    public void Start() {
        myType = FieldOccupierType.BARRICADE;
    }

    public override void MoveToField(Field nextField, Action callback) {
        currentField = nextField;
        transform.position = nextField.transform.position;
        print("MOVING BARIER");
        callback?.Invoke();
    }

    public override void EnableHighlight(bool enabled, Color color) {
        selectable = enabled;
        if(enabled == true) highlighter.Play();
        else highlighter.Stop();
    }
}
