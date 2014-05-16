using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HondenStreken
{
    class GameElement
    {
        protected Game Game { get; private set; }

        public GameElement(Game game)
        {
            Game = game;
        }

        public virtual void LoadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }


    }
}
