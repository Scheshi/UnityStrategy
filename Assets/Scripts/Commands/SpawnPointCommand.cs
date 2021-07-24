using UnityEngine;

namespace Commands
{
    public class SpawnPointCommand: ISpawnPointCommand
    {
        public SpawnPointCommand(Vector3 position)
        {
            Position = position;
        }


        public int CommandImportance { get; } = 5;

        public void Cancel()
        {
            Position = Vector3.zero;
        }

        public Vector3 Position { get; set; }
    }
}