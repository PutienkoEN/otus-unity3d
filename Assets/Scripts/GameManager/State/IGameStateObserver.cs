namespace ShootEmUp
{
    public interface IGameStateObserver<in T> where T : IGameListener
    {
        void Observe(T listener);
    }
}