using System;
using UnityEngine;
using Utils;

namespace Abstractions
{
    public abstract class ScriptableModel<T>: ScriptableObject, IAwaitable<T>
    {
        
        public event Action OnChangeValue = () => { };
        public virtual T CurrentValue { get; private set; }

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnChangeValue.Invoke();
        }

        public IAwaiter<T> GetAwaiter() => new AsyncNotification<T>(this);
    }
}