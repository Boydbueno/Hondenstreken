using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace HondenStreken
{
    class GiveBoneToDog : MiniGame
    {

        #region Constructors
        public GiveBoneToDog(Game game, int difficulty) 
            : base(game, difficulty)
        {
            Random random = new Random();

            var targetItemPositions = new List<Vector2>();
            targetItemPositions.Add(new Vector2(Center.X - 180, 510));
            targetItemPositions.Add(new Vector2(Center.X - 280, 550));
            //targetItemPositions.Add(new Vector2(Center.X - 330, 480));

            Vector2 targetItemPosition = targetItemPositions[random.Next(0, targetItemPositions.Count)];

            var itemPositions = new List<Vector2>();
            itemPositions.Add(new Vector2(Center.X + 70, 520));
            itemPositions.Add(new Vector2(Center.X + 220, 370));
            itemPositions.Add(new Vector2(Center.X + 240, 490));
            itemPositions.Add(new Vector2(Center.X + 90, 470));

            Vector2 itemPosition = itemPositions[random.Next(0, itemPositions.Count)];

            Vector2 resultItemPosition = new Vector2(targetItemPosition.X, targetItemPosition.Y - 3); // This one needs to be 3 px lower

            TargetItem = new DrawableGameElement(game, Game1._textures["food_empty"], targetItemPosition);
            Item = new MouseGameElement(game, Game1._textures["bone"], itemPosition);
            ResultItem = new DrawableGameElement(game, Game1._textures["bone_full"], resultItemPosition);
            TutorialSound = new AudioEffect(Game1._soundEffects["GiveBone_Intro"]);
            RewardSound = new AudioEffect(Game1._soundEffects["GiveBone_Reward"]);
        }
        #endregion

    }
}
