using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    public List<GameObject> Menus;
    public Transform parent;

    private GameObject menusContainer;

    // setup menu container and set the first menu as default menu
    private void Start() {
        menusContainer = new GameObject();
        EnableMenu(0);
        GenerateContainer.Generate("MenusContainer", menusContainer, parent, 1);

        foreach (GameObject menu in Menus) {
            if (menu != null) {
                menu.transform.SetParent(menusContainer.transform);
            }
        }
    }

    /// <summary> enables the menu with the right ID </summary>
    public void EnableMenu(int menuID) {
        foreach (var menu in Menus) {
            if(menu != null) {
                menu.SetActive(false);
            }
        }

        if (Menus[menuID] != null) {
            Menus[menuID].SetActive(true);
        }
        else {
            Menus[0].SetActive(true);
        }
    }
}
