using System.Threading.Tasks;
using Abstractions;
using UnityEngine;

namespace Commands
{
    public class AttackCommandExecutor: CommandExecutorBase<IAttackCommand>
    {
        private IAttacker _attacker;
        
        public AttackCommandExecutor(IAttacker attacker)
        {
            _attacker = attacker;
        }
        
        protected override async Task ExecuteTypeCommand(IAttackCommand command)
        {
            command.SetAttacker(_attacker);
            Task task = new Task(command.Attack);
            task.RunSynchronously();
            await task;
        }
    }
}