using System;
using GameSystem;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        [SerializeField] private Params MParams;

        private void Awake()
        {
            IGameListener.Register(this);
            _startPositionY = MParams.MStartPositionY;
            _endPositionY = MParams.MEndPositionY;
            _movingSpeedY = MParams.MMovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float MStartPositionY;

            [SerializeField] public float MEndPositionY;

            [SerializeField] public float MMovingSpeedY;
        }
    }
}