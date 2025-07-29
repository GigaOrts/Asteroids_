using Core;
using Zenject;

public class SpaceshipInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SpaceshipPhysics>().AsSingle();
        Container.Bind<SpaceshipInput>().AsSingle();
        Container.Bind<SpaceshipPresentation>().AsSingle();
    }
}