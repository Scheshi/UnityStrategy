using System.Collections.Generic;
using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        [Inject] private CancellationModel _cancellation;
        [Inject] private CommandCreator<ICreateUnitCommand> _unitProduceCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IAttackCommand, IAttackable> _attackCommandCreator;
        [Inject] private CommandCreator<ICancelCommand> _cancelCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IMoveCommand, Vector3> _moveCommandCreator;
        [Inject] private CommandCreatorWithCancelled<IPatrolCommand, Vector3> _patrolCommandCreator;

        private List<ITick> _tickables = new List<ITick>();
        private bool _isPending;

        public void OnCancelCommands()
        {
            _cancellation.SetValue(false);
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
            _cancellation.SetValue(false);
            _unitProduceCommandCreator.Create(executor, executor.Execute, isComplete);
            _attackCommandCreator.Create(executor, executor.Execute, isComplete);
            _cancelCommandCreator.Create(executor, executor.Execute, isComplete);
            _moveCommandCreator.Create(executor, executor.Execute, isComplete);
            _patrolCommandCreator.Create(executor, executor.Execute, isComplete);
            _isPending = true;
        }
    }
}