using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HondenStreken
{
    class PauseScreen : Screen
    {
        #region Properties
        public Button ResumeButton { get; private set; }
        public Button ExitButton { get; private set; }
        #endregion

        #region Constructors
        public PauseScreen(Game game)
            : base(game)
        {
            ResumeButton = new Button(game, Game1._arial, "Verder spelen", new Vector2(Center.X, 150), 20);
            ExitButton = new Button(game, Game1._arial, "Sluit het spel", new Vector2(Center.X, 250), 20);

            GameElements.Add(ResumeButton);
            GameElements.Add(ExitButton);
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        #endregion

    }
}
