using Abstractions;
using UnityEngine;

namespace Commands
{
    public class UnitProductionTask: IProductionTask
    {
        public UnitProductionTask(float productionTime, Sprite icon, GameObject unitPrefab, string unitName)
        {
            TimeProduceMax = productionTime;
            TimeProduce = productionTime;
            Name = unitName;
            Icon = icon;
            UnitObject = unitPrefab;
        }

        public string Name { get; }
        public Sprite Icon { get; }
        public float TimeProduceMax { get; }
        public float TimeProduce { get; set; }
        public GameObject UnitObject { get; }
    }
}