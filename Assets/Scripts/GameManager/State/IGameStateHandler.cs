namespace ShootEmUp
{
    public interface IGameStateHandler
    {
        public bool IsAllowed();
        public void ChangeState();
    }
}