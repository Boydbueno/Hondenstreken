using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace HondenStreken
{
    class MouseGameElement : DrawableGameElement
    {

        #region Fields
        private bool _draggable;
        private bool _dragging;
        #endregion

        #region Properties
        public bool IsHovering
        {
            get
            {
                return (Rectangle.Contains(Game1._currentMouseState.X, Game1._currentMouseState.Y));
            }
        }

        /// <summary>
        /// Is there a mousedown followed by a mouseup event on the element
        /// </summary>
        public bool IsClicked
        {
            get
            {
                return (IsHovering && Game1._previousMouseState.LeftButton == ButtonState.Released && Game1._currentMouseState.LeftButton == ButtonState.Pressed);
            }
        }

        public bool IsDraggable
        {
            get
            {
                return _draggable;
            }
            set
            {
                _draggable = value;
            }
        }

        public bool IsDragging
        {
            get
            {
                return _dragging;
            }
        }
        #endregion

        #region Constructors
        public MouseGameElement(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public MouseGameElement(Game game, Texture2D texture, Vector2 position)
            : base(game, texture, position)
        {
        }

        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (IsMouseDownOnObject())
            {
                _dragging = true;
            }

            if (Game1._currentMouseState.LeftButton == ButtonState.Released)
            {
                _dragging = false;
            }

            if (IsDragging && IsDraggable)
            {
                MoveWithMouse();
            }
            else
            {
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// Make the object's position equal to the mouse position
        /// </summary>
        public void FollowMouse()
        {
            Position = new Vector2(Game1._currentMouseState.X, Game1._currentMouseState.Y);
        }

        /// <summary>
        /// Move the object relative to the mouse movement
        /// </summary>
        public void MoveWithMouse()
        {
            Position += new Vector2(Game1._currentMouseState.X - Game1._previousMouseState.X, Game1._currentMouseState.Y - Game1._previousMouseState.Y);
        }

        protected bool IsMouseDownOnObject()
        {
            return (Game1._previousMouseState.LeftButton == ButtonState.Pressed && Game1._currentMouseState.LeftButton == ButtonState.Pressed
                    && IsHovering);
        }

        /// <summary>
        /// Check wether the object has hit another object
        /// Where hitting is when the center of the other object enters the rectangle
        /// </summary>
        public bool HasHitObject(DrawableGameElement element)
        {
               return (Rectangle.Contains((int)element.Center.X, (int)element.Center.Y));
        }
        #endregion
    }
}
