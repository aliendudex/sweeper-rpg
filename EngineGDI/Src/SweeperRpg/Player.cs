using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace EngineGDI.Src.SweeperRpg
{
    public class Player : Node, IDamageable, IResettable, IDrawableEntity
    {
        private Point position;
        public Point Position
        {
            get { return position; }
        }

        private readonly Transform transform = new Transform();
        public Transform Transform
        {
            get { return transform; }
        }

        private Point start = new Point();

        private readonly Image tile;
        public Image Tile
        {
            get { return tile; }
        }

        private readonly Renderer renderer;

        private readonly int maxHealth = 8;
        private int health;
        public int Hp
        {
            get { return health; }
        }

        public event Action<int> OnHealthChanged;

        private readonly Collisioner collisioner;
        public Collisioner Collisioner
        {
            get { return collisioner; }
        }

        public Player(int x, int y)
        {
            position = new Point(x, y);

            transform.Position = new Vector2(position.X * 32, position.Y * 32);

            tile = TileMap.LoadSprite(path: "Assets/32rogues/rogues.png", row: 2, column: 2);

            renderer = new Renderer(texture: tile, baseSize: new Size(32, 32));

            collisioner = new Collisioner(
                transform: transform,
                size: new Size(32, 32),
                brushColor: Color.DarkBlue
            );
        }

        public void SetStart(int x, int y)
        {
            start.X = x;
            start.Y = y;

            Reset();
        }

        public void Reset()
        {
            position = start;

            transform.Position = new Vector2(position.X * 32, position.Y * 32);

            health = maxHealth;

            collisioner.Reset();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            OnHealthChanged?.Invoke(health);
        }

        public bool IsDead()
        {
            return health < 0;
        }

        public override void Input()
        {
            bool changed = false;
            Point previousPosition = position;

            if (Engine.OnKeyDown(Keys.W))
            {
                position.Y--;
                changed = true;
            }

            if (Engine.OnKeyDown(Keys.A))
            {
                position.X--;
                changed = true;
            }

            if (Engine.OnKeyDown(Keys.S))
            {
                position.Y++;
                changed = true;
            }

            if (Engine.OnKeyDown(Keys.D))
            {
                position.X++;
                changed = true;
            }

            if (changed)
            {
                if (LevelManager.Instance.IsWithinLimits(position))
                {
                    transform.Position = new Vector2(position.X * 32, position.Y * 32);

                    CollisionManager.Instance.ValidateCollitions();

                    LevelManager.Instance.CheckVictoryCondition();
                }
                else
                {
                    position = previousPosition;
                }
            }
        }

        public override void Draw()
        {
            renderer.Draw(transform);
        }
    }
}
