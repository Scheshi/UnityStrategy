using System;

namespace Abstractions
{
    public abstract class CommandCreator<T> where T: ICommand
    {
        public void Create(ICommandExecutor executor, Action<T> onCallBackHandle)
        {
            if (executor is CommandExecutorBase<T>)
            {
                CreateCommand(onCallBackHandle);
            }
        }

        protected abstract void CreateCommand(Action<T> onCallBack);
    }
}