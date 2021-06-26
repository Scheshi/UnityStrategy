using Abstractions;
using UnityEngine;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>
    {
        protected override void ExecuteTypeCommand(ICreateUnitCommand command)
        {
            Object.Instantiate(command.InstantiateUnit());
        }
    }
}