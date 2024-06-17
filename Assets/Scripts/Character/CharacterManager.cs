using System;
using Bullets;
using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterManager : MonoBehaviour
    {
        
        [SerializeField] private GameManager.GameManager GameManager;
        
        [SerializeField] private BulletConfig BulletConfig;
        [SerializeField] private BulletPool  BulletPool;
        public BulletFactory BulletFactory;

        private void Awake()
        {
            BulletFactory = new BulletFactory(BulletConfig, BulletPool);
        }

        private void OnEnable()
        {
            GetComponent<DeathObserverComponent>().OnDeath += OnCharacterDeath;
        }

        private void OnDisable()
        {
            GetComponent<DeathObserverComponent>().OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => GameManager.FinishGame();
    }
}