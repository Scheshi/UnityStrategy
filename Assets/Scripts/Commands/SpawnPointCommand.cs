using UnityEngine;

namespace Commands
{
    public class SpawnPointCommand: ISpawnPointCommand
    {
        public SpawnPointCommand(Vector3 position)
        {
            Position = position;
        }
        
        
        public void Cancel()
        {
            Position = Vector3.zero;
        }

        public Vector3 Position { get; set; }
    }
}