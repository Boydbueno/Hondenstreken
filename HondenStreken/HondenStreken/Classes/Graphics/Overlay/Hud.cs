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
    class Hud : Screen
    {
        #region Fields
        private List<DrawableGameElement> _dogPaws = new List<DrawableGameElement>();
        private const float OVERLAP_PERCENTAGE = 0.8f; 
        #endregion

        #region Properties
        private Vector2 NextRewardPosition
        {
            get 
            {
                if (_dogPaws.Count > 0)
                {
                    return new Vector2(
                        (int)_dogPaws.Last().Position.X + (int)(Game1._textures["paw"].Width * OVERLAP_PERCENTAGE),
                        (int)_dogPaws.Last().Position.Y);
                }
                else
                {
                    return new Vector2(30, 30);
                }
            }
        }

        public DrawableGameElement LastReward
        {
            get
            {
                return _dogPaws.Last();
            }
        }
        #endregion

        #region Constructors
        public Hud(Game game)
            : base(game)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a reward to the Hud
        /// </summary>
        public void AddReward()
        {
            DrawableGameElement paw = new DrawableGameElement(Game, Game1._textures["paw"], NextRewardPosition);
            _dogPaws.Add(paw);
            GameElements.Add(paw);
        }
        #endregion

    }
}
