using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] SpawnPositions;

        [SerializeField] private Transform[] AttackPositions;

        public Transform GetRandomSpawnPosition()
        {
            return GetRandomTransform(SpawnPositions);
        }

        public Transform GetRandomAttackPosition()
        {
            return GetRandomTransform(AttackPositions);
        }

        private Transform GetRandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}