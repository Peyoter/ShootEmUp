using Components;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Input
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField] private GameObject Character;

        [SerializeField] private CharacterController CharacterController;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                var weapon = Character.GetComponent<WeaponComponent>();
                weapon.Shoot();
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }

        private void FixedUpdate()
        {
            Character.GetComponent<MoveComponent>()
                .MoveByRigidbodyVelocity(new Vector2(HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}