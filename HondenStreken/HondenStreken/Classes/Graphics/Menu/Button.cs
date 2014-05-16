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
    class Button : MouseGameElement
    {

        #region Fields
        private string _output;

        private SpriteFont _fontFamily;
        private Vector2 _fontPosition;
        private int _fontPadding;
        private Vector2 _fontLength;
        #endregion

        #region Constructors
        public Button(Game game, SpriteFont fontFamily, string output, Vector2 fontPosition, int fontPadding)
            : base(game, fontPosition)
        {
            _output = output;
            _fontFamily = fontFamily;
            _fontPosition = fontPosition;
            _fontPadding = fontPadding;
            Load();
        }
        #endregion

        #region Methods
        public void Load()
        {
            _fontLength = _fontFamily.MeasureString(_output);
            Texture = new Texture2D(Game.GraphicsDevice, (int)_fontLength.X + (_fontPadding * 2), (int)_fontLength.Y + (_fontPadding * 2));
            Texture.SetData(GetColorData(Texture));

            _position = _endPosition = new Vector2(_fontPosition.X - _fontPadding, _fontPosition.Y - _fontPadding);

            Rectangle = new Rectangle((int)_position.X - Texture.Width / 2, (int)_position.Y - Texture.Height / 2, Texture.Width, Texture.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw the rectangle and its text
            spriteBatch.Draw(Texture, _position, Rectangle, Color.White, 0f, _fontLength / 2, 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_fontFamily, _output, _fontPosition, Color.Black, 0f, _fontLength / 2, 1f, SpriteEffects.None, 0f);
        }

        private Color[] GetColorData(Texture2D texture)
        {
            Color[] data = new Color[texture.Bounds.Width * texture.Bounds.Height];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.White;
            }

            return data;
        }
        #endregion

    }
}
