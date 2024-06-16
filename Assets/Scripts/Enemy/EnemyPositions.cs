using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPositions : MonoBehaviour
    {
         [SerializeField]
        private Transform[] SpawnPositions;

         [SerializeField]
        private Transform[] AttackPositions;

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(SpawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(AttackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}