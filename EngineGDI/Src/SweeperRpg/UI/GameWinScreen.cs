using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineGDI.Src.SweeperRpg.UI
{
    public class GameWinScreen : Node
    {
        private readonly Image backgroundImg = Image.FromFile("Assets/Imgs/gameWin.png");
        private readonly Font font;

        public GameWinScreen(Font font)
        {
            this.font = font;
        }

        public override void Input()
        {
            if (Engine.OnKeyDown(Keys.Enter))
            {
                GameManager.Instance.OnMainMenu();
            }
        }

        public override void Draw()
        {
            Engine.DrawImage(texture: backgroundImg, x: 0, y: 0);

            Engine.DrawText("> Main Menu", font, Brushes.White, new Point(400, 440));
        }
    }
}
