using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class RepairFlower : MiniGame
    {

        #region Constructors
        public RepairFlower(Game game, int difficulty) 
            : base(game, difficulty)
        {
                        Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 280, 380));
            targetItemPositions.Add(new Vector2(Center.X - 340, 390)); 

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            itemPositions.Add(new Vector2(Center.X + 70, 510));
            itemPositions.Add(new Vector2(Center.X + 100, 550));
            itemPositions.Add(new Vector2(Center.X + 10, 540));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            TargetItem = new DrawableGameElement(game, Game1._textures["vase_empty"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["flower"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["vase_full"], targetItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["RepairFlowers_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["RepairFlowers_Reward"]);
        }
        #endregion

    }
}
