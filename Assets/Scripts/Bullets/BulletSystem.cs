using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
         [SerializeField] private Transform Container;
         [SerializeField] private Bullet Prefab;
         [SerializeField] private Transform WorldTransform;
         [SerializeField] private LevelBounds LevelBounds;

        private readonly Queue<Bullet> mBulletPool = new();
        private readonly HashSet<Bullet> mActiveBullets = new();
        private readonly List<Bullet> mCache = new();
        
        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(Prefab, Container);
                mBulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            mCache.Clear();
            mCache.AddRange(mActiveBullets);

            for (int i = 0, count = mCache.Count; i < count; i++)
            {
                var bullet = mCache[i];
                if (!LevelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            if (mBulletPool.TryDequeue(out var bullet))
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
            
            if (mActiveBullets.Add(bullet))
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
            if (mActiveBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(Container);
                mBulletPool.Enqueue(bullet);
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