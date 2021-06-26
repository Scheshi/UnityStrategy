using System;
using Abstractions;

namespace Commands.Creators
{
    public class MoveCommandCreator: CommandCreator<IMoveCommand>
    {
        protected override void CreateCommand(Action<IMoveCommand> onCallBack)
        {
            //
        }
    }
}