using System.Drawing;
using EngineGDI.Src.SweeperRpg;

namespace EngineGDI.Src
{
    public class Collisioner : Node
    {
        private Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
        }

        private readonly Transform transform;
        private readonly Pen pen;
        private readonly Brush brush;

        private bool debugCollisioned = false;

        public Collisioner(Transform transform, Size size, Color brushColor)
        {
            this.transform = transform;

            rect = new Rectangle(
                location: new Point(
                    x: (int)transform.Position.X + (size.Width / 4),
                    y: (int)transform.Position.Y + (size.Height / 4)
                ),
                size: new Size(width: size.Width / 2, height: size.Height / 2)
            );

            pen = new Pen(Color.Red);
            brush = new SolidBrush(brushColor);
        }

        private void UpdateRectangle()
        {
            rect.X = (int)transform.Position.X + (rect.Width / 2);
            rect.Y = (int)transform.Position.Y + (rect.Height / 2);
        }

        public void Reset()
        {
            UpdateRectangle();
            debugCollisioned = false;
        }

        public void OnCollisionIn()
        {
            debugCollisioned = true;
        }

        public void OnCollisionOut()
        {
            debugCollisioned = false;
        }

        public bool CheckCollision(Collisioner element)
        {
            UpdateRectangle();
            element.UpdateRectangle();

            return rect.X + rect.Width >= element.rect.X
                && rect.X <= element.rect.X + element.rect.Width
                && rect.Y + rect.Height >= element.rect.Y
                && rect.Y <= element.rect.Y + element.rect.Height;
        }

        public override void Draw()
        {
            UpdateRectangle();

            Engine.DrawCollision(pen: pen, rect: rect, brush: debugCollisioned ? brush : null);
        }
    }
}
