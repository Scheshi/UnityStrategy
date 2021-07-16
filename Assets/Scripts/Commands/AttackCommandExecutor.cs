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
        
        protected override Task ExecuteTypeCommand(IAttackCommand command)
        {
            return Task.Run((() => command.Attack(_ownerTransform.position)));
        }
    }
}