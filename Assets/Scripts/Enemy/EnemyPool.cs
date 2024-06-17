using System.Collections.Generic;
using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")] [SerializeField] private EnemyPositions EnemyPositions;

        [SerializeField] private GameObject Character;

        [SerializeField] private Transform WorldTransform;

        [Header("Pool")] [SerializeField] private Transform Container;

        [SerializeField] private GameObject EnemyPref;

        private readonly Queue<GameObject> _enemyPool = new();

        [SerializeField] private BulletFactory BulletFactory;

        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(EnemyPref, Container);
                _enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(WorldTransform);

            var spawnPosition = EnemyPositions.GetRandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            var attackPosition = EnemyPositions.GetRandomAttackPosition();
            enemy.GetComponent<ShootComponent>();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(Character);
            return enemy;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(Container);
            _enemyPool.Enqueue(enemy);
        }
    }
}