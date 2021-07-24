using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class PatrolCommand: IPatrolCommand, IDisposable
    {
        class PatrolOperation: IDisposable
        {
            private bool _isDispose;
            private Vector3 _startPoint;
            private Vector3 _endPoint;
            private bool _toEnd;
            private PatrolCommand _command;
            public PatrolOperation(PatrolCommand command, Vector3 startPoint, Vector3 endPoint)
            {
                _isDispose = false;
                _command = command;
                _startPoint = startPoint;
                _endPoint = endPoint;
                Thread thread = new Thread(PatrolCoroutine);
                thread.Start();
            }

            private void PatrolCoroutine()
            {
                _command._toPosition.OnNext(_endPoint);
                while (!_isDispose)
                {
                    while (Mathf.Abs(_command._currentPoint.x - _endPoint.x) > 0.1f &&
                           Mathf.Abs(_command._currentPoint.z - _endPoint.z) > 0.1f)
                    {
                        Thread.Sleep(100);
                    }

                    (_startPoint, _endPoint) = (_endPoint, _startPoint);
                    _command._toPosition.OnNext(_endPoint);
                }
            }

            public void Dispose()
            {
                _isDispose = true;
            }
        }
        
        private Vector3 _currentPoint;
        private bool _isCommandPending;
        private Subject<Vector3> _toPosition = new Subject<Vector3>();
        private readonly Vector3 _endPoint;
        private Vector3 _startPoint;
        private PatrolOperation _operation;
        private NavMeshAgent _agent;
        private IDisposable _updater;
        


        public PatrolCommand(Vector3 endPoint)
        {
            _endPoint = endPoint;
            _toPosition.ObserveOn(Scheduler.MainThread).Subscribe(Move);
        }

        public int CommandImportance { get; } = 1;

        public void Cancel()
        {
            Debug.Log(nameof(Cancel));
            Dispose();
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPoint = startPosition;
        }

        public void Patrol(NavMeshAgent agent)
        {
            _agent = agent;
            _isCommandPending = true;
            _updater = Observable.EveryUpdate().Subscribe(item => { _currentPoint = _agent.transform.position; });
            _operation = new PatrolOperation(this, _startPoint, _endPoint);
        }

        private void Move(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void Dispose()
        {
            _isCommandPending = false;
            _updater?.Dispose();
            _operation?.Dispose();
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