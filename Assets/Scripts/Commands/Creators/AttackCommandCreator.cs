using System;
using Abstractions;

namespace Commands.Creators
{
    public sealed class AttackCommandCreator: CommandCreator<IAttackCommand>
    {
        protected override void CreateCommand(Action<IAttackCommand> onCallBack)
        {
            onCallBack.Invoke(new AttackCommand());
        }
    }
}