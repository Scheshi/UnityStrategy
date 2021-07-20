using System.Threading.Tasks;
using Abstractions;
using UnityEngine;


namespace Commands
{
    public class SpawnPointExecutor: CommandExecutorBase<ISpawnPointCommand>
    {
        private IBuilding _building;
        
        public SpawnPointExecutor(IBuilding building)
        {
            _building = building;
        }
        
        protected override Task ExecuteTypeCommand(ISpawnPointCommand command)
        {
            Debug.Log("123");
            _building.UnitSpawnPosition = command.Position;
            return new Task(() => {});
        }
    }
}