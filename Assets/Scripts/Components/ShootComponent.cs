using GameSystem;
using UnityEngine;

namespace Components
{
    public class ShootComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        private bool _canShoot;
        private WeaponComponent _weaponComponent;
        private ComponentManager _characterManager;

        private void Awake()
        {
            IGameListener.Register(this);
            _weaponComponent = GetComponent<WeaponComponent>();
            _characterManager = GetComponent<ComponentManager>();
        }

        private void ShootByWeaponDirection()
        {
            _characterManager.BulletFactory.CreateBullet(_weaponComponent.Position,
                _weaponComponent.GetWeaponDirection());
        }

        public void ShootToTarget(Vector2 targetPosition)
        {
            var startPosition = _weaponComponent.Position;
            var vector = targetPosition - startPosition;
            var direction = vector.normalized;
            _characterManager.BulletFactory.CreateBullet(_weaponComponent.Position, direction);
        }

        public void OnFixedUpdate(float timeDeltaTime)
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