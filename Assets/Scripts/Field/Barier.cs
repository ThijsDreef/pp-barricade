using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : FieldOccupier {

    private ParticleSystem highlighter = null;

    public void Start() {
        myType = FieldOccupierType.BARRICADE;
    }

    public override void MoveToField(Field nextField, Action callback) {
        currentField = nextField;
        transform.position = nextField.transform.position + new Vector3(0, 0.5f, 0);
        callback?.Invoke();
    }

    public override void EnableHighlight(bool enabled, int color) {
        selectable = enabled;
        if(enabled == true) highlighter.Play();
        else highlighter.Stop();
    }
}
