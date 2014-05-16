using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace HondenStreken
{
    class TitleScreen : Screen
    {
        #region Properties
        public MouseGameElement StartButton { get; private set; }
        public MouseGameElement ExitButton { get; private set; }
        #endregion

        #region Constructors
        public TitleScreen(Game game, SpriteFont _arial)
            : base(game)
        {
            DrawableGameElement _backgroundRoom = new DrawableGameElement(game, Game1._textures["background_menu"], new Vector2(0, 0));
            GameElements.Add(_backgroundRoom);

            StartButton = new MouseGameElement(game, Game1._textures["start"], new Vector2(150, 280));
            ExitButton = new MouseGameElement(game, Game1._textures["stop"], new Vector2(150, 380));

            GameElements.Add(StartButton);
            GameElements.Add(ExitButton);
        }
        #endregion
    }
}
