using ShootEmUp.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DI
{
    public class GameButtonInstaller : MonoInstaller
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;

        public override void InstallBindings()
        {
            BindButton<StartGameButtonObserver>(startButton);
            BindButton<PauseGameButtonObserver>(pauseButton);
            BindButton<ResumeGameButtonObserver>(resumeButton);
        }

        private void BindButton<T>(Button button)
        {
            Container
                .Bind<T>()
                .AsSingle()
                .WithArguments(button)
                .NonLazy();
        }
    }
}