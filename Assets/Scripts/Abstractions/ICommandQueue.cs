namespace Abstractions
{
    public interface ICommandQueue
    {
        public int GetCommandImportance { get; }
        void EnqueueCommand(ICommand command);
        void Clear();
    }
}