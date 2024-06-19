using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using GameSystem;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour, IGameStartListener, IGamePauseListener
    {
        [SerializeField] private EnemyPool EnemyPool;

        private readonly HashSet<GameObject> _mActiveEnemies = new();
        
        [SerializeField] private int Cooldown = 1;

        private Coroutine _startCoroutine;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnStartGame()
        {
            _startCoroutine = StartCoroutine(OnStart());
        }

        public void OnPauseGame()
        {
            if (_startCoroutine != null)
            {
                StopCoroutine(_startCoroutine);    
            }
        }

        private IEnumerator OnStart()
        {
            while (true)
            {
                yield return new WaitForSeconds(Cooldown);
                var enemy = EnemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (_mActiveEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().HpEmpty += OnDestroyed;
                    }
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_mActiveEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().HpEmpty -= OnDestroyed;
                EnemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}