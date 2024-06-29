using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.Buttons
{
    public class ResumeGameButtonObserver : MonoBehaviour
    {
        private Button button;
        private GameManager gameManager;

        private void Awake()
        {
            button = GetComponent<Button>();
            gameManager = FindObjectOfType<GameManager>();

            button.onClick.AddListener(gameManager.ResumeGame);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(gameManager.ResumeGame);
        }
    }
}