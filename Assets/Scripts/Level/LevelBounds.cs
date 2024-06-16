using UnityEngine;

namespace Level
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform LeftBorder;

        [SerializeField] private Transform RightBorder;

        [SerializeField] private Transform DownBorder;

        [SerializeField] private Transform TopBorder;

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > LeftBorder.position.x
                   && positionX < RightBorder.position.x
                   && positionY > DownBorder.position.y
                   && positionY < TopBorder.position.y;
        }
    }
}