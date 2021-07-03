using System;
using Abstractions;
using UnityEngine;
using Zenject;


namespace Commands.Creators
{
    public class PatrolCommandCreator: CommandCreator<IPatrolCommand>
    {
        [Inject] private ScriptableModel<Vector3> _positionModel;
        private Action<IPatrolCommand> _action;
        
        protected override void CreateCommand(Action<IPatrolCommand> onCallBack)
        {
            _action = onCallBack;
            _positionModel.OnChangeValue += PositionModelOnOnChangeValue;
        }

        private void PositionModelOnOnChangeValue()
        {
            _action?.Invoke(new PatrolCommand(_positionModel.CurrentValue));
            _positionModel.OnChangeValue -= PositionModelOnOnChangeValue;
            _action = null;
        }
    }
}