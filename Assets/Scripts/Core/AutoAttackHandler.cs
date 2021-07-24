using System;
using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using Commands;
using UniRx;

namespace Core
{
    public class AutoAttackHandler
    {
        private IDisposable _updater;
        private AutoAttackHandler()
        {
            _updater = Observable.EveryUpdate().Subscribe(x => AttackParallel());
        }

        private void AttackParallel()
        {
            Parallel.ForEach(UnitManager.Units, DoAttack);
        }

        private void DoAttack(IUnit unit)
        {
            var enemy = UnitManager.Units
                .Where(currentEnemy =>
                    (currentEnemy.CurrentPosition - unit.CurrentPosition).sqrMagnitude <= unit.VisionRange * unit.VisionRange &&
                    currentEnemy.Team != unit.Team)
                .GroupBy(group => (group.CurrentPosition - group.CurrentPosition).sqrMagnitude)
                .FirstOrDefault()?
                .ToArray()[0];
            if (enemy != null)
            {
                if (unit.CommandQueue.GetCommandImportance < 2)
                {
                    unit.CommandQueue.Clear();
                    unit.CommandQueue.EnqueueCommand(new AttackCommand(enemy));
                }
            }
        }
    }
}