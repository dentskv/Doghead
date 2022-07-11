using UnityEngine;
using ViewControllers;
using Zenject;

namespace DefaultNamespace
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform parent;

        public override void InstallBindings()
        {
            Container.Bind<GunStatesController>().AsSingle();
            Container.BindInterfacesTo<MainStateMachine>()
                    .AsSingle()
                    .WithArguments(MainViewController.MainStates.Main, parent)
                    .NonLazy();
            
        }
    }
}
