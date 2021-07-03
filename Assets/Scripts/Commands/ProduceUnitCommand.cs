using System;
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
        private GameObject _unitPrefab;

        private int _timeToSpawnMilliseconds = 10000;
        private int _currentToSpawn;
        private int _millisecondsPerRefresh = 100;

        public Vector3 SpawnPosition { get; set; }
        
        public async void InstantiateUnit()
        {
            _currentToSpawn = _timeToSpawnMilliseconds;
            Debug.Log("Starting create unit");
            await Task.Run(async () =>
            {
                while (_currentToSpawn > 0)
                {
                    _currentToSpawn -= _millisecondsPerRefresh;
                    await Task.Delay(_millisecondsPerRefresh);
                }
                _currentToSpawn = _timeToSpawnMilliseconds;
            });
            
            Debug.Log("Create unit");
            var unit = Object.Instantiate(_unitPrefab, SpawnPosition, Quaternion.identity);
            unit.GetComponent<ISelectableItem>().SetExecutors(new MoveCommandExecutor(unit.GetComponent<NavMeshAgent>()), new AttackCommandExecutor(unit.transform), new PatrolCommandExecutor(unit.GetComponent<NavMeshAgent>()));
        }
    }
}