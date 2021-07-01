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
        
        protected override async void ExecuteTypeCommand(IMoveCommand command)
        {
            command.SetFrom(_gameObjectMoving.transform.position);
            while (true)
            {
                command.Move(_gameObjectMoving);
            }
            
        }
    }
}