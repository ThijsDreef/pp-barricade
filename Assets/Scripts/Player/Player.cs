using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : FieldOccupier {
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private bool moving;
    [SerializeField]
    private Field nextField;
    private float step = .1f;
    private float yOffset = .55f;
    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            MoveToField(nextField, null);
        }
        SelectNextField();
    }

    public override void MoveToField(Field nextField, Action callback) {
        StartCoroutine(MoveStep(nextField, callback));
    }

    private IEnumerator MoveStep(Field nextField, Action callback) {
        Vector3 target = new Vector3(nextField.transform.position.x, nextField.transform.position.y + yOffset, nextField.transform.position.z);
        do {
            transform.position =  Vector3.MoveTowards(transform.position, target, step);
            transform.LookAt(target);
            yield return null;
        } while (transform.position != target);
        callback?.Invoke();
        currentField = nextField;
        transform.eulerAngles = Vector3.zero;
    }

    private void SelectNextField() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red);
                if (hit.transform.gameObject.GetComponent<Field>() != null) {
                    nextField = hit.transform.gameObject.GetComponent<Field>();
                } 
            }
        }
    }
}
