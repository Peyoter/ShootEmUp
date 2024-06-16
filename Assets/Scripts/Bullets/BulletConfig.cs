using Common;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] public PhysicsLayerEnum PhysicsLayerEnum;

        [SerializeField] public Color Color;

        [SerializeField] public int Damage;

        [SerializeField] public float Speed;
    }
}