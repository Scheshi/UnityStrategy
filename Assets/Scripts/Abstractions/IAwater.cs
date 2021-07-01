using System.Runtime.CompilerServices;

namespace Abstractions
{
    public interface IAwaiter<T>: INotifyCompletion
    {
        bool IsComplete { get; }
        T GetResult();
    }

    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}