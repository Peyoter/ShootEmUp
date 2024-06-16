using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D Rigidbody2D;

        [SerializeField] private float Speed = 5.0f;

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = Rigidbody2D.position + vector * Speed;
            Rigidbody2D.MovePosition(nextPosition);
        }
    }
}