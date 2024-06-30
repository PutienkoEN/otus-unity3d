using UnityEngine;

namespace ShootEmUp
{
    public class GameStateStorage : MonoBehaviour
    {
        private GameState gameState = GameState.None;

        public GameState GetCurrentState()
        {
            return gameState;
        }

        public void SetCurrentState(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}