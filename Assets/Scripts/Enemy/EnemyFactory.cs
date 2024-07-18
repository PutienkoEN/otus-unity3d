using Zenject;

namespace ShootEmUp
{
    public class EnemyFactory : IFactory<Unit>
    {
        private readonly Unit enemyPrefab;
        private readonly DiContainer diContainer;

        [Inject]
        public EnemyFactory(LevelProvider levelProvider, DiContainer diContainer)
        {
            enemyPrefab = levelProvider.enemyPrefab;
            this.diContainer = diContainer;
        }

        public Unit Create()
        {
            return diContainer.InstantiatePrefabForComponent<Unit>(enemyPrefab);
        }
    }
}