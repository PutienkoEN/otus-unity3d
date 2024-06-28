namespace ShootEmUp
{
    public interface IGameUpdateListener : IGameListener
    {
        public void OnUpdate(float deltaTime);
    }
}