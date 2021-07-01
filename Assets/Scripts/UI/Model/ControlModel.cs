using Abstractions;
using UnityEngine;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        [Inject] private CommandCreator<ICreateUnitCommand> _unitProduceCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IAttackCommand, IAttackable> _attackCommandCreator;
        [Inject] private CommandCreator<ICancelCommand> _cancelCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IMoveCommand, Vector3> _moveCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IPatrolCommand, Vector3> _patrolCommandCreator;

        public void OnCancelCommands()
        {
            _attackCommandCreator.Cancel();
            _moveCommandCreator.Cancel();
            _patrolCommandCreator.Cancel();
        }
        
        public void OnClick(ICommandExecutor executor)
        {
            OnCancelCommands();
            _unitProduceCommandCreator.Create(executor, executor.Execute);
            _attackCommandCreator.Create(executor, executor.Execute);
            _cancelCommandCreator.Create(executor, executor.Execute);
            _moveCommandCreator.Create(executor, executor.Execute);
            _patrolCommandCreator.Create(executor, executor.Execute);
            
        }
    }
}