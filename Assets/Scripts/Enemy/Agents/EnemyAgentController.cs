using System;
using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.Agents
{
    public class EnemyAgentController : MonoBehaviour
    {
        [FormerlySerializedAs("MoveAgent")] [SerializeField] private EnemyMoveController MoveController;
        [FormerlySerializedAs("EnemyAttackAgent")] [SerializeField] private EnemyAttackController EnemyAttackController;
        private TargetComponent _target;
       
        private void Awake()
        {
            _target = GetComponent<TargetComponent>();
            EnemyAttackController.AppendCondition(() => MoveController.HasReachedGoal);
            EnemyAttackController.AppendCondition(() => _target.IsTargetLive());
        }
    }
}