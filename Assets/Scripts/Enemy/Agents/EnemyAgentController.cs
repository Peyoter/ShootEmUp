using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public class EnemyAgentController : MonoBehaviour
    {
        [SerializeField] private EnemyMoveController MoveController;
        [SerializeField] private EnemyAttackController EnemyAttackController;
        private TargetComponent _target;

        private void Awake()
        {
            _target = GetComponent<TargetComponent>();
            EnemyAttackController.AppendCondition(() => MoveController.HasReachedGoal);
            EnemyAttackController.AppendCondition(() => _target.IsTargetLive());
        }
    }
}