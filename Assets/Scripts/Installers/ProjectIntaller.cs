using Controllers;
using ScriptableObjects;
using Managers;
using Zenject;

public class ProjectIntaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CoinsController>().AsSingle();
        Container.Bind<CoinsPreset>().FromScriptableObjectResource("Coin").AsTransient();
        Container.Bind<StarPreset>().FromScriptableObjectResource("Stars").AsTransient();
        Container.Bind<GunPreset>().FromScriptableObjectResource("Gun").AsTransient();
        Container.Bind<SoundPreset>().FromScriptableObjectResource("Sound").AsTransient();
        Container.Bind<IAPManager>().AsSingle();
        Container.Bind<AdvertisementManager>().AsSingle();
        Container.Bind<PurchasePreset>().FromScriptableObjectResource("Purchases").AsTransient();
    }
}
