using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Object = UnityEngine.Object;


namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        [InjectAsset("PT_Medieval_Male_Peasant_01_e")]
        private IUnit _unitPrefab;

        private ProduceModel _produceValueModel;

        private int _timeToSpawnMilliseconds = 10000;
        private int _millisecondsPerRefresh = 100;

        public ProduceUnitCommand(ProduceModel produceModel)
        {
            _produceValueModel = produceModel;
        }

        public Vector3 SpawnPosition { get; set; }
        public float ProductionTime { get; } = 10;
        public Sprite Icon => _unitPrefab.Icon;
        public GameObject UnitPrefab => _unitPrefab.GameObject;
        public string UnitName => _unitPrefab.Name;

        public async void InstantiateUnit()
        {
           
        }
    }
}