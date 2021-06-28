using Abstractions;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        [Inject] private CommandCreator<ICreateUnitCommand> _unitProduceCommandCreator;
        [Inject] private CommandCreator<IAttackCommand> _attackCommandCreator;
        [Inject] private CommandCreator<ICancelCommand> _cancelCommandCreator;
        [Inject] private CommandCreator<IMoveCommand> _moveCommandCreator;
        [Inject] private CommandCreator<IPatrolCommand> _patrolCommandCreator;

        
        public void OnClick(ICommandExecutor executor)
        {
            _unitProduceCommandCreator.Create(executor, executor.Execute);
            _attackCommandCreator.Create(executor, executor.Execute);
            _cancelCommandCreator.Create(executor, executor.Execute);
            _moveCommandCreator.Create(executor, executor.Execute);
            _patrolCommandCreator.Create(executor, executor.Execute);
            
        }
    }
}