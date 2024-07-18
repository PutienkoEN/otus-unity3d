using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class CharacterInstaller : MonoInstaller
    {
        // Left here, since very specific use case. Probably won't be used anywhere else.
        [SerializeField] private GameObject characterTarget;

        public override void InstallBindings()
        {
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