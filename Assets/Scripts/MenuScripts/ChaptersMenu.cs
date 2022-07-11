using UnityEngine;

public class ChaptersMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevelMenu;
    [SerializeField] private GameObject shopMenu;

    public void BackHandler()
    {
        SwitchMenu(mainMenu);
    }

    public void ShopHandler()
    {
        SwitchMenu(shopMenu);
    }

    public void StartHandler()
    {
        SwitchMenu(selectLevelMenu);
    }

    private void SwitchMenu(GameObject menu)
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
