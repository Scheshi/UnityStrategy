using System;

namespace Abstractions
{
    public interface ISelectableItem
    {
        event Action OnSelect;
        string Name { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void Select();
    }

    public interface IBuilding : ISelectableItem
    {
        
    }

    public interface IUnit : ISelectableItem
    {
        
    }
}
