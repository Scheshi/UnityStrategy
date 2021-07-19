namespace Abstractions
{
    public interface ICommandExecutor<T>: ICommandExecutor where T: ICommand
    {
        
    }
}