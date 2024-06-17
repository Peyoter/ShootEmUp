using Common;
using UnityEngine;

namespace Bullets
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private BulletConfig Config;
        [SerializeField] private BulletPool BulletPool;
        
        public void CreateEnemyBullet(Vector2 position, Vector2 direction)
        {
            // Перенести в 
            SetupBullet(new Args
            {
                PhysicsLayer = (int)PhysicsLayerEnum.ENEMY_BULLET,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }

        public void CreateUserBullet(Vector2 position, Vector2 direction)
        {
            SetupBullet(new Args
            {
                PhysicsLayer = (int)Config.PhysicsLayerEnum,
                Color = Config.Color,
                Damage = Config.Damage,
                Position = position,
                Velocity = direction * Config.Speed
            });
        }
        
        public void SetupBullet(Args args)
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
        
        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
        }
    }
}