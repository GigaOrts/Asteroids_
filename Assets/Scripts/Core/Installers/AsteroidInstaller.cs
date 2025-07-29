using Core;
using UnityEngine;
using Zenject;

public class AsteroidInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AsteroidPhysics>().AsTransient();
        Container.Bind<AsteroidPresentation>().AsTransient();
    }
}