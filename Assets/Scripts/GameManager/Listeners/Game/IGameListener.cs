using System;

namespace ShootEmUp
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;

        public static void Register(IGameListener gameListener)
        {
            OnRegister?.Invoke(gameListener);
        }
    }
}