using Controllers;
using Core.Scripts.CoreScripts.Observer;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MainMenuViewController : MonoBehaviour
{
    public class MainMenu : MonoBehaviour, IObservableNotifier<CoinsController.CoinsData>
    {
        [Inject] private IDataObservable<CoinsController.CoinsData> observer;
        [Inject] private CoinsController coinsController;
        
        [SerializeField] private TMP_Text scoreText;

        private IDisposable disposable;

        private void Awake()
        {
            disposable = observer.Subscribe(this);
            scoreText.text = coinsController.GetAmount.ToString();
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }

        public void Notify(CoinsController.CoinsData data)
        {
            scoreText.text = data.amount.ToString();
        }
    }

}
