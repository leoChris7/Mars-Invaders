using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Threading.Tasks;

namespace MarsInvader
{
    public class Game1 : Game
    {
        public string _gameState;
        public string _previousGameState;
        // Enregistre l'état de la scène 
            /// GeneralMenu : le menu de démarrage, le menu général
            /// Game : le jeu
            /// GameOver : le menu de Gameover
            /// Menu : le menu en jeu
            /// Ending : le menu de fin

        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch {get; set;}
       

        private ScreenManager _screenManager;
        public ScreenGame _screenGame;

        private ScreenStarting _screenStarting;
        private ScreenGameOver _screenGameOver;

        public Rectangle _beginButton = new Rectangle(450, 200, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _leaderboardButton = new Rectangle(450, 300, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _optionsRectButton = new Rectangle(450, 400, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _leaveButton = new Rectangle(450, 500, _BUTTONWIDTH, _BUTTONHEIGHT);


        KeyboardState keyboardState;

        public const int _WINDOWSIZE = 800;
        public const int _WINDOWWIDTH = 1000;

        /// Dimensions des choix du menu
        public const int _BUTTONWIDTH = 128;
        public const int _BUTTONHEIGHT = 64;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _previousGameState = _gameState;
            _gameState = "GeneralMenu";

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = _WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = _WINDOWSIZE;
            _graphics.ApplyChanges();

            // On initialise le jeu de base
            base.Initialize();

            // Après qu'on a initialisé le jeu, on démarre directement le menu de départ
            LoadStartingScreen();
        }

        protected override void LoadContent()
        {
            _screenGame = new ScreenGame(this); 
            _screenStarting = new ScreenStarting(this);
            _screenGameOver = new ScreenGameOver(this);



            base.LoadContent();
        }
        public void LoadStartingScreen()
        {
            _previousGameState = _gameState;
            _gameState = "GeneralMenu";
            _screenManager.LoadScreen(_screenStarting, new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadGameScreen()
        // Cette méthode gère l'affichage de la scène 1 du jeu, c'est-à-dire la map avec le joueur
        {
            this.IsMouseVisible = false;
            _previousGameState = _gameState;
            _gameState = "Game";
            _screenManager.LoadScreen(_screenGame, new FadeTransition(GraphicsDevice, Color.Black)); 
        }

        public void LoadGameMenuScreen()
        // Cette méthode gère l'affichage de la scène 2 du jeu, c'est-à-dire le menu en jeu
        {
            this.IsMouseVisible = true;
            _previousGameState = _gameState;

            _gameState = "Menu";
        }

        public void LoadGameOverScreen()
        {
            this.IsMouseVisible = true;
            _previousGameState = _gameState;
            _gameState = "GameOver";
            _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black));
        }
        public static bool SourisSurRect(Rectangle rect)
        {
            MouseState _mouseState = Mouse.GetState();
            return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            // Quitter le jeu
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                (SourisSurRect(_leaveButton) && _gameState == "GeneralMenu"))
                Exit();

            // Afficher le jeu
            if ((keyboardState.IsKeyDown(Keys.Space) || SourisSurRect(_beginButton)) && _gameState == "GeneralMenu")
            {
                LoadGameScreen();
            }

            // Afficher le menu de pause
            else if (keyboardState.IsKeyDown(Keys.Escape) && _gameState == "Game")
            {
                LoadGameMenuScreen();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // On ne dessine rien dans Game1.
            base.Draw(gameTime);
        }
    }
}