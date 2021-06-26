using System;
using Abstractions;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Models/SelectableModel")]
    public class SelectableModel: ScriptableObject
    {
        private Action<ISelectableItem> _onSetItem;
        private ISelectableItem _value;
        public ISelectableItem Value => _value;

        public void SelectItem(ISelectableItem value)
        {
            if (value != null)
            {
                _value?.Unselect();
                _value = value;
                _value.Select();
                _onSetItem.Invoke(value);
            }
        }

        public void SubscriptionOnSelect(Action<ISelectableItem> onSetItem)
        {
            _onSetItem += onSetItem;
        }

        public void UnsubscriptionOnSelect(Action<ISelectableItem> onSetItem)
        {
            _onSetItem -= onSetItem;
        }
    }
}