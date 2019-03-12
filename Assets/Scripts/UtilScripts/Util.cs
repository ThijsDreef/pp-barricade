using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Util {
    /// <summary>returns a field from a raycast from camera to mouse clicked object.</summary>
    static public Field GetFieldAtMouse() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red);
            if (hit.transform.gameObject.GetComponent<Field>() != null) {
                return hit.transform.gameObject.GetComponent<Field>();
            }
        }
        return null;
    }
};