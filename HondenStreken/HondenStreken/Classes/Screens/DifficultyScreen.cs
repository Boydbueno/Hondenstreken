using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace HondenStreken
{
    class DifficultyScreen : Screen
    {

        #region Properties
        public MouseGameElement EasyModeButton { get; private set; }
        public MouseGameElement NormalModeButton { get; private set; }
        public MouseGameElement HardModeButton { get; private set; }

        public AudioEffect DifficultyTutorial { get; private set; }
        #endregion

        #region Constructors
        public DifficultyScreen(Game game, SpriteFont _arial)
            : base(game)
        {

            DifficultyTutorial = new AudioEffect(Game1._soundEffects["DifficultySelect"]);

            DrawableGameElement _backgroundRoom = new DrawableGameElement(game, Game1._textures["background"], new Vector2(-Game1._textures["background"].Width / 2, 0));
            GameElements.Add(_backgroundRoom);
            
            EasyModeButton = new MouseGameElement(game, Game1._textures["difficulty_one"], new Vector2(Center.X - (int)Game1._textures["difficulty_one"].Width * 2 + 50, 200));
            NormalModeButton = new MouseGameElement(game, Game1._textures["difficulty_two"], new Vector2(Center.X - (int)Game1._textures["difficulty_two"].Width / 2, 200));
            HardModeButton = new MouseGameElement(game, Game1._textures["difficulty_three"], new Vector2(Center.X + (int)Game1._textures["difficulty_three"].Width - 50, 200));

            GameElements.Add(EasyModeButton);
            GameElements.Add(NormalModeButton);
            GameElements.Add(HardModeButton);
        }
        #endregion

    }
}
