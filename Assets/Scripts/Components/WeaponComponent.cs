using Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePoint.position;

        public Quaternion Rotation => FirePoint.rotation;

        [SerializeField] private Transform FirePoint;
        [SerializeField] private BulletFactory BulletFactory;

        private bool _canShoot;

        public Vector2 GetWeaponDirection()
        {
            return Rotation * Vector3.up;
        }

        public void Fire()
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