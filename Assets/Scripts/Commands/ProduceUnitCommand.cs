using Abstractions;
using UnityEngine;

namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        public GameObject InstantiateUnit()
        {
            Debug.Log("Create unit");
            return null;
        }
    }
}