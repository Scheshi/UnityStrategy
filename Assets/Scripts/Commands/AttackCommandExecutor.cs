using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Commands
{
    public class AttackCommandExecutor: CommandExecutorBase<IAttackCommand>
    {
        private Transform _ownerTransform;
        
        public AttackCommandExecutor(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }
        
        protected override async Task ExecuteTypeCommand(IAttackCommand command)
        {
            Task task = new Task(() => command.Attack(_ownerTransform.position));
            task.RunSynchronously();
            await task;
        }
    }
}