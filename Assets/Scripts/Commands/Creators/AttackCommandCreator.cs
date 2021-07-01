using System;
using Abstractions;
using Zenject;

namespace Commands.Creators
{
    public sealed class AttackCommandCreator: CommandCreator<IAttackCommand>
    {
        [Inject] private ScriptableModel<IAttackable> _model;
        private Action<IAttackCommand> _action;

        protected override async void CreateCommand(Action<IAttackCommand> onCallBack)
        {
            var target = await _model;
            _action?.Invoke(new AttackCommand(target));
        }
    }
}