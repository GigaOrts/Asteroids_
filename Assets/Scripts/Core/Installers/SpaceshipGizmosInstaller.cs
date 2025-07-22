using Zenject;

namespace Core.Installers
{
    public class SpaceshipGizmosInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SpaceshipGizmos>().AsSingle();
        }
    }
}