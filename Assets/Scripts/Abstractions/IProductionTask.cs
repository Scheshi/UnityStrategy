using UnityEngine;

namespace Abstractions
{
    public interface IProductionTask
    {
        string Name { get; }
        Sprite Icon { get; }
        float TimeProduceMax { get; }
        float TimeProduce { get; }
    }
}