﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class ThrowBallForDog : MiniGame
    {

        #region Constructors
        public ThrowBallForDog(Game game, int difficulty) 
            : base(game, difficulty)
        {
            Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 310, 530));
            targetItemPositions.Add(new Vector2(Center.X - 300, 550));

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            itemPositions.Add(new Vector2(Center.X + 100, 500));
            itemPositions.Add(new Vector2(Center.X + 260, 345));
            itemPositions.Add( new Vector2(Center.X + 140, 530));
            itemPositions.Add(new Vector2(Center.X + 140, 450));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            Vector2 resultItemPosition = new Vector2(targetItemPosition.X + 5, targetItemPosition.Y);

            TargetItem = new DrawableGameElement(game, Game1._textures["ball_position"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["ball"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["ball"], resultItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["ThrowBall_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["ThrowBall_Reward"]);
        }
        #endregion

    }
}
