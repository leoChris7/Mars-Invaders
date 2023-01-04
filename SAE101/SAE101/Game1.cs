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
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch {get; set;}
        private ScreenManager _screenManager;
        private MyScreen1 _myScreen1;
        private MyScreen2 _myScreen2;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _myScreen1 = new MyScreen1(this); // en leur donnant une référence au Game
            _myScreen2 = new MyScreen2(this);
            _tiledMap = Content.Load<TiledMap>("map_V1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            {
                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                _screenManager.LoadScreen(_myScreen1, new FadeTransition(GraphicsDevice,
                Color.Black));
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                _screenManager.LoadScreen(_myScreen2, new FadeTransition(GraphicsDevice,
                Color.Black));
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _tiledMapRenderer.Draw();
            base.Draw(gameTime);
        }
    }
}