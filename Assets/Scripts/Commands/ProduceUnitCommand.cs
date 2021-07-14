using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Object = UnityEngine.Object;


namespace Commands
{
    public class ProduceUnitCommand: ICreateUnitCommand
    {
        [InjectAsset("PT_Medieval_Male_Peasant_01_e")]
        private GameObject _unitPrefab;

        private ProduceModel _produceValueModel;

        private int _timeToSpawnMilliseconds = 10000;
        private int _millisecondsPerRefresh = 100;

        public ProduceUnitCommand(ProduceModel produceModel)
        {
            _produceValueModel = produceModel;
        }

        public Vector3 SpawnPosition { get; set; }
        
        public async void InstantiateUnit()
        {
            var currentToSpawn = _timeToSpawnMilliseconds;
            _produceValueModel.StartProduce();
            _produceValueModel.SetValue((float)currentToSpawn / _timeToSpawnMilliseconds);
            while (currentToSpawn > 0)
            {
                if (Time.timeScale > 0)
                {
                    currentToSpawn -= _millisecondsPerRefresh;
                    _produceValueModel.SetValue((float) currentToSpawn / _timeToSpawnMilliseconds);
                }
                await Task.Delay(_millisecondsPerRefresh);
            }
            _produceValueModel.EndProduce();
            
            var unit = Object.Instantiate(_unitPrefab, SpawnPosition, Quaternion.identity);
            unit.GetComponent<ISelectableItem>().SetExecutors(new MoveCommandExecutor(unit.GetComponent<NavMeshAgent>()), new AttackCommandExecutor(unit.transform), new PatrolCommandExecutor(unit.GetComponent<NavMeshAgent>()));
        }
    }
}