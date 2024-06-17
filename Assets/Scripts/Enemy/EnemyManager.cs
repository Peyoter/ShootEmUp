using System.Collections;
using System.Collections.Generic;
using Bullets;
using Common;
using Components;
using Enemy.Agents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool EnemyPool;

        [FormerlySerializedAs("BulletPuller")] [SerializeField] private BulletPool BulletPool;

        private readonly HashSet<GameObject> _mActiveEnemies = new();

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

        // Todo FireManager
        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            BulletPool.CreateBullets(new BulletPool.Args
            {
                PhysicsLayer = (int)PhysicsLayerEnum.ENEMY_BULLET,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}