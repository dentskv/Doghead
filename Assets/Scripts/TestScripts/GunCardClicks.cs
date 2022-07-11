using Controllers;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
using Zenject;

public class GunCardClicks : MonoBehaviour
{
    [Inject] private GunPreset gunPreset;
    [Inject] private CoinsController coinController;

    [SerializeField] private GameObject lockedCard;
    [SerializeField] private Button unlockButton;
    [SerializeField] private int price;

    private void Start()
    {
        unlockButton.onClick.AddListener(UnlockButtonClick);
    }

    private void UnlockButtonClick()
    {
        if (coinController.GetAmount >= price)
        {
            Debug.Log("UnlockButtonClick");
            coinController.SpendCoins(price);
        }
    }
}
