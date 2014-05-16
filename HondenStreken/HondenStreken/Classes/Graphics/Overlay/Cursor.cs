using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HondenStreken
{
    class Cursor : MouseGameElement
    {

        #region Constructors
        /// <summary>
        /// To create a cursor that follows the mouse
        /// </summary>
        public Cursor(Game game, Texture2D texture) 
            : base(game, texture, Vector2.Zero)
        {
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            FollowMouse();
        }
        #endregion
    }
}
