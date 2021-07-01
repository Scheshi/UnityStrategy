using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class ScriptableModel<T>: ScriptableObject
    {
        public class AsyncNotification: IAwaiter<T>
        {
            private ScriptableModel<T> _model;
            private bool _isComplete;
            private Action _onCompletedAction;
            
            public AsyncNotification(ScriptableModel<T> model)
            {
                _model = model;
                model.OnChangeValue += OnNotification;
            }

            private void OnNotification()
            {
                _onCompletedAction?.Invoke();
                _isComplete = true;
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
            public T GetResult()
            {
                return _model.CurrentValue;
            }
        }
        public event Action OnChangeValue = () => { };
        public virtual T CurrentValue { get; private set; }

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnChangeValue.Invoke();
        }
    }
}