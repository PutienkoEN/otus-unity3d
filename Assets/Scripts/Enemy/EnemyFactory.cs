using Zenject;

namespace ShootEmUp
{
    public class EnemyFactory : IFactory<Unit>
    {
        private readonly Unit enemyPrefab;
        private readonly DiContainer diContainer;

        [Inject]
        public EnemyFactory(Unit enemyPrefab, DiContainer diContainer)
        {
            this.enemyPrefab = enemyPrefab;
            this.diContainer = diContainer;
        }

        public Unit Create()
        {
            return diContainer.InstantiatePrefabForComponent<Unit>(enemyPrefab);
        }
    }
}