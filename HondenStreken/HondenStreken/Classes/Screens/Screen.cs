using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HondenStreken
{
    class Screen : GameElement
    {

        #region Fields
        protected List<Screen> _backgrounds = new List<Screen>();
        protected List<Screen> _overlays = new List<Screen>();
        protected double _totalSeconds;
        #endregion

        #region Properties
        public List<DrawableGameElement> GameElements { get; set; }

        /// <summary>
        /// Check if there is a mousedown followed by a mouseup event. Anywhere
        /// </summary>
        public bool IsClicked
        {
            get
            {
                return (Game1._previousMouseState.LeftButton == ButtonState.Pressed && Game1._currentMouseState.LeftButton == ButtonState.Released);
            }
        }
        public bool IsActive { get; private set; }

        public Vector2 Center
        {
            get
            {
                return new Vector2(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            }
        }
        #endregion

        #region Contructors
        public Screen(Game game) 
            : base (game)
        {
            GameElements = new List<DrawableGameElement>();
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            _totalSeconds = gameTime.TotalGameTime.TotalSeconds;
            // Ugly bit simple workaround for 'the button issue'
            IsActive = true;
            foreach (Screen background in _backgrounds)
            {
                background.Update(gameTime);
            }
            foreach (DrawableGameElement gameElement in GameElements)
            {
                gameElement.Update(gameTime);
            }
            foreach (Screen overlay in _overlays)
            {
                overlay.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Screen background in _backgrounds)
            {
                background.Draw(gameTime, spriteBatch);
            }
            foreach (DrawableGameElement gameElement in GameElements)
            {
                gameElement.Draw(gameTime, spriteBatch);
            }
            foreach (Screen overlay in _overlays)
            {
                overlay.Draw(gameTime, spriteBatch);
            }
        }
        
        public bool IsKeyEvent(Keys key)
        {
            if (Game1._previousKeyboardState.IsKeyDown(key) && Game1._currentKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see if a certain overlay screen is active
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public bool IsOverlayActive(Screen screen)
        {
            return _overlays.Contains(screen);
        }

        /// <summary>
        /// Checks if the seconds given has passed on the game clock
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        protected bool HasTotalSecondsPassed(double seconds)
        {
            return (_totalSeconds >= seconds);
        }
        #endregion
    }
}
