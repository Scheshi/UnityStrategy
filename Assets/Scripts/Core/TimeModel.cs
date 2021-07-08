using System;
using Abstractions;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TimeModel: ITimeModel, ITickable
    {
        public IObservable<int> TimeTick { get; }
        private float _time;
        
        public void Tick()
        {
            _time += Time.deltaTime;
        }
    }
}