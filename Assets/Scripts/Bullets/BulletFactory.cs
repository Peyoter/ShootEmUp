using UnityEngine;

namespace Bullets
{
    public class BulletFactory
    {
     

        private BulletPool _bulletPool;
        private BulletConfig _bulletConfig;
        
        public BulletFactory(BulletConfig bulletConfig, BulletPool bulletPool)
        {
            _bulletConfig = bulletConfig;
            _bulletPool = bulletPool;
        }
        
        public void CreateBullet(Vector2 position, Vector2 direction)
        {
            SetupBullet(new Args
            {
                PhysicsLayer = (int)_bulletConfig.PhysicsLayerEnum,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = position,
                Velocity = direction
            });
        }

        private void SetupBullet(Args args)
        {
            var bullet = _bulletPool.GetBullet();
            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.SetVelocity(args.Velocity);

            if (_bulletPool.AddActiveBullet(bullet))
            {
                bullet.OnCollisionEntered += _bulletPool.OnBulletCollision;
            }
        }

        private struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
        }
    }
}