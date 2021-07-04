using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utils;


namespace Commands
{
    public class MoveCommand : IMoveCommand
    {
        private Vector3 _to;
        private CancellationTokenSource _cancellationToken;
        private CancellationModel _cancellation;

        public MoveCommand(Vector3 to, CancellationModel cancellationModel)
        {
            _to = to;
            _cancellation = cancellationModel;
            _cancellation.OnChangeValue += Cancel;
        }

        private void Cancel()
        {
            if (!_cancellation.CurrentValue)
            {
                _cancellationToken?.Cancel();
            }
        }

        public async void Move(NavMeshAgent agent)
        {
            _cancellationToken = new CancellationTokenSource();
            try
            {
                while (true)
                {
                    agent.SetDestination(_to);
                    await new MoveAwatable(agent, _to, null).WithCancellation(_cancellationToken.Token);
                }
            }
            catch(Exception e)
            {
                agent.SetDestination(agent.transform.position);
                //Debug.Log(e);
            }
        }

        private async Task<int> MoveTo(NavMeshAgent agent, Vector3 to)
        {
            agent.SetDestination(to);
            while (Mathf.Abs(agent.transform.position.x - _to.x) < 0.1f &&
                   Mathf.Abs(agent.transform.position.z - _to.z) < 0.1f)
            {
                await Task.Yield();
            }

            return 1;
        }
    }
}