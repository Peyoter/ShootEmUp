using System;
using UnityEngine;

namespace Components
{
    public class DeathObserverComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        private void OnEnable()
        {
            GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject o)
        {
            OnDeath?.Invoke(o);
        }
    }
}