using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Commands
{
    public class BuildingCommandQueue: IDisposable, ICommandQueue
    {
        private CommandExecutorBase<ICreateUnitCommand> _produceCommandExecutor;
        private CommandExecutorBase<ISpawnPointCommand> _unitSpawnPointExecutor;


        private ReactiveCollection<ICommand> _innerCollection = new ReactiveCollection<ICommand>();

        public BuildingCommandQueue(CommandExecutorBase<ICreateUnitCommand> unitProducer,
            CommandExecutorBase<ISpawnPointCommand> commandExecutorBase)
        {
            _produceCommandExecutor = unitProducer;
            _unitSpawnPointExecutor = commandExecutorBase;
            Init();
        }
        
        private void Init()
        {
            _innerCollection.ObserveAdd().Subscribe(item =>
            {
                OnNewCommand(item.Value, item.Index);
            });
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
            await _produceCommandExecutor.TryExecute(command);
            if (_innerCollection.Count > 0)
            {
                _innerCollection.RemoveAt(0);
            }
            CheckTheQueue();
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
            _innerCollection.Clear();
        }

        public void Dispose()
        {
            _innerCollection?.Dispose();
        }
    }
}