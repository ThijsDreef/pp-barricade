using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateContainer {

    /// <summary> generates a container to put sertain objects in </summary>
    public static void Generate(string name, GameObject container, Transform parent, int hierarchyIndex) {
        container.name = name;
        container.transform.parent = parent;
        container.transform.SetSiblingIndex(hierarchyIndex);
        container.transform.localPosition = Vector3.zero;
        container.transform.localScale = Vector3.one;
    }
}
