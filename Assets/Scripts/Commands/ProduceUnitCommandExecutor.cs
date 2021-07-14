using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>, IUnitProducer
    {
        private ReactiveCollection<IProductionTask> _queue = new ReactiveCollection<IProductionTask>();
        private int _millisecondsPerRefresh = 100;
        public Vector3 SpawnPosition { get;}

        public ProduceUnitCommandExecutor(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
        
        public async void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];
            while (innerTask.TimeProduce  > 0)
            {
                if (Time.timeScale > 0)
                {
                    innerTask.TimeProduce -= _millisecondsPerRefresh;
                }
                await Task.Delay(_millisecondsPerRefresh);
            }

            var unit = Object.Instantiate(innerTask.UnitObject, SpawnPosition, Quaternion.identity);
                unit.GetComponent<ISelectableItem>().SetExecutors(new MoveCommandExecutor(unit.GetComponent<NavMeshAgent>()), new AttackCommandExecutor(unit.transform), new PatrolCommandExecutor(unit.GetComponent<NavMeshAgent>()));
            RemoveTaskAtIndex(0);
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);

        private void RemoveTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count - 1);
        }

        
        protected override void ExecuteTypeCommand(ICreateUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }

        public IReadOnlyReactiveCollection<IProductionTask> Queue => _queue;
    }
}