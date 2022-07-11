using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    public void BackHandler()
    {
        SwitchMenu(mainMenu);
    }
    private void SwitchMenu(GameObject menu)
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
