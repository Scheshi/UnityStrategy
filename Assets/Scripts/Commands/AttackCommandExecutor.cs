using Abstractions;

namespace Commands
{
    public class AttackCommandExecutor: CommandExecutorBase<IAttackCommand>
    {
        protected override void ExecuteTypeCommand(IAttackCommand command)
        {
            command.Attack();
        }
    }
}