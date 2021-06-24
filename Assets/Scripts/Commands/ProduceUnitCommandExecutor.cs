using Abstractions;

namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>
    {
        protected override void ExecuteTypeCommand(ICreateUnitCommand command)
        {
            command.InstantiateUnit();
        }
    }
}