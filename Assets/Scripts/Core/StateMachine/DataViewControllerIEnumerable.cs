using System;
using System.Collections.Generic;
using Core.Scripts.CoreScripts.Observer;
using Core.Scripts.StateMachine;
using Zenject;

namespace CoreScripts.StateMachine
{
    public abstract class DataViewControllerIEnumerable<TState, TData> : 
        ViewController<TState>, IObservableNotifierEnumerable<TData> where TState : Enum
    {
        [Inject] private IDataObservableEnumerable<TData> observable;
        
        private List<TData> lastData;
        private IDisposable disposable;
        
        protected List<TData> LastData 
        {
            get => lastData;
            private set => lastData = value;
        }
        
        protected abstract void UpdateView(List<TData> data);

        protected virtual void OnDestroy()
        {
            disposable.Dispose();
        }
        
        void IObservableNotifierEnumerable<TData>.Notify(List<TData> data)
        {
            LastData = data;
            
            UpdateView(LastData);
        }
        
        public override void Initialize()
        {
            disposable = observable.Subscribe(this);
        }

        public virtual void AddNotification(TData data)
        {
            
        }

        public virtual void RemoveNotification(TData data)
        {
            
        }
    }
}