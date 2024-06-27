using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        private GameState gameState;

        public void StartGame()
        {
            Debug.Log("Game started!");
        }

        public void FinishGame()
        {
            Debug.Log("Game finished!");

            // if (gameState == GameState.InProgress)
            // {
            //     Debug.Log("Game over!");
            //     Time.timeScale = 0;
            //     gameState = GameState.Finished;
            // }
        }

        public void PauseGame()
        {
            Debug.Log("Game paused!");
        }
    }

    internal enum GameState
    {
        InProgress,
        Finished
    }
}