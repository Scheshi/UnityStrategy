using System;
using Abstractions;
using Zenject;

namespace Commands.Creators
{
    public sealed class AttackCommandCreator: CommandCreator<IAttackCommand>
    {
        [Inject] private ScriptableModel<IAttackable> _model;
        private Action<IAttackCommand> _action;
        
        protected override void CreateCommand(Action<IAttackCommand> onCallBack)
        {
            _action = onCallBack;
            _model.OnChangeValue += ModelOnOnChangeValue;
        }

        private void ModelOnOnChangeValue()
        {
            _action.Invoke(new AttackCommand(_model.CurrentValue));
        }
    }
}