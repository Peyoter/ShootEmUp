using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HpEmpty;
        
        [SerializeField] private int HitPoints;
        
        public bool IsHitPointCountNotNull() {
            return HitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
            if (HitPoints <= 0)
            {
                HpEmpty?.Invoke(gameObject);
            }
        }
    }
}