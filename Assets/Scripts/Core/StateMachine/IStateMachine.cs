using System;

namespace Core.Scripts.StateMachine
{
    public interface IStateMachine<TState> : IStateMachine
    {
        void Fire(TState state);
        TState CurrentState { get; }
        event Action<TState> StateChanged;
        void Subscribe(TState state, Action callBack);
        void ResetState();
        void ResetState(TState state);
    }

    public interface IStateMachine
    {
        void PreviewsStateFire();
    }
}