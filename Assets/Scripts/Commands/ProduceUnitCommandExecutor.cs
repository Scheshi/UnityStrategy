using System.Threading.Tasks;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Object = UnityEngine.Object;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>, IUnitProducer, ITick
    {
        private ReactiveCollection<IProductionTask> _queue = new ReactiveCollection<IProductionTask>();
        private int _millisecondsPerRefresh = 100;
        private IBuilding _building;

        public ProduceUnitCommandExecutor(IBuilding building)
        {
            _building = building;
            Observable.EveryUpdate().Subscribe(_ => Tick());
        }
        
        public void Tick()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeProduce -= Time.deltaTime;
            if (innerTask.TimeProduce <= 0)
            {
                var unit = Object.Instantiate(innerTask.UnitObject, _building.UnitSpawnPosition, Quaternion.identity);
                unit.GetComponent<ISelectableItem>().SetExecutors(new MoveCommandExecutor(unit.GetComponent<NavMeshAgent>()), new AttackCommandExecutor(unit.GetComponent<IAttacker>()), new PatrolCommandExecutor(unit.GetComponent<NavMeshAgent>()));
                RemoveTaskAtIndex(0);
            }
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

        
        protected override Task ExecuteTypeCommand(ICreateUnitCommand command)
        {
            Enqueue(command);
            return Task.Run(() => { });
        }

        private void Enqueue(ICreateUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }

        public IReadOnlyReactiveCollection<IProductionTask> Queue => _queue;
    }
}