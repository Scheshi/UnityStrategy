using Abstractions;
using UnityEngine;


namespace Utils
{
    [CreateAssetMenu(menuName = "Models/SelectableModel")]
    public class SelectableModel: ScriptableModel<ISelectableItem>
    {
        public override void SetValue(ISelectableItem value)
        {
            if (value != null)
            {
                CurrentValue?.Unselect();
                base.SetValue(value);
                CurrentValue.Select();
            }
        }
    }
}