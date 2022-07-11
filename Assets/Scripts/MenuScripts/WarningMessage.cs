using UnityEngine;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
    [SerializeField] private Button confirmButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(ConfirmButtonClick);
    }

    private void ConfirmButtonClick()
    {
        gameObject.SetActive(false);
    }
}
