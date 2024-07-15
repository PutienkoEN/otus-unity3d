namespace ShootEmUp
{
    public interface IGameStateObserver<T> where T : IGameListener
    {
        void Observe(T listener);
    }
}