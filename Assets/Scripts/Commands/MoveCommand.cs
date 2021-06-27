using System;
using Abstractions;
using UnityEngine;


namespace Commands
{
    public class MoveCommand: IMoveCommand
    {
        private Vector3 _to;
        public event Action OnEndPath = () => { };
        public MoveCommand(Vector3 to)
        {
            _to = to;
        }
        
        public void Move(Transform transform)
        {
            if (Mathf.Abs(transform.position.x - _to.x) < 0.1f && Mathf.Abs(transform.position.y - _to.y) < 0.1f  &&
                Mathf.Abs(transform.position.z - _to.z) < 0.1f)
            {
                OnEndPath.Invoke();
                return;
            }
            transform.Translate((_to - transform.position).normalized * 3.0f * Time.deltaTime);
        }
    }
}