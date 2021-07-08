using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TimeModel: ITimeModel, ITickable
    {
        public IObservable<int> TimeTick => _timeProperty.Select(x => (int) x);
        private ReactiveProperty<float> _timeProperty = new ReactiveProperty<float>();

        public void Tick()
        {
            _timeProperty.Value += Time.deltaTime;
        }
    }
}