using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class ScriptableModel<T>: ScriptableObject
    {
        public event Action OnChangeValue = () => { };
        public virtual T CurrentValue { get; private set; }

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnChangeValue.Invoke();
        }
    }
}