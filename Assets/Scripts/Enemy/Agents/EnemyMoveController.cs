using Components;
using GameSystem;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveController : MonoBehaviour, IGameFixedUpdateListener
    {
        public bool HasReachedGoal { get; private set; }

        [SerializeField] private MoveComponent MoveComponent;

        private Vector2 _destination;

        private void Awake()
        {
            IGameListener.Register(this);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            HasReachedGoal = false;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
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

            var direction = vector.normalized * fixedDeltaTime;
            MoveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}