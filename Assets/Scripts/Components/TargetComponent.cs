using UnityEngine;

namespace Components
{
    public class TargetComponent : MonoBehaviour
    {
        private GameObject _target;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public Vector2 GetTargetPosition()
        {
            return _target.transform.position;
        }

        public bool IsTargetLive()
        {
            return GetComponent<HitPointsComponent>().IsHitPointCountNotNull();
        }
    }
}