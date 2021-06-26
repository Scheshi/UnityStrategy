using System;
using Abstractions;

namespace Commands.Creators
{
    public class CancelCommandCreator: CommandCreator<ICancelCommand>
    {
        protected override void CreateCommand(Action<ICancelCommand> onCallBack)
        {
            //
        }
    }
}