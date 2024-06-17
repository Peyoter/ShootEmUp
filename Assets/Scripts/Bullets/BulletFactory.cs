using Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullets
{
    public class BulletFactory : MonoBehaviour
    {
        // Если бы был бы zenject я бы фабричные методы разместил бы в самих ScriptbleObject'aх
        
        [SerializeField] private BulletConfig UserBullets;
        [SerializeField] private BulletConfig EnemyBullet;
        [SerializeField] private BulletPool BulletPool;

        public void CreateEnemyBullet(Vector2 position, Vector2 direction)
        {
            SetupBullet(new Args
            {
                PhysicsLayer = (int)EnemyBullet.PhysicsLayerEnum,
                Color = EnemyBullet.Color,
                Damage = EnemyBullet.Damage,
                Position = position,
                Velocity = direction * 2.0f
            });
        }

        public void CreateUserBullet(Vector2 position, Vector2 direction)
        {
            SetupBullet(new Args
            {
                PhysicsLayer = (int)UserBullets.PhysicsLayerEnum,
                Color = UserBullets.Color,
                Damage = UserBullets.Damage,
                Position = position,
                Velocity = direction * UserBullets.Speed
            });
        }

        private void SetupBullet(Args args)
        {
            var bullet = BulletPool.GetBullet();
            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.SetVelocity(args.Velocity);

            if (BulletPool.AddActiveBullet(bullet))
            {
                bullet.OnCollisionEntered += BulletPool.OnBulletCollision;
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