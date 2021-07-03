using System;
using Abstractions;
using UnityEngine;
using Zenject;


namespace Commands.Creators
{
    public sealed class MoveCommandCreator: CommandCreator<IMoveCommand>
    {
        private Action<IMoveCommand> _onCallBack;
        [Inject]private ScriptableModel<Vector3> _position;
        
        protected override void CreateCommand(Action<IMoveCommand> onCallBack)
        {
            _onCallBack = onCallBack;
            _position.OnChangeValue += OnChangePosition;
        }

        private void OnChangePosition()
        {
            _position.OnChangeValue -= OnChangePosition;
            _onCallBack.Invoke(new MoveCommand(_position.CurrentValue));
        }
    }
}