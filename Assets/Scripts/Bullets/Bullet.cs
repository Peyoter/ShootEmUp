using System;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        [NonSerialized] public int Damage;

        [SerializeField] private new Rigidbody2D Rigidbody2D;

        [SerializeField] private SpriteRenderer SpriteRenderer;

        public void OnCollisionEnter2D()
        {
            OnCollisionEntered?.Invoke(this);
        }

        public void SetVelocity(Vector2 velocity)
        {
            Rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            SpriteRenderer.color = color;
        }
    }
}