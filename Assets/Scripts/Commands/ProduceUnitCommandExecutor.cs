using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>, IUnitProducer
    {
        public Vector3 SpawnPosition { get;}

        public ProduceUnitCommandExecutor(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
        
        protected override void ExecuteTypeCommand(ICreateUnitCommand command)
        {
            command.SpawnPosition = SpawnPosition;
            command.InstantiateUnit();
        }

        public IReadOnlyReactiveCollection<IProductionTask> Queue { get; } = new ReactiveCollection<IProductionTask>();
        public void Cancel(int index)
        {
            throw new NotImplementedException();
        }
    }
}