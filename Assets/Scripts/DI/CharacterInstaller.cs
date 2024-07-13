using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private GameObject characterTarget;

        public override void InstallBindings()
        {
            Container
                .Bind<Unit>()
                .FromComponentOn(gameObject)
                .AsSingle();

            Container
                .Bind<PlayerAttackAgent>()
                .AsSingle()
                .WithArguments(characterTarget);

            Container
                .Bind<PlayerController>()
                .AsSingle()
                .NonLazy();
        }
    }
}