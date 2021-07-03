using Abstractions;
using UnityEngine;


namespace Utils
{
    [CreateAssetMenu(menuName = "Models/" + nameof(PositionModel))]
    public class PositionModel : ScriptableModel<Vector3>
    {
        
    }
}