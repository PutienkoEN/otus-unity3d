using Components;
using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Unit character;
        [SerializeField] private GameObject characterTarget;

        [SerializeField] private BulletPool bulletPool;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            PlayerConfiguration();
            BulletConfiguration();
        }

        private void BulletConfiguration()
        {
            Container
                .Bind<BulletPool>()
                .FromInstance(bulletPool)
                .AsSingle();

            Container
                .Bind<DamageComponent>()
                .AsSingle();

            Container
                .Bind<BulletFactory>()
                .AsSingle();
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