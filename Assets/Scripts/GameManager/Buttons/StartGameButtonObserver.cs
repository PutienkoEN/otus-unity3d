using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.Buttons
{
    public class StartGameButtonObserver : MonoBehaviour
    {
        private Button button;
        private GameManager gameManager;

        private void Awake()
        {
            button = GetComponent<Button>();
            gameManager = FindObjectOfType<GameManager>();

            button.onClick.AddListener(gameManager.StartGame);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(gameManager.StartGame);
        }
    }
}