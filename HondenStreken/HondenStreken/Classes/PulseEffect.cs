using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HondenStreken
{
    class PulseEffect : VisualEffect
    {

        public PulseEffect(Game game, int loops)
            : base(game, 1, 1, 1.2f, 0.02f, loops)
        {
        }

    }
}
