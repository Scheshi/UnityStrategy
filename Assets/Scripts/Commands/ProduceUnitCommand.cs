using Abstractions;
using UnityEngine;
using Utils;

namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        [InjectAsset("name")]private GameObject _unitPrefab;
        public GameObject InstantiateUnit()
        {
            Debug.Log("Create unit");
            return _unitPrefab;
        }
    }
}