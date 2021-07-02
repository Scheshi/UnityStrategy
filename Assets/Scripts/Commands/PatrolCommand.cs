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
                    await new MoveAwatable(agent, _endPoint, null).WithCancellation(_cancellationToken.Token);
                    (_startPoint, _endPoint) = (_endPoint, _startPoint);
                }
            }
            catch(Exception e)
            {
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
                agent.SetDestination(to);
                while (Mathf.Abs(agent.transform.position.x - to.x) < 0.1f &&
                       Mathf.Abs(agent.transform.position.z - to.z) < 0.1f)
                {
                    await Task.Yield();
                }
            }
            catch (OperationCanceledException)
            {
                _onCancelled?.Invoke();
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