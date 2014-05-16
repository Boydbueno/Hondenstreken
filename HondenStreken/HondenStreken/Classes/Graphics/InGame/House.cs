using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HondenStreken
{
    class House : Screen
    {

        #region Fields
        public enum Room
        {
            kitchen,
            livingRoom
        }
        #endregion

        #region Properties
        public Room CurrentRoom { get; set; }

        public bool IsDoneSliding
        {
            get
            {
                return GameElements.First().HasReachedDestination;
            }
        }
        #endregion

        #region Constructors
        public House(Game game)
            : base(game)
        {
            DrawableGameElement _backgroundRoom = new DrawableGameElement(game, Game1._textures["background"], new Vector2(-Game1._textures["background"].Width / 2, 0));
            GameElements.Add(_backgroundRoom);
            DrawableGameElement _light = new DrawableGameElement(game, Game1._textures["light"], new Vector2(Center.X - 215, 500));
            GameElements.Add(_light);
            CurrentRoom = Room.livingRoom;
        }
        #endregion

        #region Methods   
        public void SlideToLivingRoom()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.MoveTo(new Vector2(element.Position.X - Game.GraphicsDevice.Viewport.Width, element.Position.Y));
            }
        }

        public void SlideToKitchen()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.MoveTo(new Vector2(element.Position.X + Game.GraphicsDevice.Viewport.Width, element.Position.Y));
            }
        }

        public void SlideToOtherRoom()
        {
            switch (CurrentRoom)
            {
                case Room.kitchen:
                    SlideToLivingRoom();
                    break;

                case Room.livingRoom:
                    SlideToKitchen();
                    break;
            }
        }

        public void SwapRoom()
        {
            switch (CurrentRoom)
            {
                case Room.kitchen:
                    CurrentRoom = Room.livingRoom;
                    break;

                case Room.livingRoom:
                    CurrentRoom = Room.kitchen;
                    break;
            }
        }
        #endregion

    }
}
