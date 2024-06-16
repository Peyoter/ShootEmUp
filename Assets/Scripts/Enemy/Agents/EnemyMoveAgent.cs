using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached { get; private set; }

        [SerializeField] private MoveComponent MoveComponent;

        private Vector2 _destination;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        private void FixedUpdate()
        {
            if (IsReached)
            {
                return;
            }
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            MoveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}