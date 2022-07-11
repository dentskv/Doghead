using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Scripts.Utils;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Core.Scripts.StateMachine
{
    public class StateMachine<TState> : IStateMachine<TState>, IInitializable where TState : Enum
    {
        private bool inTransition = false;
        private TState startState;
        private TState previewsState;
        private Transform viewsParent;
        private readonly StateMachineQueue<TState> lastStates;
        protected readonly List<ViewController<TState>> viewControllers;
        protected readonly Dictionary<TState, List<Action>> subscribers = new Dictionary<TState, List<Action>>();
        protected Dictionary<TState, StateParams> viewsParams = new Dictionary<TState, StateParams>();
        public TState CurrentState { get; private set; }
        public event Action<TState> StateChanged;

        public StateMachine(TState startState, Transform viewsParent)
        {
            CurrentState = this.startState = startState;
            this.viewsParent = viewsParent;
            lastStates = new StateMachineQueue<TState>();
            lastStates.EndPeek(startState);
            viewControllers = new List<ViewController<TState>>();
        }

        ~StateMachine()
        {
            viewControllers.Clear();
            subscribers.Clear();
            viewsParams.Clear();
            previewsState = default;
            CurrentState = default;
        }

        public void Initialize()
        {
            ValidateViews();

            foreach (var controller in viewControllers)
            {
                controller.gameObject.SetActive(false);
            }

            InternalFire(CurrentState);
            
            Debug.LogWarning($"State Machine {nameof(TState)} started with state : {CurrentState.ToString()}");
        }

        private void ValidateViews()
        {
            foreach (var view in viewsParent.GetComponentsInChildren<ViewController<TState>>(true))
            {
                if (!view)
                {
                    continue;
                }

                view.Initialize();
                viewControllers.Add(view);

                var state = view.ViewState;

                Subscribe(state, BaseViewSubscription);
            }

            SetupParams();
        }

        protected virtual void BaseViewSubscription()
        {
            
        }

        private async void InternalFire(TState state)
        {
            if (inTransition)
            {
                return;
            }

            inTransition = true;
            
            inTransition = false;
            
            if (subscribers.ContainsKey(state))
            {
                subscribers[state].ForEach(f => f?.Invoke());
            }

            lastStates.EndPeek(state);

            StateChanged?.Invoke(state);

            ViewsActivation();
        }

        private void ViewsActivation()
        {
            foreach (var viewController in viewControllers)
            {
                if (Equals(viewController.ViewState, CurrentState))
                {
                    viewController.Activated();
                    viewController.gameObject.SetActive(true);
                }
                else
                {
                    if (viewsParams.ContainsKey(viewController.ViewState))
                    {
                        var param = viewsParams[viewController.ViewState];

                        if (param.Modal)
                        {
                            viewController.Activated();
                            viewController.gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        viewController.DeActivated();
                        viewController.gameObject.SetActive(false);
                    }
                }
            }
        }

        protected virtual void SetupParams()
        {

        }

        public virtual void Fire(TState state)
        {
            Debug.Log($"Firing : {state.ToString()}");
            if (CurrentState.Equals(state))
            {
                return;
            }

            previewsState = CurrentState;
            CurrentState = state;

            InternalFire(CurrentState);
        }

        public void PreviewsStateFire()
        {

            var state = lastStates.Enqueue();
            Debug.Log($"Firing : {state.ToString()}");
            Fire(state);
        }

        public virtual void Subscribe(TState state, Action callBack)
        {
            if (!subscribers.ContainsKey(state))
            {
                subscribers.Add(state, new List<Action>());
            }

            subscribers[state].Add(callBack);
        }

        public void ResetState()
        {
            Fire(startState);
        }

        public void ResetState(TState state)
        {
            Fire(state);
        }

        public void UnSubscribe(TState state, Action callBack)
        {
            if (!subscribers.ContainsKey(state))
            {
                return;
            }

            if (!subscribers[state].Contains(callBack))
            {
                return;
            }

            subscribers[state].Remove(callBack);
        }
    }
}