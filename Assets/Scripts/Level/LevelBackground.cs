using System;
using UnityEngine;

namespace ShootEmUp
{
    public class LevelBackground : MonoBehaviour,
        IGameFixedUpdateListener
    {
        [SerializeField] private Params positions;

        private Transform myTransform;
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;

        private float positionX;
        private float positionZ;

        private void Awake()
        {
            IGameListener.Register(this);
            enabled = false;

            startPositionY = positions.startPositionY;
            endPositionY = positions.endPositionY;
            movingSpeedY = positions.movingSpeed;
            myTransform = transform;

            var position = myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var position = myTransform.position;

            if (position.y <= endPositionY)
            {
                myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

            myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * fixedDeltaTime,
                positionZ
            );
        }

        [Serializable]
        public class Params
        {
            [SerializeField] public float startPositionY;
            [SerializeField] public float endPositionY;
            [SerializeField] public float movingSpeed;
        }
    }
}