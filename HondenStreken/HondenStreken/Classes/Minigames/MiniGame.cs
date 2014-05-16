using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Timers;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace HondenStreken
{
    class MiniGame : Screen
    {

        #region Fields
        private int _difficulty;

        private MouseGameElement _item;
        private DrawableGameElement _targetItem;
        private DrawableGameElement _resultItem;
        private MinigameState _state;

        private Double _timeSinceLastTutorial;

        public AudioEffect TutorialSound { get; set; }
        public AudioEffect RewardSound { get; set; }

        public enum MinigameState
        {
            ready,
            tutorial, 
            game, 
            rewardStart,
            rewardFinished,
            finished, 
            ended
        }
        #endregion

        #region Properties
        public MinigameState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                LastStateChange = _totalSeconds;
            }
        }
        
        public double LastStateChange { get; set; }
        /// <summary>
        /// Assign the targetitem and add it to the gameElements list.
        /// </summary>
        public DrawableGameElement TargetItem
        {
            get
            {
                return _targetItem;
            }
            set
            {
                _targetItem = value;
                GameElements.Add(value);
            }
        }

        /// <summary>
        /// Assign the item and add it to the gameElements list.
        /// </summary>
        public MouseGameElement Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                GameElements.Add(value);
            }
        }

        /// <summary>
        /// Assign the resultitem and add it to the gameElements list.
        /// </summary>
        public DrawableGameElement ResultItem
        {
            get
            {
                return _resultItem;
            }
            set
            {
                _resultItem = value;
                _resultItem.IsVisible = false;
                GameElements.Add(value);
            }
        }
        #endregion

        #region Constructors
        public MiniGame(Game game, int difficulty)
            : base(game)
        {
            _difficulty = difficulty;
            State = MinigameState.ready;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (State == MinigameState.tutorial)
            {
                TutorialSound.PlayOnce(gameTime);

                _timeSinceLastTutorial = TutorialSound.TimeFinished;

                // When the tutorial sound is finished playing, start the minigame
                if (HasTotalSecondsPassed(TutorialSound.TimeFinished))
                {
                    _item.Pulse(2);
                    _targetItem.Pulse(2);
                    State = MinigameState.game;
                }
            }

            if (State == MinigameState.game)
            {
                AllowInteraction();

                if (HasTotalSecondsPassed(TutorialSound.TimeFinished + 10))
                {
                    TutorialSound.Restart(gameTime);
                    _item.Pulse(2);
                    _targetItem.Pulse(2);
                }

                if (_item.HasHitObject(_targetItem))
                {
                    RewardSound.Play(gameTime);
                    ResolveItems();
                    State = MinigameState.rewardStart;
                }
            }

            if (State == MinigameState.rewardStart)
            {
                if (HasTotalSecondsPassed(RewardSound.TimeFinished))
                {
                    State = MinigameState.rewardFinished;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Replace the on screen items with the result item
        /// </summary>
        private void ResolveItems()
        {
            GameElements.Remove(_item);
            GameElements.Remove(_targetItem);
            _resultItem.IsVisible = true;
        }

        /// <summary>
        /// Allow interaction depending on the difficulty
        /// </summary>
        private void AllowInteraction()
        {
            if (_difficulty == 1)
            {
                if (IsClicked)
                {
                    Item.MoveTo(TargetItem.Center);
                }
            }
            else if (_difficulty == 2)
            {
                if (_item.IsClicked)
                {
                    Item.MoveTo(TargetItem.Center);
                }
            }
            else if (_difficulty == 3)
            {
                _item.IsDraggable = true;
            }
        }

        /// <summary>
        /// Starts the minigame by setting the state to the tutorial state
        /// </summary>
        public void Start()
        {
            State = MinigameState.tutorial;
        }

        // Waayy too much repetition below, should be refactored
        public void PlaceItemsLeft()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.Position = new Vector2(element.Position.X - Game.GraphicsDevice.Viewport.Width, element.Position.Y);
            }
        }

        public void PlaceItemsRight()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.Position = new Vector2(element.Position.X + Game.GraphicsDevice.Viewport.Width, element.Position.Y);
            }
        }

        public void SlideItemsLeft()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.MoveTo(new Vector2(element.Position.X - Game.GraphicsDevice.Viewport.Width, element.Position.Y));
            }
        }

        public void SlideItemsRight()
        {
            foreach (DrawableGameElement element in GameElements)
            {
                element.MoveTo(new Vector2(element.Position.X + Game.GraphicsDevice.Viewport.Width, element.Position.Y));
            }
        }
        #endregion
    }
}
