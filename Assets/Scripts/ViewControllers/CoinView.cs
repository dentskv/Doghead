using Controllers;
using Core.Scripts.CoreScripts.Observer;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour, IObservableNotifier<CoinsController.CoinsData>
{
    [Inject] IDataObservable<CoinsController.CoinsData> observable;
    [Inject] private CoinsController coinsController;

    [SerializeField] private TMP_Text coinText;

    private IDisposable _disposable;

    private void Awake()
    {
        _disposable = observable.Subscribe(this);
    }

    private void Start()
    {
        coinText.text = coinsController.GetAmount.ToString();
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }

    public void Notify(CoinsController.CoinsData data)
    {
        coinText.text = data.amount.ToString();
    }
}
