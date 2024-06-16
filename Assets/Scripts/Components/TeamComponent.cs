using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public bool IsPlayer
        {
            get { return isPlayer; }
        }
        
        [SerializeField]
        private bool isPlayer;
    }
}