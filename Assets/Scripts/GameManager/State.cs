using System.Collections.Generic;

namespace ShootEmUp
{
    public class State
    {
        private readonly HashSet<GameState> allowedStates;

        public State(HashSet<GameState> allowedStates)
        {
            this.allowedStates = allowedStates;
        }

        public bool IsNotAllowedTransition(GameState gameState)
        {
            return !allowedStates.Contains(gameState);
        }
    }
}