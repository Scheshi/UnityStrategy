using System.Threading;
using System.Threading.Tasks;
using Abstractions;

public static class AsyncUtils
{
    public struct VoidObject { }
        
    public static Task<T> AsTask<T>(this IAwaitable<T> awaitable)
    {
        return Task.Run(async () => await awaitable);
    }
        
    public  static async Task<T> WithCancellation<T>(this IAwaitable<T> awaitable, CancellationToken token)
    {
        return await WithCancellation(awaitable.AsTask(), token);
    }

    public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken token)
    {
        var cancelTask = new TaskCompletionSource<VoidObject>();
        using (token.Register(t => ((TaskCompletionSource<VoidObject>)t).TrySetResult(new VoidObject()), cancelTask))
        {
            var any = await Task.WhenAny(task, cancelTask.Task);
            if (any == cancelTask.Task)
            {
                token.ThrowIfCancellationRequested();
            }
        }
        return await task;
    }
}