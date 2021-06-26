using System;
using Abstractions;

namespace Commands.Creators
{
    public class AttackCommandCreator: CommandCreator<IAttackCommand>
    {
        protected override void CreateCommand(Action<IAttackCommand> onCallBack)
        {
            //
        }
    }
}