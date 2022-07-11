using Core.Scripts.StateMachine;
using UnityEngine;
using ViewControllers;

namespace DefaultNamespace
{
    public class MainStateMachine : StateMachine<MainViewController.MainStates>
    {
        public MainStateMachine(MainViewController.MainStates startState, Transform viewsParent) : base(startState, viewsParent)
        {
        }
    }
}
