using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
         [SerializeField] private Transform Container;
         [SerializeField] private Bullet Prefab;
         [SerializeField] private Transform WorldTransform;
         [SerializeField] private LevelBounds LevelBounds;

        private readonly Queue<Bullet> _mBulletPool = new();
        private readonly HashSet<Bullet> _mActiveBullets = new();
        private readonly List<Bullet> _mCache = new();
        
        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(Prefab, Container);
                _mBulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            _mCache.Clear();
            _mCache.AddRange(_mActiveBullets);

            for (int i = 0, count = _mCache.Count; i < count; i++)
            {
                var bullet = _mCache[i];
                if (!LevelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            if (_mBulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(WorldTransform);
            }
            else
            {
                bullet = Instantiate(Prefab, WorldTransform);
            }

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);
            
            if (_mActiveBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_mActiveBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(Container);
                _mBulletPool.Enqueue(bullet);
            }
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}