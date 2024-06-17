using System;
using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.Agents
{
    public class EnemyAgentSystem : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAgent MoveAgent;
        [SerializeField] private EnemyAttackAgent EnemyAttackAgent;
        private TargetComponent _target;
       
        private void Awake()
        {
            _target = GetComponent<TargetComponent>();
            EnemyAttackAgent.AppendCondition(() => MoveAgent.HasReachedGoal);
            EnemyAttackAgent.AppendCondition(() => _target.IsTargetLive());
        }
    }
}