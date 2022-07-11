using System;
using Core.Scripts.CoreScripts.Observer;
using Core.Scripts.StateMachine;
using Controllers;
using TMPro;
using UnityEngine;

namespace ViewControllers
{
    public class GamePlayStateMachine : ViewController<MainViewController.MainStates>, IObservableNotifier<CoinsController.CoinsData>
    {

        [SerializeField] private TMP_Text coinsAmountText;

        private IDisposable disposable;

        public override MainViewController.MainStates ViewState => MainViewController.MainStates.Main;

        public void Notify(CoinsController.CoinsData data)
        {
            coinsAmountText.text = data.amount.ToString();
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    } 
}
