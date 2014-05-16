using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace HondenStreken
{
    class Dog : AnimatedGameElement
    {

        #region Fields
        private AudioEffect _bark;

        public enum DogState
        {
            WalkingLeft,
            WalkingRight,
            Sitting,
            Barking
        }
        #endregion

        #region Properties
        public DogState CurrentState { get; set; }
        #endregion

        #region Constructors
        public Dog(Game game, Texture2D texture, Vector2 position, int spriteWidth, int spriteHeigth)
            : base(game, texture, position, spriteWidth, spriteHeigth)
        {
            MoveHorizontal(game.GraphicsDevice.Viewport.Width / 2 - SpriteWidth / 2);
            CurrentState = DogState.WalkingRight;
            _bark = new AudioEffect(Game1._soundEffects["dogBark"]);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {

            if (gameTime.TotalGameTime.TotalSeconds >= _bark.TimeFinished && CurrentState.Equals(DogState.Barking))
            {
                CurrentState = DogState.Sitting;
            }

            ChangeTexture();
            if (HasReachedDestination)
            {
                Sit();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Method to let the dog bark
        /// </summary>
        public void Bark(GameTime gameTime)
        {
            _currentFrame = 0;
            CurrentState = DogState.Barking;
            _bark.Play(gameTime);
        }

        public void MoveHorizontal(int xPos)
        {
            MoveTo(new Vector2(xPos, Position.Y));
            if (xPos < Position.X)
            {
                CurrentState = DogState.WalkingLeft;
            }
            else if(xPos > Position.X)
            {
                CurrentState = DogState.WalkingRight;
            }
        }

        public void Sit()
        {
            CurrentState = DogState.Sitting;
        }

        /// <summary>
        /// Change texture depending on state
        /// </summary>
        private void ChangeTexture()
        {
            switch (CurrentState)
            {
                case (DogState.Sitting):

                    _spriteRow = 0;
                    break;

                case (DogState.WalkingLeft):

                    _spriteRow = 1;
                    break;

                case (DogState.WalkingRight):

                    _spriteRow = 2;
                    break;

                case (DogState.Barking):
                    
                    _spriteRow = 3;
                    break;
            }
        }
        #endregion
    }
}
