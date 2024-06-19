using System;
using Components;
using GameSystem;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackController : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private ShootComponent ShootComponent;
        [SerializeField] private float Countdown;

        private float _currentTime;

        private readonly CompositeCondition _compositeCondition = new();

        public void Reset()
        {
            _currentTime = Countdown;
        }

        public void Awake()
        {
            IGameListener.Register(this);
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            
            if (!_compositeCondition.IsTrue()) return;

            _currentTime -= fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += Countdown;
            }
        }

        private void Fire()
        {
            var targetPosition = GetComponent<TargetComponent>().GetTargetPosition();
            ShootComponent.ShootToTarget(targetPosition);
        }

        public void AppendCondition(Func<bool> condition)
        {
            _compositeCondition.AddCondition(condition);
        }
    }
}