using Abstractions;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Models/CancellationModel")]
    public class CancellationModel: ScriptableModel<bool>
    {
        public override void SetValue(bool value)
        {
            base.SetValue(value);
            if (!value)
            {
                SetValue(true);
            }
        }
    }
}