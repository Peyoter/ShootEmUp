using System.Collections.Generic;
using GameSystem;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletPool : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private int InitialCount = 50;
        [SerializeField] private Transform Container;
        [SerializeField] private Bullet Prefab;
        [SerializeField] private Transform WorldTransform;
        [SerializeField] private LevelBounds LevelBounds;

        private readonly Queue<Bullet> _mBulletPool = new();
        private readonly HashSet<Bullet> _mActiveBullets = new();
        private readonly List<Bullet> _mCache = new();

        private void Awake()
        {
            IGameListener.Register(this);
            for (var i = 0; i < InitialCount; i++)
            {
                var bullet = Instantiate(Prefab, Container);
                _mBulletPool.Enqueue(bullet);
            }
        }

        public void OnFixedUpdate(float t)
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

        public void OnBulletCollision(Bullet bullet)
        {
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

        public Bullet GetBullet()
        {
            if (_mBulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(WorldTransform);
            }
            else
            {
                bullet = Instantiate(Prefab, WorldTransform);
            }

            return bullet;
        }

        public bool AddActiveBullet(Bullet bullet)
        {
            return _mActiveBullets.Add(bullet);
        }
    }
}