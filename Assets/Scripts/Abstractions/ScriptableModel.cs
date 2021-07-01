using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class ScriptableModel<T>: ScriptableObject, IAwaitable<T>
    {
        public class AsyncNotification<TAwait>: IAwaiter<TAwait>, IDisposable
        {
            private ScriptableModel<TAwait> _model;
            private bool _isComplete;
            private Action _onCompletedAction;
            private TAwait _result;
            
            public AsyncNotification(ScriptableModel<TAwait> model)
            {
                _model = model;
                model.OnChangeValue += OnNotification;
            }

            private void OnNotification()
            {
                _model.OnChangeValue -= OnNotification;
                _result = _model.CurrentValue;
                _isComplete = true;
                _onCompletedAction?.Invoke();
            }
            
            public void OnCompleted(Action continuation)
            {
                if (_isComplete)
                {
                    continuation?.Invoke();
                }
                else
                {
                    _onCompletedAction = continuation;
                }
            }

            public bool IsComplete => _isComplete;
            public TAwait GetResult() => _result;

            public void Dispose()
            {
                _onCompletedAction = null;
                _model = null;
            }
        }
        public event Action OnChangeValue = () => { };
        public virtual T CurrentValue { get; private set; }

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnChangeValue.Invoke();
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new AsyncNotification<T>(this);
        }
    }
}