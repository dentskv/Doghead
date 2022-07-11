using System;
using System.Collections.Generic;
using Core.Scripts.CoreScripts.Observer;
using Interfaces;
using ScriptableObjects;
using Zenject;

namespace Controllers
{
    public class CoinsController : IDataObservable<CoinsController.CoinsData>, IInitializable, IDisposable, IUpdateCoins
    {
        public CoinsData Data { get; protected set; }

        [Inject] private CoinsPreset preset;
        [Inject] private AdvertisementManager advertisementManager;

        private readonly List<IObservableNotifier<CoinsData>> observables = new List<IObservableNotifier<CoinsData>>();

        public IDisposable Subscribe(IObservableNotifier<CoinsData> observer)
        {
            if(!observables.Contains(observer))
            {
                if(Data != null)
                {
                    observer.Notify(Data);
                }

                observables.Add(observer);
            }
            return new Unsubscriber<CoinsData>(observables, observer);
        }

        public void Initialize()
        {
            Data = preset.CoinsData;
            advertisementManager.OnEarnedReward += AddCoins;
        }

        public void Dispose()
        {
            advertisementManager.OnEarnedReward -= AddCoins;
        }
        
        public void CollectCoins(float coin)
        {
            Data.amount += coin;

            observables.ForEach(f => f?.Notify(Data));
        }

        public void AddCoins(float coins)
        {
            Data.amount += coins;
            
            observables.ForEach(f => f?.Notify(Data));
        }
        
        public float GetAmount
        {
            get => Data.amount;
        }

        public void SpendCoins(float price)
        {
            Data.amount -= price;
            
            observables.ForEach(f => f?.Notify(Data));
        }

        [Serializable]
        public class CoinsData
        {
            public float amount;
        }
    }
}
