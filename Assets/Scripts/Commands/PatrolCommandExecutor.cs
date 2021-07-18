using System.Threading.Tasks;
using Abstractions;
using UnityEngine.AI;


namespace Commands
{
    public class PatrolCommandExecutor: CommandExecutorBase<IPatrolCommand>
    {
        private NavMeshAgent _ownerTransform;
        
        
        public PatrolCommandExecutor(NavMeshAgent ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }
        
        protected override async Task ExecuteTypeCommand(IPatrolCommand command)
        {
            command.SetStartPosition(_ownerTransform.transform.position); 
            Task task = new Task(() => command.Patrol(_ownerTransform));
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            await task;
        }
    }
}