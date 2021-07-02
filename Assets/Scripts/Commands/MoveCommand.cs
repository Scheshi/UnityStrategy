using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class MoveCommand : IMoveCommand
    {
        private Vector3 _to;
        private CancellationTokenSource _cancellationToken;

        public MoveCommand(Vector3 to, CancellationTokenSource token)
        {
            _to = to;
            _cancellationToken = token;
        }

        public async void Move(NavMeshAgent agent)
        {
            try
            {
                await MoveTo(agent, _to).WithCancellation(_cancellationToken.Token);
            }
            catch (OperationCanceledException e)
            {
                return;
            }
        }

        private async Task<AsyncUtils.VoidObject> MoveTo(NavMeshAgent agent, Vector3 to)
        {
            agent.SetDestination(to);
            while (Mathf.Abs(agent.transform.position.x - _to.x) < 0.1f &&
                   Mathf.Abs(agent.transform.position.z - _to.z) < 0.1f)
            {
                await Task.Yield();
            }
            return new AsyncUtils.VoidObject();
        }
    }
}