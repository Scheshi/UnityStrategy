using System;
using Abstractions;

namespace Commands.Creators
{
    public sealed class CancelCommandCreator: CommandCreator<ICancelCommand>
    {
        protected override void CreateCommand(Action<ICancelCommand> onCallBack, bool isComplete = false)
        {
            //
        }
    }
}