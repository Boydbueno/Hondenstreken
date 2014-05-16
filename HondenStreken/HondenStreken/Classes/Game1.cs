using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.IO;

namespace HondenStreken
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region Fields
        private const string TEXTURES_FOLDER = "Images";
        private const string SOUNDEFFECTS_FOLDER = "Sounds";

        public static MouseState _previousMouseState;
        public static MouseState _currentMouseState;

        public static KeyboardState _previousKeyboardState;
        public static KeyboardState _currentKeyboardState;

        public static Dictionary<string, Texture2D> _textures;
        public static Dictionary<string, SoundEffect> _soundEffects;

        public static SpriteFont _arial;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TitleScreen _titleScreen;
        private InGameScreen _inGameScreen;
        private DifficultyScreen _difficultyScreen;

        private Screen _currentScreen;

        private Cursor _cursor;
        #endregion

        #region Contructors
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            _graphics.IsFullScreen = true;
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
            // Loading the content
            _arial = Content.Load<SpriteFont>("Arial");
            _textures = LoadContent<Texture2D>(Content, TEXTURES_FOLDER);
            _soundEffects = LoadContent<SoundEffect>(Content, SOUNDEFFECTS_FOLDER);

            InitializeScreens();

            _currentScreen = _titleScreen;

            _cursor = new Cursor(this, Game1._textures["cursor"]);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            // We should abstract this away in it's own method
            if (_currentScreen is TitleScreen)
            {
                TitleScreen titleScreen = (TitleScreen)_currentScreen;

                if (titleScreen.ExitButton.IsClicked)
                {
                    this.Exit();
                }

                if (titleScreen.StartButton.IsClicked)
                {
                    _currentScreen = _difficultyScreen;
                    _difficultyScreen.DifficultyTutorial.Play(gameTime);
                }
            }

            // We should abstract this away in it's own method
            if (_currentScreen is DifficultyScreen)
            {
                DifficultyScreen difficultyScreen = (DifficultyScreen)_currentScreen;

                if (difficultyScreen.EasyModeButton.IsClicked && difficultyScreen.IsActive)
                {
                    _inGameScreen = new InGameScreen(this, 1);
                    _difficultyScreen.DifficultyTutorial.Stop();
                    _currentScreen = _inGameScreen;
                }

                if (difficultyScreen.NormalModeButton.IsClicked && difficultyScreen.IsActive)
                {
                    _inGameScreen = new InGameScreen(this, 2);
                    _difficultyScreen.DifficultyTutorial.Stop();
                    _currentScreen = _inGameScreen;
                }

                if (difficultyScreen.HardModeButton.IsClicked && difficultyScreen.IsActive)
                {
                    _inGameScreen = new InGameScreen(this, 3);
                    _difficultyScreen.DifficultyTutorial.Stop();
                    _currentScreen = _inGameScreen;
                }
            }

            _cursor.Update(gameTime);

            _currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentScreen.Draw(gameTime, _spriteBatch);
            _cursor.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Load all the content from one folder and add it to a dictionary
        /// </summary>
        /// <returns>A dictionary with the content</returns>
        private Dictionary<String, T> LoadContent<T>(ContentManager contentManager, string contentFolder)
        {
            //Load directory info, abort if none
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "\\" + contentFolder);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            //Init the resulting list
            Dictionary<String, T> result = new Dictionary<String, T>();

            //Load all files that matches the file filter
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                result[key] = contentManager.Load<T>(contentFolder + "/" + key);
            }

            return result;
        }

        /// <summary>
        /// Initialize the different screens
        /// </summary>
        private void InitializeScreens()
        {
            _titleScreen = new TitleScreen(this, _arial);
            _difficultyScreen = new DifficultyScreen(this, _arial);
        }
        #endregion

    }
}
