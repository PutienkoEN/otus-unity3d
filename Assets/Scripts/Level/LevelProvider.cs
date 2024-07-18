using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class LevelProvider : MonoBehaviour
    {
        [Header("Character")] public Unit characterObject;

        [Header("Prefabs")] public Unit enemyPrefab;
        public Bullet bulletPrefab;

        [Header("Buttons")] public Button startButton;
        public Button pauseButton;
        public Button resumeButton;

        [Header("Containers")] public Transform worldContainer;
        public Transform disabledContainerForBullets;
        public Transform disabledContainerForEnemies;
    }
}