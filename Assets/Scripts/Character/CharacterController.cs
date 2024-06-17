using Bullets;
using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject Character;
        [SerializeField] private GameManager.GameManager GameManager;
        [FormerlySerializedAs("BulletPuller")] [SerializeField] private BulletPool BulletPool;
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
            BulletPool.CreateBullets(new BulletPool.Args
            {
                PhysicsLayer = (int)BulletConfig.PhysicsLayerEnum,
                Color = BulletConfig.Color,
                Damage = BulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * BulletConfig.Speed
            });
        }
    }
}