using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Unit character;
        [SerializeField] private GameObject characterTarget;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            PlayerConfiguration();
        }

        private void PlayerConfiguration()
        {
            Container
                .Bind<InputManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Unit>()
                .FromInstance(character)
                .AsSingle()
                .WhenInjectedInto(typeof(PlayerAttackAgent), typeof(PlayerController));


            Container
                .Bind<GameObject>()
                .FromInstance(characterTarget)
                .AsSingle()
                .WhenInjectedInto(typeof(PlayerAttackAgent));

            Container
                .Bind<PlayerAttackAgent>()
                .AsSingle();

            Container
                .Bind<PlayerController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}