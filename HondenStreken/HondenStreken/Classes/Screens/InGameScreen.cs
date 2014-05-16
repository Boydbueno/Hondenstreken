using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace HondenStreken
{
    class InGameScreen : Screen
    {
        #region Fields
        private Dog _dog;
        private House _house;

        private PauseScreen _pauseScreen;

        private enum State
        {
            intro,
            minigame
        }

        private State _state;

        private List<MiniGame> _kitchenGames = new List<MiniGame>();
        private List<MiniGame> _livingRoomGames = new List<MiniGame>();

        private MiniGame _currentMiniGame; // This could also be a list with active minigames, two max.
        private MiniGame _nextMinigame;

        private AudioEffect _introSound;

        private int _difficulty;

        private Hud _hud;
        private Random _random = new Random();
        #endregion
        
        #region Constructors
        public InGameScreen(Game game, int difficulty)
            : base(game)
        {
            _difficulty = difficulty;

            _state = State.intro;

            Initialize();
        }
        #endregion

        #region Methods
        public void Initialize() // This should be a GameElement override and called automatically after the constructor
        {
            //Create elements
            _house = new House(Game);
            _hud = new Hud(Game);
            _dog = new Dog(Game, Game1._textures["spritesheet"], new Vector2(-100, 320), 163, 213);

            //Add elements
            _backgrounds.Add(_house);
            _overlays.Add(_hud);
            GameElements.Add(_dog);

            _introSound = new AudioEffect(Game1._soundEffects["Intro"]);
            _pauseScreen = new PauseScreen(Game);

            InitializeKitchenMinigames();
            InitializeLivingRoomMinigames();

        }

        public override void Update(GameTime gameTime)
        {
            if (IsKeyEvent(Keys.Escape))
            {
                _overlays.Add(_pauseScreen);
            }

            // Abstract in own method
            if (IsOverlayActive(_pauseScreen))
            {
                if (_pauseScreen.ExitButton.IsClicked)
                {
                    Game.Exit();
                }

                if (_pauseScreen.ResumeButton.IsClicked)
                {
                    _overlays.Remove(_pauseScreen);
                }
            }

            if (_state == State.intro)
            {
                _introSound.PlayOnce(gameTime);

                if (HasTotalSecondsPassed(_introSound.TimeFinished + 3))
                {
                    _currentMiniGame = _livingRoomGames.First();
                    _livingRoomGames.Remove(_currentMiniGame);
                    _currentMiniGame.Start();
                    _state = State.minigame;
                }
            }

            if (_state == State.minigame)
            {
                Minigame(gameTime);

                if (_house.IsDoneSliding)
                {

                    _house.SwapRoom();

                    MakeNextMinigameCurrentMinigame(); // By lack of a better name
                    _currentMiniGame.Start();
                    _dog.MoveHorizontal((int)Center.X - _dog.SpriteWidth / 2);

                }

                _currentMiniGame.Update(gameTime);
                if (_nextMinigame != null)
                    _nextMinigame.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            // Draw stuff here

            if(_currentMiniGame != null)
                _currentMiniGame.Draw(gameTime, spriteBatch);

            if (_nextMinigame != null)
                 _nextMinigame.Draw(gameTime, spriteBatch);

        }

        /// <summary>
        /// Do all minigame related stuff, depending on it's state.
        /// </summary>
        private void Minigame(GameTime gameTime)
        {
            if (_currentMiniGame.State == MiniGame.MinigameState.rewardStart)
            {
                _dog.MoveHorizontal((int)_currentMiniGame.ResultItem.Position.X + 50);
            }
            else if (_currentMiniGame.State == MiniGame.MinigameState.rewardFinished)
            {
                _hud.AddReward();
                _hud.LastReward.Pulse(3);
                _dog.Bark(gameTime);
                _currentMiniGame.State = MiniGame.MinigameState.finished;
            }
            else if (_currentMiniGame.State == MiniGame.MinigameState.finished)
            {
                if (HasTotalSecondsPassed(_currentMiniGame.LastStateChange + 1))
                {
                    PrepareNextMinigame();
                    SlideToOtherRoom();
                    _currentMiniGame.State = MiniGame.MinigameState.ended;  // ended is a state to make sure the finished stuff is no longer executed
                }
            }
        }

        private void PrepareNextMinigame()
        {
            if (_house.CurrentRoom == House.Room.livingRoom)
            {
                if (_kitchenGames.Count == 0)
                {
                    InitializeKitchenMinigames();
                }
                _nextMinigame = GetRandomMinigame(_kitchenGames);
                _nextMinigame.PlaceItemsLeft();
            }
            else if (_house.CurrentRoom == House.Room.kitchen)
            {
                if (_livingRoomGames.Count == 0)
                {
                    InitializeLivingRoomMinigames();
                }
                _nextMinigame = GetRandomMinigame(_livingRoomGames);
                _nextMinigame.PlaceItemsRight();
            }
        }

        private void MakeNextMinigameCurrentMinigame()
        {
            // We have to shuffle places because we need to free up nextMinigame
            _currentMiniGame = _nextMinigame;
            _nextMinigame = null;
        }

        /// <summary>
        /// Initialize every minigame that will/can be used in the game
        /// </summary>
        private void InitializeKitchenMinigames()
        {
            _kitchenGames.Add(new FeedTheDog(Game, _difficulty));
            //_kitchenGames.Add(new WashTheDog(Game, _difficulty));
            //_kitchenGames.Add(new RepairFlower(Game, _difficulty));
        }

        /// <summary>
        /// Initialize every minigame that will/can be used in the game
        /// </summary>
        private void InitializeLivingRoomMinigames()
        {
            _livingRoomGames.Add(new GiveBoneToDog(Game, _difficulty));
            _livingRoomGames.Add(new ThrowBallForDog(Game, _difficulty));
            _livingRoomGames.Add(new ThrowFrisbee(Game, _difficulty));

        }

        private void SlideToKitchen()
        {
            // make the room and the minigame components slide.
            _house.SlideToKitchen();
            _currentMiniGame.SlideItemsRight();
            _livingRoomGames.Remove(_currentMiniGame);
            _dog.CurrentState = Dog.DogState.WalkingLeft;
        }

        private void SlideToLivingRoom()
        {
            // make the room and the minigame components slide.
            _house.SlideToLivingRoom();
            _currentMiniGame.SlideItemsLeft();
            _kitchenGames.Remove(_currentMiniGame);
            _dog.CurrentState = Dog.DogState.WalkingRight;
        }

        private void SlideToOtherRoom()
        {
            switch (_house.CurrentRoom)
            {
                case House.Room.livingRoom:
                    SlideToKitchen();
                    break;

                case House.Room.kitchen:
                    SlideToLivingRoom();
                    break;
            }
        }

        private MiniGame GetRandomMinigame(List<MiniGame> minigameList)
        {
            int randomMinigame = _random.Next(0, minigameList.Count());
            return minigameList.ElementAt(randomMinigame);
        }
        #endregion
    }
}
