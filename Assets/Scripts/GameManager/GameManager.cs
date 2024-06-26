using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        private GameState gameState;

        public void FinishGame()
        {
            if (gameState == GameState.InProgress)
            {
                Debug.Log("Game over!");
                Time.timeScale = 0;
                gameState = GameState.Finished;
            }
        }
    }

    internal enum GameState
    {
        InProgress,
        Finished
    }
}