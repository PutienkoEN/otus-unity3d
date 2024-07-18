using ShootEmUp.Buttons;
using Zenject;

namespace DI
{
    public class GameButtonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ButtonController>()
                .AsSingle()
                .NonLazy();
        }
    }
}