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
    class DrawableGameElement : GameElement
    {
        #region Fields
        protected Vector2 _position;        
        protected Vector2 _endPosition;

        protected VisualEffect _effect;

        protected float _scale;
        protected float _rotation;
        
        protected bool _isMoving;

        protected Vector2 _speed;
        #endregion

        #region Properties
        public Texture2D Texture { get; set; }
        public Vector2 OriginalPosition { get; private set; }
        public Rectangle Rectangle { get; protected set; }
        public bool IsVisible { get; set; }

        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                Rectangle = new Rectangle((int)_position.X, (int)_position.Y, Texture.Width, Texture.Height);
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(_position.X + Texture.Width / 2, _position.Y + Texture.Height / 2);
            }
        }

        public bool HasReachedDestination
        {
            get
            {
                if (_isMoving && Position == _endPosition)
                {
                    _isMoving = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Constructors
        public DrawableGameElement(Game game, Vector2 position) 
            : base(game)
        {
            IsVisible = true;
            _scale = 1;
            _rotation = 0;
            _position = OriginalPosition = _endPosition = position;
            _speed = new Vector2(2);
        }

        public DrawableGameElement(Game game, Texture2D texture, Vector2 position)
            : this(game, position)
        {
            Texture = texture;
            Rectangle = new Rectangle((int)_position.X, (int)_position.Y, Texture.Width, Texture.Height);
        }
        #endregion

        #region Methods
        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (_effect != null)
            {
                _effect.Update(gameTime);
                if (_effect is PulseEffect)
                {
                    var pulseEffect = (PulseEffect)_effect;
                    _scale = pulseEffect.Magnitude;
                }
            }
            Move();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, _position, null, Color.White, 0, Vector2.Zero, _scale, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Move the element to a new position
        /// </summary>
        /// <param name="moveTo">Position to move to</param>
        public void MoveTo(Vector2 moveTo)
        {
            _isMoving = true;
            _endPosition = moveTo;
        }

        /// <summary>
        /// Makes the element pulse to draw focus
        /// </summary>
        public void Pulse(int count)
        {
            _effect = new PulseEffect(Game, count);
        }

        /// <summary>
        /// Move the element to the designated position
        /// </summary>
        protected void Move()
        {
            if (!Position.Equals(_endPosition))
            {
                Position = GetNewPosition(_endPosition);
            }
        }

        /// <summary>
        /// Decide what the new incremental position is depending on the endposition
        /// </summary>
        /// <param name="endPosition">The position to move to.</param>
        /// <returns>The new position increment</returns>
        protected Vector2 GetNewPosition(Vector2 endPosition)
        {
            Vector2 newPosition;

            //If the current position plus the speed is exceeds the 
            //new position, just place the object where it needs to be
            if (Position.X < endPosition.X)
            {
                newPosition.X = (Position.X + (int)_speed.X > endPosition.X) ? (int)endPosition.X : (int)Position.X + (int)_speed.X;
            }
            else
            {
                newPosition.X = (Position.X - (int)_speed.X < endPosition.X) ? (int)endPosition.X : (int)Position.X - (int)_speed.X;
            }

            //Do the same thing again for the Y position
            if (Position.Y < endPosition.Y)
            {
                newPosition.Y = (Position.Y + (int)_speed.Y > endPosition.Y) ? (int)endPosition.Y : (int)Position.Y + (int)_speed.Y;
            }
            else
            {
                newPosition.Y = (Position.Y - (int)_speed.Y < endPosition.Y) ? (int)endPosition.Y : (int)Position.Y - (int)_speed.Y;
            }

            return newPosition;
        }
        #endregion

    }
}
