using System;
using UnityEngine;

namespace Utils
{
    public class PositionModel : ScriptableObject
    {
        public Vector3 ClickPosition { get; private set; }
        public event Action OnSetPosition = () => { };

        public void SetClickPosition(Vector3 position)
        {
            ClickPosition = position;
            OnSetPosition.Invoke();
        }
    }
}