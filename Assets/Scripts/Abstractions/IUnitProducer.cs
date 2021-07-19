using UniRx;

namespace Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IProductionTask> Queue { get; }
        void Cancel(int index);
    }
}