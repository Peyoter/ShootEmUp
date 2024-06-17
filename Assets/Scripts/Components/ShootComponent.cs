using Character;
using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour
    {

        private bool _canShoot;
        private WeaponComponent _weaponComponent;
        private CharacterManager _characterManager;

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _characterManager = GetComponent<CharacterManager>();
        }

        private void Shoot()
        {
            _characterManager.BulletFactory.CreateBullet(_weaponComponent.Position, _weaponComponent.GetWeaponDirection());
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