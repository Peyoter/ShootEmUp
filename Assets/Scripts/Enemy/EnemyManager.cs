using System.Collections;
using System.Collections.Generic;
using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool EnemyPool;
        [SerializeField] private BulletPool BulletPool;
        [SerializeField] private BulletConfig BulletConfig;
        private BulletFactory _bulletFactory;

        private readonly HashSet<GameObject> _mActiveEnemies = new();

        private void Awake()
        {
            _bulletFactory = new BulletFactory(BulletConfig, BulletPool);
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = EnemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (_mActiveEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().HpEmpty += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                    }
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_mActiveEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().HpEmpty -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                EnemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletFactory.CreateBullet(position, direction);
        }
    }
}