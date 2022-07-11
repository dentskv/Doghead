using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public void PlayHandler()
    {
        SceneManager.LoadScene(4);
    }
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
