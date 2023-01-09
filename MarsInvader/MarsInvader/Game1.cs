using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Threading.Tasks;

namespace MarsInvader
{
    public class Game1 : Game
    {
        public string _gameState;
        // Enregistre l'état de la scène 
            /// GeneralMenu : le menu de démarrage, le menu général
            /// Game : le jeu
            /// Menu : le menu en jeu
            /// Ending : le menu de fin

        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch {get; set;}
        private ScreenManager _screenManager;
        private ScreenGame _screenGame;
        private screenMenu _screenMenu;
        private ScreenStarting _screenStarting;
        private SaveGameMenu _saveGameMenu;

        private Texture2D _mainBackground, _begin, _options, _leaderboard, _leave;

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
            _screenMenu = new screenMenu(this);
            _screenStarting = new ScreenStarting(this);
        }
        public void LoadStartingScreen()
        {
            _gameState = "GeneralMenu";
            _screenManager.LoadScreen(_screenStarting, new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadGameScreen()
        // Cette méthode gère l'affichage de la scène 1 du jeu, c'est-à-dire la map avec le joueur
        {
            _gameState = "Game";
            _screenManager.LoadScreen(_screenGame, new FadeTransition(GraphicsDevice, Color.Black)); 
        }

        public void LoadGameMenuScreen()
        // Cette méthode gère l'affichage de la scène 2 du jeu, c'est-à-dire le menu en jeu
        {
            _gameState = "Menu";

            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            {
                keyboardState = Keyboard.GetState();

                // Afficher le jeu
                if ((keyboardState.IsKeyDown(Keys.Space) || _screenMenu.SourisSurRect(_beginButton)) && _gameState == "GeneralMenu")
                { 
                    this.IsMouseVisible = false;
                    LoadGameScreen();
                }

                else if ((_screenMenu.SourisSurRect(_leaveButton)) && _gameState == "GeneralMenu")
                {
                    Exit();
                }

                // Afficher le menu de pause
                else if (keyboardState.IsKeyDown(Keys.Escape) && _gameState == "Game")
                {
                    this.IsMouseVisible = true;
                    LoadGameMenuScreen();
                }

            }
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}