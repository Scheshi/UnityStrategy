using System.Threading;
using Abstractions;
using UnityEngine;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        [Inject(Id = "Command")] private CancellationTokenSource _tokenSource;
        [Inject] private CommandCreator<ICreateUnitCommand> _unitProduceCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IAttackCommand, IAttackable> _attackCommandCreator;
        [Inject] private CommandCreator<ICancelCommand> _cancelCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IMoveCommand, Vector3> _moveCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IPatrolCommand, Vector3> _patrolCommandCreator;
        
        
        private bool _isPending;

        public void OnCancelCommands()
        {
            _tokenSource.Cancel();
        }

        public void OnCancelCommandCreators()
        {
            if (!_isPending) return;
            _isPending = false;
            _attackCommandCreator.Cancel();
            _moveCommandCreator.Cancel();
            _patrolCommandCreator.Cancel();
        }

        public void OnClick(ICommandExecutor executor)
        {
            OnCancelCommands();
            CreateCommand(executor);
        }

        public void CreateCommand(ICommandExecutor executor, bool isComplete = false)
        {
            _unitProduceCommandCreator.Create(executor, executor.Execute, isComplete);
            _attackCommandCreator.Create(executor, executor.Execute, isComplete);
            _cancelCommandCreator.Create(executor, executor.Execute, isComplete);
            _moveCommandCreator.Create(executor, executor.Execute, isComplete);
            _patrolCommandCreator.Create(executor, executor.Execute, isComplete);
            _isPending = true;
        }
    }
}