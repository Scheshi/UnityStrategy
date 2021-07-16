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

        public void OnClick(ICommandExecutor executor, ICommandQueue queue)
        {
            OnCancelCommands();
            CreateCommand(executor, queue);
        }

        public void CreateCommand(ICommandExecutor executor, ICommandQueue queue, bool isComplete = false)
        {
            if (!Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.RightShift) || isComplete)
            {
                queue.Clear();
            }
            _cancellation.SetValue(false);
            _unitProduceCommandCreator.Create(executor, queue.EnqueueCommand, isComplete);
            _attackCommandCreator.Create(executor, queue.EnqueueCommand, isComplete);
            _cancelCommandCreator.Create(executor, queue.EnqueueCommand, isComplete);
            _moveCommandCreator.Create(executor, queue.EnqueueCommand, isComplete);
            _patrolCommandCreator.Create(executor, queue.EnqueueCommand, isComplete);
            _isPending = true;
        }
    }
}