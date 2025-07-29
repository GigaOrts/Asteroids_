using Core;
using UnityEngine;
using Zenject;

public class AsteroidGizmosInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AsteroidGizmos>().AsTransient();
    }
}