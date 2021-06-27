using Abstractions;
using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(menuName = "Models/"+nameof(TargetModel))]
    public class TargetModel: ScriptableModel<ISelectableItem>
    {
        
    }
}