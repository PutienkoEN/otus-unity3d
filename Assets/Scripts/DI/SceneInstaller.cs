using Components;
using ShootEmUp;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container
                .Bind<InputManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<DamageComponent>()
                .AsSingle();
        }
    }
}