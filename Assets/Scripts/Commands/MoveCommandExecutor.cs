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
        
        protected override Task ExecuteTypeCommand(IMoveCommand command)
        {
            return Task.Run(() => command.Move(_gameObjectMoving));
        }
    }
}