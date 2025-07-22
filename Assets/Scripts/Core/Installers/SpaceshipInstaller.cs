using Core;
using Zenject;

public class SpaceshipInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SpaceshipMovement>().AsSingle();
        Container.Bind<SpaceshipInput>().AsSingle();
        Container.Bind<SpaceshipPresentation>().AsSingle();
    }
}