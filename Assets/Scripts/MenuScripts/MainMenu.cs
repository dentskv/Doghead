using Controllers;
using Core.Scripts.CoreScripts.Observer;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour, IObservableNotifier<CoinsController.CoinsData>
{
    [Inject] private IDataObservable<CoinsController.CoinsData> observer;
    [Inject] private CoinsController coinsController;

    [SerializeField] private GameObject options;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button openOptions;

    private IDisposable disposable;

    private void Awake()
    {
        disposable = observer.Subscribe(this);
    }

    private void Start()
    {
        openOptions.onClick.AddListener(OpenOptions);
        scoreText.text = coinsController.GetAmount.ToString();
    }

    private void OpenOptions()
    {
        options.SetActive(true);
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
