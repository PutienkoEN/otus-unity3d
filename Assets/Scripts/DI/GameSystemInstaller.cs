using ShootEmUp;
using Zenject;

namespace DI
{
    public class GameSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallStateHandlers();
            BindInterfaceAndSelfSingle<GameLifecycleManager>();

            Container
                .BindInterfacesAndSelfTo<LevelBackground>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<GameStartTimer>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<GameStateStorage>()
                .AsSingle();

            Container
                .Bind<GameStateManager>()
                .AsSingle();
        }

        private void InstallStateHandlers()
        {
            BindInterfaceAndSelfSingle<StartGameStateHandler>();
            BindInterfaceAndSelfSingle<PauseGameStateHandler>();
            BindInterfaceAndSelfSingle<ResumeGameStateHandler>();
            BindInterfaceAndSelfSingle<FinishGameStateHandler>();
        }

        private void BindInterfaceAndSelfSingle<T>()
        {
            Container
                .BindInterfacesAndSelfTo<T>()
                .AsSingle();
        }
    }
}