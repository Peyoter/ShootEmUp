using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveController : MonoBehaviour
    {
        public bool HasReachedGoal { get; private set; }

        [SerializeField] private MoveComponent MoveComponent;

        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            HasReachedGoal = false;
        }

        private void FixedUpdate()
        {
            if (HasReachedGoal)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                HasReachedGoal = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            MoveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}