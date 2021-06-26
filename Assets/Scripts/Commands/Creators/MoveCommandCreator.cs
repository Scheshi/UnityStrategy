using System;
using Abstractions;
using Utils;

namespace Commands.Creators
{
    public sealed class MoveCommandCreator: CommandCreator<IMoveCommand>
    {
        private Action<IMoveCommand> _onCallBack;
        private PositionModel _position;
        
        protected override void CreateCommand(Action<IMoveCommand> onCallBack)
        {
            _onCallBack = onCallBack;
            _position.OnSetPosition += OnChangePosition;
        }

        private void OnChangePosition()
        {
            _position.OnSetPosition -= OnChangePosition;
            _onCallBack.Invoke(new MoveCommand(_position.ClickPosition));
        }
    }
}