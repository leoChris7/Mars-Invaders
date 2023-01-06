using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

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
        KeyboardState keyboardState;

        public const int _WINDOWSIZE = 800;
        public const int _WINDOWWIDTH = 1000;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameState = "GeneralMenu";
            // TODO: Add your initialization logic here
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
            _myScreen1 = new MyScreen1(this); // en leur donnant une référence au Game
            _myScreen2 = new MyScreen2(this);
            
            // TODO: use this.Content to load your game content here
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
                if (keyboardState.IsKeyDown(Keys.Space) && _gameState == "GeneralMenu")
                { 
                    this.IsMouseVisible = false;
                    LoadScreen1();
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
            GraphicsDevice.Clear(Color.OrangeRed);
            
            base.Draw(gameTime);
        }
    }
}