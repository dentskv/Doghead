using System;
using System.Collections.Generic;

namespace Core.Scripts.CoreScripts.Observer
{
    public class Unsubscriber<TData> : IDisposable
    {
        private List<IObservableNotifier<TData>> observers;
        private IObservableNotifier<TData> observer;

        public Unsubscriber(List<IObservableNotifier<TData>> observers, IObservableNotifier<TData> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
                observers.Remove(observer);
        }
    }
    
    public class UnsubscriberEnumerable<TData> : IDisposable
    {
        private List<IObservableNotifierEnumerable<TData>> observers;
        private IObservableNotifierEnumerable<TData> observer;

        public UnsubscriberEnumerable(List<IObservableNotifierEnumerable<TData>> observers, IObservableNotifierEnumerable<TData> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
                observers.Remove(observer);
        }
    }
}