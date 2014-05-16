using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class ThrowFrisbee : MiniGame
    {

        #region Constructors
        public ThrowFrisbee(Game game, int difficulty) 
            : base(game, difficulty)
        {
            Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 310, 530));
            targetItemPositions.Add(new Vector2(Center.X - 300, 550));

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            //itemPositions.Add(new Vector2(Center.X + 100, 500));
            //itemPositions.Add(new Vector2(Center.X + 140, 530));
            //itemPositions.Add(new Vector2(Center.X + 220, 560));
            //itemPositions.Add(new Vector2(Center.X + 280, 520));
            itemPositions.Add(new Vector2(Center.X + 105, 475));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            Vector2 resultItemPosition = new Vector2(targetItemPosition.X + 10, targetItemPosition.Y);

            TargetItem = new DrawableGameElement(game, Game1._textures["ball_position"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["frisbee"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["frisbee"], resultItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["ThrowFrisbee_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["ThrowFrisbee_Reward"]);
        }
        #endregion

    }
}
