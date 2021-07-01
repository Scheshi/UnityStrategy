using System.Runtime.CompilerServices;

namespace Abstractions
{
    public interface IAwaiter<T>: INotifyCompletion
    {
        bool IsComplete { get; }
        T GetResult();
    }
}