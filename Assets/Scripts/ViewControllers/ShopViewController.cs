using Core.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ViewControllers
{
    public class ShopViewController : ViewController<MainViewController.MainStates>
    {
        public override MainViewController.MainStates ViewState => MainViewController.MainStates.Shop;

        [Inject] private IStateMachine<MainViewController.MainStates> stateMachine;

        [SerializeField] private Button backButton;

        public override void Initialize()
        {
            backButton.onClick.AddListener(BackButtonClick);
        }

        private void BackButtonClick()
        {
            stateMachine.Fire(MainViewController.MainStates.Main);
        }
    }
}

