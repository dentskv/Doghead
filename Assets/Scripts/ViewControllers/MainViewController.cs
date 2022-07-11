using System;
using Core.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Managers;

namespace ViewControllers
{
    public class MainViewController : ViewController<MainViewController.MainStates>
    {
        public override MainStates ViewState => MainStates.Main;

        [Inject] private IStateMachine<MainStates> stateMachine;
        [Inject] private AdvertisementManager advertisementManager;
        [Inject] private IAPManager manager;

        [SerializeField] private Button advertisementButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button plusButton;

        private void Awake()
        {
            advertisementManager.Initialize();
            manager.Initialize();
        }

        private void OnDestroy()
        {
            manager.Dispose();
        }

        public override void Initialize()
        {
            playButton.onClick.AddListener(PlayButtonClick);
            shopButton.onClick.AddListener(ShopButtonClick);
            plusButton.onClick.AddListener(CoinShopButtonClick);
            advertisementButton.onClick.AddListener(AdvertisementButtonClick);
        }

        private void PlayButtonClick()
        {
            stateMachine.Fire(MainStates.Chapters);
        }

        private void CoinShopButtonClick()
        {
            stateMachine.Fire(MainStates.CoinShop);
        }
        
        private void ShopButtonClick()
        {
            stateMachine.Fire(MainStates.Shop);
        }

        private void AdvertisementButtonClick()
        {
            advertisementManager.UserChoseToWatchAd();
        }
        
        public enum MainStates
        {
            Main,
            SelectLevel,
            Shop,
            Chapters,
            CoinShop
        }
    }
}
