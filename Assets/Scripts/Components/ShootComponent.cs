using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour
    {

        private bool _canShoot;
        private WeaponComponent _weaponComponent;
        private ComponentManager _characterManager;

        private void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
            _characterManager = GetComponent<ComponentManager>();
        }

        private void ShootByWeaponDirection()
        {
            _characterManager.BulletFactory.CreateBullet(_weaponComponent.Position, _weaponComponent.GetWeaponDirection());
        }
        
        public void ShootToTarget(Vector2 targetPosition)
        {
            var startPosition = _weaponComponent.Position;
            var vector = targetPosition - startPosition;
            var direction = vector.normalized;
            _characterManager.BulletFactory.CreateBullet(_weaponComponent.Position, direction);
        }

        private void FixedUpdate()
        {
            if (!_canShoot) return;
            ShootByWeaponDirection();
            _canShoot = false;
        }

        public void Fire()
        {
            _canShoot = true;
        }
    }
}