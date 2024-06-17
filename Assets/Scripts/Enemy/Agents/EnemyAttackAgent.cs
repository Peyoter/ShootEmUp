using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {

        [SerializeField] private ShootComponent ShootComponent;
        [SerializeField] private EnemyMoveAgent MoveAgent;
        [SerializeField] private float Countdown;

        private GameObject _target;
        private float _currentTime;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = Countdown;
        }

        private void FixedUpdate()
        {
            if (!MoveAgent.IsReached)
            {
                return;
            }

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointCountNotNull())
            {
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += Countdown;
            }
        }

        private void Fire()
        {
            ShootComponent.ShootToTarget(_target.transform.position);
        }
    }
}