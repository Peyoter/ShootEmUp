using Bullets;
using UnityEngine;

namespace Components
{
    public class ComponentManager : MonoBehaviour
    {
        [SerializeField] private BulletConfig BulletConfig;
        [SerializeField] public BulletPool  BulletPool;
        public BulletFactory BulletFactory;

        private void Awake()
        {
            Init();
        }

        public void SetBulletPool(BulletPool bulletPool)
        {
            BulletPool = bulletPool;
        }
        
        public void Init()
        {
            BulletFactory = new BulletFactory(BulletConfig, BulletPool);
        }
    }
}