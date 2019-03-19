using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    public List<GameObject> Menus;
    public Transform parent;
    public Material blur;

    private GameObject menusContainer;
    private int previousMenu, currentMenu = 0;

    // setup menu container and set the first menu as default menu
    private void Start() {
        ToggleBlur(true);
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
        previousMenu = currentMenu;
        currentMenu = menuID;
        foreach (var menu in Menus) {
            if(menu != null) {
                menu.SetActive(false);
            }
        }

        if (Menus[currentMenu] != null) {
            Menus[currentMenu].SetActive(true);
        } else {
            Menus[0].SetActive(true);
        }
    }

    public void Back() {
        EnableMenu(previousMenu);
    }

    public void ToggleBlur(bool status)
    {
        StartCoroutine(StartBlur(status));
    }

    private IEnumerator StartBlur(bool status) {
        float currentBlur = blur.GetFloat("_Size");
        if(status) {
            while(currentBlur < 2) {
                yield return new WaitForEndOfFrame();
                currentBlur += 0.1f;
                blur.SetFloat("_Size", currentBlur);
            }
        } else {
            while(currentBlur > 0) {
                yield return new WaitForEndOfFrame();
                currentBlur += -0.1f;
                blur.SetFloat("_Size", currentBlur);
            }
            yield return null;
        }
    }

    private void AdjustBlur(float currentBlur, float modifier)
    {
        print(currentBlur);
        
    }
}
