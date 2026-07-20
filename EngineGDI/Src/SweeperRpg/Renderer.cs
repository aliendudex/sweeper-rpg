using System.Drawing;

namespace EngineGDI.Src.SweeperRpg
{
    public class Renderer
    {
        public Image Texture { get; set; }

        public Size BaseSize { get; set; }

        public Renderer(Image texture, Size baseSize)
        {
            Texture = texture;
            BaseSize = baseSize;
        }

        public void Draw(Transform transform)
        {
            Engine.DrawImage(
                texture: Texture,
                x: (int)transform.Position.X,
                y: (int)transform.Position.Y
            );
        }
    }
}
