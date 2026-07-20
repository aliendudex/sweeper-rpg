using System.Numerics;

namespace EngineGDI.Src.SweeperRpg
{
    public class Transform
    {
        public Vector2 Position { get; set; }

        public Vector2 Rotation { get; set; }

        public Vector2 Scale { get; set; }

        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = Vector2.Zero;
            Scale = new Vector2(1, 1);
        }
    }
}
