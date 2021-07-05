using System;
using Abstractions;

namespace Utils
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

        public bool IsCompleted => _isComplete;
        public TAwait GetResult() => _result;

        public void Dispose()
        {
            _onCompletedAction = null;
            _model = null;
        }
    }
}