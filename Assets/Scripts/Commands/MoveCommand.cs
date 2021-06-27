using Abstractions;
using UnityEngine;

namespace Commands
{
    public class MoveCommand: IMoveCommand
    {
        private Vector3 _to;
        public MoveCommand(Vector3 to)
        {
            _to = to;
        }
        
        public void Move(Transform transform)
        {
            Debug.Log("Moving from " + transform.position +" to " + _to);
        }
    }
}