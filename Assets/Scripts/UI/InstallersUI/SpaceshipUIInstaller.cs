using Zenject;

namespace UI
{
    public class SpaceshipUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SpaceshipUI>().AsSingle();
        }
    }
}