using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class LevelProvider : MonoBehaviour
    {
        // Objects
        public Unit characterObject;

        // Prefabs
        public Unit enemyPrefab;
        public Bullet bulletPrefab;

        // Containers
        public Transform worldContainer;
        public Transform disabledContainerForBullets;
        public Transform disabledContainerForEnemies;
    }
}