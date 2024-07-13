using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private Transform firingPoint;

        [SerializeField] private UnitConfig unitConfig;
        [SerializeField] private BulletConfig bulletConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<HitPointsComponent>()
                .AsSingle()
                .WithArguments(unitConfig.initialHealth);

            var characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
            Container
                .Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(characterRigidBody, unitConfig.speed);

            Container
                .Bind<TeamComponent>()
                .AsSingle()
                .WithArguments(unitConfig.team);

            Container
                .Bind<BulletConfig>()
                .FromInstance(bulletConfig)
                .AsSingle();

            Container
                .Bind<Transform>()
                .FromInstance(firingPoint)
                .AsSingle()
                .WhenInjectedInto(typeof(WeaponComponent));

             Container
                .Bind<WeaponComponent>()
                .AsSingle();
        }
    }
}