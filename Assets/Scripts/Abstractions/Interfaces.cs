namespace Abstractions
{
    public interface ISelectableItem
    {
        string Name { get; }
        void Select();
    }
}
