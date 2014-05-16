using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class WashTheDog : MiniGame
    {

        #region Constructors
        public WashTheDog(Game game, int difficulty) 
            : base(game, difficulty)
        {
            Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 260, 420));
            targetItemPositions.Add(new Vector2(Center.X - 290, 420));
            targetItemPositions.Add(new Vector2(Center.X - 320, 420));

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            itemPositions.Add(new Vector2(Center.X + 70, 460));
            itemPositions.Add(new Vector2(Center.X + 100, 480));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            TargetItem = new DrawableGameElement(game, Game1._textures["water_empty"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["shampoo"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["water_full"], targetItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["WashDog_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["WashDog_Reward"]);
        }
        #endregion

    }
}
