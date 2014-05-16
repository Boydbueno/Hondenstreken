using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HondenStreken
{
    class VisualEffect : GameElement
    {
        protected bool _goingUp;
        protected float _min;
        protected float _max;
        protected float _increments;
        protected int _loops;


        /// <summary>
        /// Can't think of a better name atm
        /// </summary>
        public float Magnitude { get; set; }

        /// <summary>
        /// The amount of loops done
        /// </summary>
        public int Count { get; protected set; }

        public VisualEffect(Game game, float start, float min, float max, float increments, int loops, bool goingUp = true)
            : base(game)
        {
            Magnitude = start;
            _min = min;
            _max = max;
            _increments = increments;
            _loops = loops;
            _goingUp = goingUp;
        }

        public override void Update(GameTime gameTime)
        {
            if (Count <= _loops)
            {
                UpdateDirection();

                if (_goingUp)
                {
                    Magnitude += _increments;
                }
                else
                {
                    Magnitude -= _increments;
                }
            }

            base.Update(gameTime);
        }

        private void UpdateDirection()
        {
            if (Magnitude >= _max)
            {

                _goingUp = false;
            }
            else if (Magnitude <= _min)
            {
                Count++;
                _goingUp = true;
            }
        }

    }
}
