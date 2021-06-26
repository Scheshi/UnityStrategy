using Abstractions;
using UnityEngine;
using Utils;

namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        [InjectAsset("PT_Medieval_Male_Peasant_01_e")]
        private GameObject _unitPrefab;
        public GameObject InstantiateUnit()
        {
            Debug.Log("Create unit");
            return _unitPrefab;
        }
    }
}