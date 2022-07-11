using System;

namespace Core.Scripts.CoreScripts.Observer
{
    public interface IDataObservable<T>
    {
        IDisposable Subscribe(IObservableNotifier<T> observer);
    }
    
    public interface IDataObservableEnumerable<T>
    {
        IDisposable Subscribe(IObservableNotifierEnumerable<T> observer);
    }
}