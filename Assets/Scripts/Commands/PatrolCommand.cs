using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utils;


namespace Commands
{
    public class PatrolCommand: IPatrolCommand
    {
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private CancellationTokenSource _cancellationToken;
        private CancellationModel _cancellation;
        private bool _isCommandPending;


        public PatrolCommand(Vector3 endPoint, CancellationModel model)
        {
            _endPoint = endPoint;
            _cancellation = model;
            model.OnChangeValue += Cancel;
        }

        private void Cancel()
        {
            if (!_cancellation.CurrentValue)
            {
                _cancellationToken?.Cancel();
            }
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPoint = startPosition;
            _isCommandPending = true;
        }

        public async void Patrol(NavMeshAgent agent)
        {
            _cancellationToken = new CancellationTokenSource();
            try
            {
                while (true)
                {
                    agent.SetDestination(_endPoint);
                    await new MoveAwatable(agent, _endPoint, null).WithCancellation(_cancellationToken.Token);
                    (_startPoint, _endPoint) = (_endPoint, _startPoint);
                }
            }
            catch(Exception e)
            {
                agent.SetDestination(agent.transform.position);
                Debug.Log(e);
            }
        }
    }

    public class MoveAwatable : IAwaitable<int>
    {
        private NavMeshAgent _agent;
        private Vector3 _to;
        private Action _onCancelled;

        public MoveAwatable(NavMeshAgent agent, Vector3 to, Action onCancelled)
        {
            _agent = agent;
            _to = to;
            _onCancelled = onCancelled;
        }
        
        public IAwaiter<int> GetAwaiter()
        {
            return new MoveAwaiter(_agent, _to, _onCancelled);
        }
    }

    public class MoveAwaiter : IAwaiter<int>
    {
        private bool _isComplete;
        private Action _onComplete;
        private Action _onCancelled;

        public MoveAwaiter(NavMeshAgent agent, Vector3 to, Action onCancelled)
        {
            _onCancelled = onCancelled;
            MoveTo(agent, to);
        }
        
        private async void MoveTo(NavMeshAgent agent, Vector3 to)
        {
            try
            {
                while (agent.transform.position != to)
                {
                    await Task.Yield();
                }
            }
            catch (Exception e)
            {
                //Only main thread
                //TODO: Подумать над нормальной реализацией, но отмена работает
                //Debug.Log(e.Message);
            }

            _isComplete = true;
            _onComplete.Invoke();
        }
        
        public void OnCompleted(Action continuation)
        {
            _onComplete = continuation;
            if (IsCompleted)
            {
                continuation.Invoke();
            }
        }

        public bool IsCompleted => _isComplete;
        public int GetResult()
        {
            return 1;
        }
    }
}