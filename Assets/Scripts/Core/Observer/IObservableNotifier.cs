namespace Core.Scripts.CoreScripts.Observer
{
    public interface IObservableNotifier<in TData>
    {
        void Notify(TData data);
    }
}