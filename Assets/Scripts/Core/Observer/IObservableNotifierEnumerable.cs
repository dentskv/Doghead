using System.Collections.Generic;

namespace Core.Scripts.CoreScripts.Observer
{
    public interface IObservableNotifierEnumerable<TData>
    {
        void Notify(List<TData> data);
        void AddNotification(TData data);
        void RemoveNotification(TData data);
    }
}