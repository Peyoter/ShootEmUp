using System;
using Character;
using Components;
using UnityEngine;

namespace Input
{
    public static class Move {
        public const float Left = -1.0f;
        public const float Right = 1.0f;
        public const float Stop = 0;
    }
        
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        [SerializeField] private GameObject Character;
        
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                var shoot = Character.GetComponent<ShootComponent>();
                shoot.Fire();
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = Move.Left;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = Move.Right;
            }
            else
            {
                HorizontalDirection = Move.Stop;
            }
        }

        private void FixedUpdate()
        {
            Character.GetComponent<MoveComponent>()
                .MoveByRigidbodyVelocity(new Vector2(HorizontalDirection, 0) * Time.fixedDeltaTime);
        }
    }
}