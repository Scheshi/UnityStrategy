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
        
        protected override void ExecuteTypeCommand(IAttackCommand command)
        {
            command.Attack(_ownerTransform.position);
        }
    }
}