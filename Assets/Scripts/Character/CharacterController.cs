using Bullets;
using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject Character;
        [SerializeField] private GameManager.GameManager GameManager;
        [SerializeField] private BulletPuller BulletPuller;
        [SerializeField] private BulletConfig BulletConfig;

        public bool FireRequired;

        private void OnEnable()
        {
            Character.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            Character.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
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
            BulletPuller.CreateBullets(new BulletPuller.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int)BulletConfig.PhysicsLayerEnum,
                Color = BulletConfig.Color,
                Damage = BulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * BulletConfig.Speed
            });
        }
    }
}