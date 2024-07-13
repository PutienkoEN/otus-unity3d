using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private UnitConfig unitConfig;

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
        }
    }
}