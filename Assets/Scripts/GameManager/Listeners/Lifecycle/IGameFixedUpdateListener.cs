namespace ShootEmUp
{
    public interface IGameFixedUpdateListener : IGameListener
    {
        public void OnFixedUpdate(float deltaTime);
    }
}