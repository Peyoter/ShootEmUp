using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePoint.position;

        private Quaternion Rotation => FirePoint.rotation;

        [SerializeField] private Transform FirePoint;
        [SerializeField] private BulletFactory BulletFactory;

        private bool _canShoot;

        private Vector2 GetWeaponDirection()
        {
            return Rotation * Vector3.up;
        }

        private void Fire()
        {
            BulletFactory.CreateUserBullet(Position, GetWeaponDirection());
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