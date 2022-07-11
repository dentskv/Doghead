using System;
using CoreScripts.StateMachine;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Scripts.StateMachine
{
    public abstract class ViewController<TState> : ViewControllerBase, IInitializable where TState : Enum
    {
        public abstract TState ViewState { get; }

        public virtual void Activated()
        {
            
        }
        
        public virtual void DeActivated()
        {
            
        }

        public virtual void Initialize()
        {
            
        }
    }
}