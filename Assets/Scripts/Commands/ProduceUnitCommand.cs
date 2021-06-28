using Abstractions;
using UnityEngine;
using Utils;

namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        [InjectAsset("PT_Medieval_Male_Peasant_01_e")]
        private GameObject _unitPrefab;

        public Vector3 SpawnPosition { get; set; }
        
        public void InstantiateUnit()
        {
            Debug.Log("Create unit");
            var unit = Object.Instantiate(_unitPrefab, SpawnPosition, Quaternion.identity);
            unit.GetComponent<ISelectableItem>().SetExecutors(new MoveCommandExecutor(unit), new AttackCommandExecutor(unit.transform));
        }
    }
}