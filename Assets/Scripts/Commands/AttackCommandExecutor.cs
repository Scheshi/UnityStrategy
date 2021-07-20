using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;

namespace Commands
{
    public class AttackCommandExecutor: CommandExecutorBase<IAttackCommand>
    {
        private IAttacker _attacker;
        private NavMeshAgent _agent;
        
        public AttackCommandExecutor(IAttacker attacker, NavMeshAgent agent)
        {
            _attacker = attacker;
            _agent = agent;
        }
        
        protected override async Task ExecuteTypeCommand(IAttackCommand command)
        {
            command.SetDependency(_attacker, _agent);
            Task task = new Task(command.StartAttack);
            task.Start(TaskScheduler.FromCurrentSynchronizationContext());
            await task;
        }
    }
}