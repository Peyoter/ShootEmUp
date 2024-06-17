using Bullets;
using UnityEngine;

namespace Components
{
    public class CollisionBulletComponent : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Bullet bullet)) return;
            bullet.OnCollisionEnter2D();
            var hit = GetComponent<HitPointsComponent>();
            hit.TakeDamage(bullet.Damage);
        }
    }
}