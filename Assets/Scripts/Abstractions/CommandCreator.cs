using System;
using System.Threading;
using UnityEngine;
using Utils;
using Zenject;

namespace Abstractions
{
    public abstract class CommandCreator<T> where T: ICommand
    {
        public void Create(ICommandExecutor executor, Action<T> onCallBackHandle, bool isComplete = false)
        {
            if (executor is CommandExecutorBase<T>)
            {
                CreateCommand(onCallBackHandle, isComplete);
            }
        }

        protected abstract void CreateCommand(Action<T> onCallBack, bool isComplete = false);
    }

    public abstract class CommandCreatorWithCancelled<T, TParam> : CommandCreator<T> where T : ICommand
    {
        private CancellationTokenSource _tokenSource;
        private IAwaitable<TParam> _awaitable;
        [Inject] private CancellationModel _cancellation;

        protected CancellationModel CancellationModel => _cancellation;

        public void SetAwaitable(IAwaitable<TParam> awaitable)
        {
            _awaitable = awaitable;
        }
        
        protected override async void CreateCommand(Action<T> onCallBack, bool isComplete = false)
        {
            _tokenSource = new CancellationTokenSource();
            if (!isComplete)
            {
                try
                {
                    var result = await _awaitable.AsTask().WithCancellation(_tokenSource.Token);
                    onCallBack?.Invoke(GetCommand(result));
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("Command " + nameof(T) + " is cancelled");
                }
            }
            else if(_awaitable is ScriptableModel<TParam> model)
            {
                onCallBack?.Invoke(GetCommand(model.CurrentValue));
            }
        }

        public void Cancel()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = null;
            }
        }

        protected abstract T GetCommand(TParam result);
    }
}