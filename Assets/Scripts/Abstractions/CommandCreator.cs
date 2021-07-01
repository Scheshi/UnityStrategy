using System;
using System.Threading;
using UnityEngine;

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

    public abstract class CommandCreatorWithCancelled<T, TParam> : CommandCreator<T> where T : ICommand
    {
        private CancellationTokenSource _tokenSource;
        private IAwaitable<TParam> _awaitable;

        public void SetAwaitable(IAwaitable<TParam> awaitable)
        {
            _awaitable = awaitable;
        }
        
        protected override async void CreateCommand(Action<T> onCallBack)
        {
            _tokenSource = new CancellationTokenSource();
            try
            {
                var result = await _awaitable.WithCancellation(_tokenSource.Token);
                onCallBack?.Invoke(GetCommand(result));
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Command " + nameof(T) + " is cancelled");
            }
        }

        protected abstract T GetCommand(TParam result);
    }
}