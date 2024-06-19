using System;
using GameSystem;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameFinishLister
    {
        public event Action<Bullet> OnCollisionEntered;

        [NonSerialized] public int Damage;

        [SerializeField] private new Rigidbody2D Rigidbody2D;

        [SerializeField] private SpriteRenderer SpriteRenderer;

        private Vector2 _prevVelocity;

        public void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnCollisionEnter2D()
        {
            OnCollisionEntered?.Invoke(this);
        }

        public void SetVelocity(Vector2 velocity)
        {
            Rigidbody2D.velocity = velocity;
            _prevVelocity = velocity;
        }

        public void OnPauseGame()
        {
            _prevVelocity = Rigidbody2D.velocity;
            Rigidbody2D.velocity = new Vector2(0, 0);
        }

        public void OnFinishGame()
        {
            _prevVelocity = Rigidbody2D.velocity;
            Rigidbody2D.velocity = new Vector2(0, 0);
        }

        public void OnStartGame()
        {
            enabled = true;
            Rigidbody2D.velocity = _prevVelocity;
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