using System;
using Core.Scripts.CoreScripts.Observer;
using Core.Scripts.StateMachine;
using Zenject;

namespace CoreScripts.StateMachine
{
    /// <summary>
    /// TData - data from observer
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public abstract class DataViewController<TState, TData> : 
        ViewController<TState>, IObservableNotifier<TData> where TState : Enum
    {
        [Inject] private IDataObservable<TData> observable;
        
        private TData lastData;
        private IDisposable disposable;
        
        protected TData LastData 
        {
            get => lastData;
            private set => lastData = value;
        }
        
        protected abstract void UpdateView(TData data);

        protected virtual void OnDestroy()
        {
            disposable?.Dispose();
        }
        
        void IObservableNotifier<TData>.Notify(TData data)
        {
            LastData = data;
            
            UpdateView(LastData);
        }
        
        public override void Initialize()
        {
            disposable = observable.Subscribe(this);
        }
    }
}