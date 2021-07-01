using System.Runtime.CompilerServices;

namespace Abstractions
{
    public interface IAwaiter<TParam>: INotifyCompletion
    {
        bool IsCompleted { get; }
        TParam GetResult();
    }

    public interface IAwaitable<TResult>
    {
        IAwaiter<TResult> GetAwaiter();
    }
}