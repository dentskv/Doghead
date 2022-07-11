using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Scripts.StateMachine
{
    public class ButtonStateMachine<TState, TStateMachine> : MonoBehaviour where TState : Enum 
                                                                           where TStateMachine : StateMachine<TState>
    {
        [SerializeField] private Button targetButton;
        [SerializeField] private TState targetState;

        [Inject] private TStateMachine stateMachine;

        private void Awake()
        {
            targetButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            stateMachine.Fire(targetState);
        }
    }
}
