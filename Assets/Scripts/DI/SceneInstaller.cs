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
                .Bind<LevelProvider>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<InputManager>()
                .AsSingle();

            Container
                .Bind<DamageComponent>()
                .AsSingle();
        }
    }
}