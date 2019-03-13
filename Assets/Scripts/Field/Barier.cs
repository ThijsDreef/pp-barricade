using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : FieldOccupier {

    private ParticleSystem highlighter;

    public override void MoveToField(Field nextField, Action callback) {
        currentField = nextField;
        nextField.onField = this;
        this.transform.position = nextField.transform.position;
        callback();
    }

    public override void EnableHighlight(bool enabled, Color color) {
        selectable = enabled;
        if(enabled == true) highlighter.Play();
        else highlighter.Stop();
    }
}
