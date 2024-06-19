using Components;
using GameSystem;
using UnityEngine;

namespace Character
{
    public sealed class CharacterManager : MonoBehaviour, IGameStartListener, IGameFinishLister
    {
        [SerializeField] private GameStateManager GameStatus;

        public void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            GetComponent<DeathObserverComponent>().OnDeath += OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            GetComponent<DeathObserverComponent>().OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => GameStatus.FinishGame();
    }
}