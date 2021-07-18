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

        public async Task Move(NavMeshAgent agent)
        {
            _cancellationToken = new CancellationTokenSource();
            Task task = new Task(async () =>
            {
                agent.SetDestination(_to);
                while (!(agent.transform.position == _to))
                {
                    await Task.Yield();
                }
            });
            try
            { 
                task.Start(TaskScheduler.FromCurrentSynchronizationContext());
                await task;
            }
            catch (Exception e)
            {
                agent.SetDestination(agent.transform.position);
                Debug.Log(e);
            }
        }
    }
}