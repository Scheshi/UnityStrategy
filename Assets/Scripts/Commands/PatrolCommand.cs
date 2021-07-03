using System;
using Abstractions;
using UnityEngine;

namespace Commands
{
    public class PatrolCommand: IPatrolCommand
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;

        public PatrolCommand(Vector3 endPoint)
        {
            _endPoint = endPoint;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPoint = startPosition;
        }

        public void Patrol(Transform transform)
        {
            if (Mathf.Abs(transform.position.x - _endPoint.x) < 0.1f && Mathf.Abs(transform.position.y - _endPoint.y) < 0.1f  &&
                Mathf.Abs(transform.position.z - _endPoint.z) < 0.1f)
            {
                (_startPoint, _endPoint) = (_endPoint, _startPoint);
                return;
            }
            transform.Translate((_endPoint - transform.position).normalized * 3.0f * Time.deltaTime);
        }
    }
}