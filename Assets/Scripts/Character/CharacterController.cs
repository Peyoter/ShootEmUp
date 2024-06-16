using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject Character; 
        [SerializeField] private GameManager GameManager;
        [SerializeField] private BulletSystem BulletSystem;
        [SerializeField] private BulletConfig BulletConfig;
        
        public bool FireRequired;

        private void OnEnable()
        {
            Character.GetComponent<HitPointsComponent>().hpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            Character.GetComponent<HitPointsComponent>().hpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => GameManager.FinishGame();

        private void FixedUpdate()
        {
            if (FireRequired)
            {
                OnFlyBullet();
                FireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = Character.GetComponent<WeaponComponent>();
            BulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) BulletConfig.PhysicsLayer,
                color = BulletConfig.Color,
                damage = BulletConfig.Damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * BulletConfig.Speed
            });
        }
    }
}