using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;


namespace Commands
{
    public class UnitCommandQueue: IDisposable, ICommandQueue
    {
        [Inject] CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
        [Inject] CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;
        [Inject] CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
        

        private ReactiveCollection<ICommand> _innerCollection = new ReactiveCollection<ICommand>();
        private bool _isPending;

        public UnitCommandQueue(CommandExecutorBase<IMoveCommand> moveCommandExecutor,
            CommandExecutorBase<IPatrolCommand> patrolCommandExecutor,
            CommandExecutorBase<IAttackCommand> attackCommandExecutor)
        {
            _moveCommandExecutor = moveCommandExecutor;
            _patrolCommandExecutor = patrolCommandExecutor;
            _attackCommandExecutor = attackCommandExecutor;
            Init();
        }
        
        private void Init()
        {
            _innerCollection.ObserveAdd().Subscribe(item => OnNewCommand(item.Value, item.Index));
        }

        private void OnNewCommand(ICommand command, int index)
        {
            if (index == 0)
            {
                ExecuteCommand(command);
            }
        }

        private async void ExecuteCommand(ICommand command)
        {
            if (!_isPending)
            {
                _isPending = true;
                await _moveCommandExecutor.TryExecute(command);
                await _patrolCommandExecutor.TryExecute(command);
                await _attackCommandExecutor.TryExecute(command);
                if (_innerCollection.Count > 0)
                {
                    _innerCollection.RemoveAt(0);
                }

                _isPending = false;
                Debug.Log(_isPending);
                CheckTheQueue();
            }
        }

        private void CheckTheQueue()
        {
            if (_innerCollection.Count > 0)
            {
                ExecuteCommand(_innerCollection[0]);
            }
        }

        public void EnqueueCommand(ICommand wrappedCommand)
        {
            _innerCollection.Add(wrappedCommand);
        }

        public void Clear()
        {
            if (_innerCollection.Count > 0)
            {
                _innerCollection[0].Cancel();
            }

            _innerCollection.Clear();
        }

        public void Dispose()
        {
            _innerCollection?.Dispose();
        }
    }
}