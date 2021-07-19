namespace Abstractions
{
    public interface ICommandQueue
    {
        void EnqueueCommand(ICommand command);
        void Clear();
    }
}