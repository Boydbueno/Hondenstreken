using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace HondenStreken
{
    class AnimatedGameElement : DrawableGameElement
    {
        #region Fields
        protected int _currentFrame;
        protected int _spriteRow;
        private float _timer = 0.08f;
        private const float TIMER = 0.08f;
        #endregion

        #region Properties
        public int SpriteWidth { get; private set; }
        public int SpriteHeight { get; private set; }
        #endregion

        #region Constructors
        public AnimatedGameElement(Game game, Texture2D texture, Vector2 position, int spriteWidth, int spriteHeigth) 
            : base(game, texture, position)
        {
            SpriteWidth = spriteWidth;
            SpriteHeight = spriteHeigth;
            _currentFrame = 0;
            _spriteRow = 0;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {

                _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer < 0)
                {

                    //Timer expired, execute action
                    _currentFrame++;
                    if (_currentFrame * (int)SpriteWidth >= Texture.Width)
                    {
                        _currentFrame = 0;
                    }
                    _timer = TIMER; //Reset Timer

                }
                Rectangle sourceRectangle = new Rectangle(_currentFrame * (int)SpriteWidth, _spriteRow * (int)SpriteHeight, (int)SpriteWidth, (int)SpriteHeight);
                spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);

            }
        }
        #endregion

    }
}
