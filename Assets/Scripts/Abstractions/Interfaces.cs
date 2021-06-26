using System;

namespace Abstractions
{
    public interface ISelectableItem
    {
        ICommandExecutor[] Executors { get; }
        event Action OnSelect;
        string Name { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void Select();
        void Unselect();
    }

    public interface IBuilding : ISelectableItem
    {
        
    }

    public interface IUnit : ISelectableItem
    {
        
    }
}
