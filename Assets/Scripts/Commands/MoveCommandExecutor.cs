using System.Threading.Tasks;
using Abstractions;
using UnityEngine.AI;


namespace Commands
{
    public class MoveCommandExecutor: CommandExecutorBase<IMoveCommand>
    {
        private NavMeshAgent _gameObjectMoving;

        public MoveCommandExecutor(NavMeshAgent gameObjectMoving)
        {
            _gameObjectMoving = gameObjectMoving;
        }
        
        protected override async Task ExecuteTypeCommand(IMoveCommand command)
        {
            await command.Move(_gameObjectMoving);
        }
    }
}