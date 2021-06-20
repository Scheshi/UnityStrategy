using System;

namespace Abstractions
{
    public interface ISelectableItem
    {
        event Action OnSelect;
        string Name { get; }
        void Select();
    }

    public interface IBuilding : ISelectableItem
    {
        
    }

    public interface IUnit : ISelectableItem
    {
        
    }
}
