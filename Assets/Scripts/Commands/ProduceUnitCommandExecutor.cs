using Abstractions;
using UnityEngine;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>
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
    }
}