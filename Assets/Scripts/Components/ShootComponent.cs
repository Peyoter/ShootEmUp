using System;
using Bullets;
using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour
    {
        [SerializeField] private BulletFactory BulletFactory;

        private bool _canShoot;
        private WeaponComponent _weaponComponent;

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
        }

        private void Shoot()
        {
            BulletFactory.CreateUserBullet(_weaponComponent.Position, _weaponComponent.GetWeaponDirection());
        }

        private void FixedUpdate()
        {
            if (_canShoot)
            {
                Shoot();
                _canShoot = false;
            }
        }

        public void Fire()
        {
            _canShoot = true;
        }
    }
}