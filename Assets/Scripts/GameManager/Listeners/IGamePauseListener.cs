namespace ShootEmUp
{
    public interface IGamePauseListener : IGameListener
    {
        public void OnGamePause();
    }
}