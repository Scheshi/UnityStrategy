using System.Collections.Concurrent;
using System.Linq;
using Abstractions;

namespace Core
{
    public static class UnitManager
    {
        private static ConcurrentBag<IUnit> _units = new ConcurrentBag<IUnit>();

        public static ConcurrentBag<IUnit> Units => _units;
        public static void RegisterUnit(IUnit unit)
        {
            _units.Add(unit);
        }

        public static void UnregisterUnit(IUnit unit)
        {
            _units = new ConcurrentBag<IUnit>(_units.Except(new[] {unit}));
        }
    }
}