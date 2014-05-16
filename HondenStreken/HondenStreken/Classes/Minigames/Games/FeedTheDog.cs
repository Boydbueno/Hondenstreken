using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class FeedTheDog : MiniGame
    {

        #region Constructors
        /// <summary>
        /// Initialize a new minigame called Feed The Dog
        /// </summary>
        public FeedTheDog(Game game, int difficulty) 
            : base(game, difficulty)
        {
            Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 180, 540));
            targetItemPositions.Add(new Vector2(Center.X - 240, 550)); 
            targetItemPositions.Add(new Vector2(Center.X - 320, 500));

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            //itemPositions.Add(new Vector2(Center.X + 70, 525));
            itemPositions.Add(new Vector2(Center.X + 100, 350)); // aanrechtblad
            //itemPositions.Add(new Vector2(Center.X + 80, 535));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            TargetItem = new DrawableGameElement(game, Game1._textures["food_empty"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["food"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["food_full"], targetItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["GiveFood_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["GiveFood_Reward"]);
        }
        #endregion

    }
}
