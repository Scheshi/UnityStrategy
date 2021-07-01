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
        
        protected override void ExecuteTypeCommand(IPatrolCommand command)
        {
            command.SetStartPosition(_ownerTransform.transform.position);
            command.Patrol(_ownerTransform);
        }
    }
}