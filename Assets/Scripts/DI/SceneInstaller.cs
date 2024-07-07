using ShootEmUp;
using UnityEngine;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        public const string CharacterTarget = "Character Target";
        public const string Character = "Character";

        [SerializeField] private Unit character;
        [SerializeField] private GameObject characterTarget;

        public override void InstallBindings()
        {
            Container
                .Bind<GameManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<InputManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Unit>()
                .WithId(Character)
                .FromInstance(character)
                .AsSingle();

            Container
                .Bind<GameObject>()
                .WithId(CharacterTarget)
                .FromInstance(characterTarget)
                .AsSingle();
            
            Container
                .Bind<PlayerAttackAgent>()
                .AsSingle();
        }
    }
}