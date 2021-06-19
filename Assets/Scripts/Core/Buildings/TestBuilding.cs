using Abstractions;
using UnityEngine;


namespace Core.Buildings
{
    public class TestBuilding: MonoBehaviour, ISelectableItem
    {
        [SerializeField] private string itemName;

        public string Name => itemName;

        public void Select()
        {
            //TODO:
        }
    }
}