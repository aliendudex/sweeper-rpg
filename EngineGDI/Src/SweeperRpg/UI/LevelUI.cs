using System.Drawing;

namespace EngineGDI.Src.SweeperRpg.UI
{
    public class LevelUI : Node
    {
        private readonly Player player;
        private readonly Font font;
        private readonly Image heart = Image.FromFile("Assets/Imgs/heart.png");
        private int level;

        public LevelUI(Font font, Player player)
        {
            this.font = font;
            this.player = player;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }

        public override void Draw()
        {
            Engine.DrawImage(texture: player.Tile, x: 30, y: 540, scaleX: 2, scaleY: 2);

            Engine.DrawText("Player", font, Brushes.White, new Point(120, 530));
            //Engine.DrawText($"HP: {player.Hp}", font, Brushes.White, new Point(120, 620));
            int startX = 120;
            int startY = 565;
            int spacing = 40;

            for (int i = 0; i < player.Hp; i++)
            {
                Engine.DrawImage(
                    texture: heart,
                    x: startX + (i * spacing),
                    y: startY,
                    scaleX: 0.15f,
                    scaleY: 0.15f
                );
            }

            Engine.DrawText($"Level: {level}", font, Brushes.White, new Point(120, 600));
            Engine.DrawText(
                $"Score: {GameManager.Instance.Score}",
                font,
                Brushes.White,
                new Point(120, 640)
            );
        }
    }
}
