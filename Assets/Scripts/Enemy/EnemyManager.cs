using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Enemy
{
    
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool EnemyPool;

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