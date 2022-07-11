using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseHandler);
    }

    public void CloseHandler()
    {
        options.SetActive(false);
    }
}
