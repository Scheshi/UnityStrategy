using Abstractions;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        //[Inject]
        [Inject] private CommandCreator<ICreateUnitCommand> _unitProduceCommandCreator;
        [Inject] private CommandCreator<IAttackCommand> _attackCommandCreator;
        [Inject] private CommandCreator<ICancelCommand> _cancelCommandCreator;
        [Inject] private CommandCreator<IMoveCommand> _moveCommandCreator;

        public void OnClick(ICommandExecutor executor)
        {
            _unitProduceCommandCreator.Create(executor, executor.Execute);
            _attackCommandCreator.Create(executor, executor.Execute);
            _cancelCommandCreator.Create(executor, executor.Execute);
            _moveCommandCreator.Create(executor, executor.Execute);
            
        }
    }
}