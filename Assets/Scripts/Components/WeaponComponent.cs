using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => FirePoint.position;

        private Quaternion Rotation => FirePoint.rotation;

        [SerializeField] private Transform FirePoint;

        public Vector2 GetWeaponDirection()
        {
            return Rotation * Vector3.up;
        }
        
    }
}