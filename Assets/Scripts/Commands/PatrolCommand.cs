using System;
using System.Threading;
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
        private CancellationTokenSource _cancellationToken;


        public PatrolCommand(Vector3 endPoint, CancellationTokenSource token)
        {
            _endPoint = endPoint;
            _cancellationToken = token;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPoint = startPosition;
        }

        public async void Patrol(NavMeshAgent agent)
        {
            try
            {
                while (true)
                {
                    await MoveTo(agent, _endPoint).WithCancellation(_cancellationToken.Token);
                    (_startPoint, _endPoint) = (_endPoint, _startPoint);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
        
        private async Task<AsyncUtils.VoidObject> MoveTo(NavMeshAgent agent, Vector3 to)
        {
            try
            {
                agent.SetDestination(to);
                while (Mathf.Abs(agent.transform.position.x - to.x) < 0.1f &&
                       Mathf.Abs(agent.transform.position.z - to.z) < 0.1f)
                {
                    await Task.Yield();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return new AsyncUtils.VoidObject();
        }
    }
}