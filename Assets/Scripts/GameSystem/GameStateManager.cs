using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace GameSystem
{
    public class GameStateManager : MonoBehaviour
    {
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _lateUpdateListeners = new();

        private enum GameStateEnum
        {
            START,
            FINISH,
            PAUSE,
            RESUME,
            OFF,
        }

        [SerializeField, ReadOnly]
        private GameStateEnum GameState;
        
        private void Awake()
        {
            IGameListener.OnRegister += AddListener;
            GameState = GameStateEnum.OFF;
        }
        
        private void OnDestroy()
        {
            IGameListener.OnRegister -= AddListener;
            GameState = GameStateEnum.FINISH;
        }

        private void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is IGameUpdateListener gameUpdateListener)
            {
                _updateListeners.Add(gameUpdateListener);
            }
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
            
            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        private void FixedUpdate()
        {
            if (GameState != GameStateEnum.START) return;
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(deltaTime);
            }
        }
        
        private void LateUpdate()
        {
            if (GameState != GameStateEnum.START) return;
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }
        private void Update()
        {
            if (GameState != GameStateEnum.START) return;
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        [Button]
        public void StartGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }

            GameState = GameStateEnum.START;
        }
        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameFinishLister gameStartListener)
                {
                    gameStartListener.OnFinishGame();
                }
            }
            GameState = GameStateEnum.FINISH;
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGamePauseListener gameStartListener)
                {
                    gameStartListener.OnPauseGame();
                }
            }
            GameState = GameStateEnum.PAUSE;
        }
        
        [Button]
        public void Resume()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameResumeListener gameStartListener)
                {
                    gameStartListener.OnResumeGame();
                }
            }
            GameState = GameStateEnum.RESUME;
        }
    }
}