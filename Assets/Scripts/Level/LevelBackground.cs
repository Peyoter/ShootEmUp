using System;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

         [SerializeField]
        private Params MParams;

        private void Awake()
        {
            _startPositionY = MParams.MStartPositionY;
            _endPositionY = MParams.MEndPositionY;
            _movingSpeedY = MParams.MMovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void FixedUpdate()
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
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
             [SerializeField]
            public float MStartPositionY;

             [SerializeField]
            public float MEndPositionY;

             [SerializeField]
            public float MMovingSpeedY;
        }
    }
}