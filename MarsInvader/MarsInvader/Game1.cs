using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Threading.Tasks;

namespace SAE101
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
        private MyScreen1 _myScreen1;
        private MyScreen2 _myScreen2;

        private Texture2D _mainBackground, _begin, _options, _leaderboard, _leave;

        KeyboardState keyboardState;

        public const int _WINDOWSIZE = 800;
        public const int _WINDOWWIDTH = 1000;

        /// Dimensions des choix du menu
        public const int _BUTTONWIDTH = 128;
        public const int _BUTTONHEIGHT = 64;

        public Rectangle _beginButton = new Rectangle(450, 200, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _leaderboardButton = new Rectangle(450, 300, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _optionsRectButton = new Rectangle(450, 400, _BUTTONWIDTH, _BUTTONHEIGHT);
        public Rectangle _leaveButton = new Rectangle(450, 500, _BUTTONWIDTH, _BUTTONHEIGHT);

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

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _myScreen1 = new MyScreen1(this); 
            _myScreen2 = new MyScreen2(this);
            _mainBackground = Content.Load<Texture2D>("MainMenu");
            _options = Content.Load<Texture2D>("optionsGeneralMenu");
            _begin = Content.Load<Texture2D>("beginGeneralMenu");
            _leaderboard = Content.Load<Texture2D>("leaderboardGeneralMenu");
            _leave = Content.Load<Texture2D>("leaveGeneralMenu");
        }

        public void LoadScreen1()
        // Cette méthode gère l'affichage de la scène 1 du jeu, c'est-à-dire la map avec le joueur
        {
            _gameState = "Game";
            _screenManager.LoadScreen(_myScreen1, new FadeTransition(GraphicsDevice, Color.Black)); 
        }

        public void LoadScreen2()
        // Cette méthode gère l'affichage de la scène 2 du jeu, c'est-à-dire le menu en jeu
        {
            _gameState = "Menu";
            _screenManager.LoadScreen(_myScreen2, new FadeTransition(GraphicsDevice, Color.Black));
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            {
                keyboardState = Keyboard.GetState();

                // Afficher le jeu
                if ((keyboardState.IsKeyDown(Keys.Space) || _myScreen2.SourisSurRect(_beginButton)) && _gameState == "GeneralMenu")
                { 
                    this.IsMouseVisible = false;
                    LoadScreen1();
                }

                else if ((_myScreen2.SourisSurRect(_leaveButton)) && _gameState == "GeneralMenu")
                {
                    Exit();
                }

                // Afficher le menu de pause
                else if (keyboardState.IsKeyDown(Keys.Escape) && _gameState == "Game")
                {
                    this.IsMouseVisible = true;
                    LoadScreen2();
                }

            }
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_mainBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_begin, new Vector2(450, 200), Color.White);
            _spriteBatch.Draw(_leaderboard, new Vector2(450, 300), Color.White);
            _spriteBatch.Draw(_options, new Vector2(450, 400), Color.White);
            _spriteBatch.Draw(_leave, new Vector2(450, 500), Color.White);
            GraphicsDevice.Clear(Color.OrangeRed);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}