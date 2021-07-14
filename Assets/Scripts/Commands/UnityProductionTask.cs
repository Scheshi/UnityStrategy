using Abstractions;
using UnityEngine;

namespace Commands
{
    public class UnityProductionTask: IProductionTask
    {
        public string Name { get; }
        public Sprite Icon { get; }
        public float TimeProduceMax { get; }
        public float TimeProduce { get; }
        public GameObject UnitObject { get; }
    }
}