using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
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
            Task task = new Task(() => command.Move(_gameObjectMoving));
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            await task;
            Debug.Log(nameof(ExecuteTypeCommand));
        }
    }
}