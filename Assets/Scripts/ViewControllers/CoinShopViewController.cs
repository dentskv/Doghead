using Core.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using ScriptableObjects;
using ViewControllers;
using Zenject;

public class CoinShopViewController : ViewController<MainViewController.MainStates>
{
    public override MainViewController.MainStates ViewState => MainViewController.MainStates.CoinShop;
    
    [SerializeField] private Button backButton;
    [SerializeField] private Button[] buyButtons;
    
    [Inject] private IAPManager managerIAP;
    [Inject] private IStateMachine<MainViewController.MainStates> stateMachine;
    [Inject] private PurchasePreset purchasePreset;

    public override void Initialize()
    {
        backButton.onClick.AddListener(BackButtonClick);
        buyButtons[0].onClick.AddListener(() => BuyButtonClick(purchasePreset.purchases[0].purchaseName));
        buyButtons[1].onClick.AddListener(() => BuyButtonClick(purchasePreset.purchases[1].purchaseName));
    }

    private void BuyButtonClick(string s)
    {
        managerIAP.BuyProduct(s);
    }
    
    private void BackButtonClick()
    {
        stateMachine.Fire(MainViewController.MainStates.Main);
    }
}
