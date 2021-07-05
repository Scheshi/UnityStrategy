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
        
        protected override void ExecuteTypeCommand(IMoveCommand command)
        {
            command.Move(_gameObjectMoving);
        }
    }
}