using System;

namespace GameSystem
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;
        public static event Action<IGameListener> OnUnRegister;

        public static void Register(IGameListener gameListener)
        {
            OnRegister?.Invoke(gameListener);
        }
        
        public static void UnRegister(IGameListener gameListener)
        {
            OnUnRegister?.Invoke(gameListener);
        }

    }
   
    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IGameFinishLister : IGameListener
    {
        void OnFinishGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}