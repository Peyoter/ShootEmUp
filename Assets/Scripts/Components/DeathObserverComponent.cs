using System;
using GameSystem;
using UnityEngine;

namespace Components
{
    public class DeathObserverComponent : MonoBehaviour, IGameStartListener, IGameFinishLister
    {
        public event Action<GameObject> OnDeath;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject o)
        {
            OnDeath?.Invoke(o);
        }
    }
}