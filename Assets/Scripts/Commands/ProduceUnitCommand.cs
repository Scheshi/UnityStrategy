using Abstractions;
using UnityEngine;

namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        private GameObject _unitPrefab;
        public GameObject InstantiateUnit()
        {
            Debug.Log("Create unit");
            return _unitPrefab;
        }
    }
}