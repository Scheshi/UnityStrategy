using System;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class PatrolCommand: IPatrolCommand
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private bool _isStopPatrol;


        public PatrolCommand(Vector3 endPoint)
        {
            _endPoint = endPoint;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPoint = startPosition;
        }

        public async void Patrol(NavMeshAgent agent)
        {
            while (!_isStopPatrol)
            {
                (_startPoint, _endPoint) = (_endPoint, _startPoint);
                await MoveTo(agent, _endPoint);
            }
        }
        
        private async Task MoveTo(NavMeshAgent agent, Vector3 to)
        {
            agent.SetDestination(to);
            while (Mathf.Abs(agent.transform.position.x - to.x) < 0.1f &&
                   Mathf.Abs(agent.transform.position.z - to.z) < 0.1f)
            {
                await Task.Yield();
            }
        }

        public void StopPatrol()
        {
            _isStopPatrol = true;
        }
    }
}