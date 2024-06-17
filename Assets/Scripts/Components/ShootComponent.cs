using Bullets;
using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField] private BulletFactory BulletFactory;

        private bool _canShoot;

        private void Fire()
        {
            // BulletFactory.CreateUserBullet(Position, GetWeaponDirection());
        }

        private void FixedUpdate()
        {
            if (_canShoot)
            {
                Fire();
                _canShoot = false;
            }
        }

        public void Shoot()
        {
            _canShoot = true;
        }
    }
}