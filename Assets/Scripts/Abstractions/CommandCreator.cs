using System;
using System.Threading;
using UnityEngine;
using Zenject;

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
        [Inject(Id = "Command")] protected CancellationTokenSource CancellationCommandTokenSource;

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

        public void Cancel()
        {
            _tokenSource?.Cancel();
        }

        protected abstract T GetCommand(TParam result);
    }
}