using System;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

public class screenMenu : GameScreen
	{
		private Texture2D _menuBackground;
		private SpriteFont _police;
		private Game1 _myGame;
		private readonly ScreenManager _screenManager;

		public SpriteBatch SpriteBatch { get; set; }
		private ScreenGame _gameData;	

		MouseState _mouseState;

		Rectangle Continue;
		Rectangle Leave;

		// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
		// défini dans Game1
		
			public screenMenu(Game1 game) : base(game)
			{
				_myGame = game;
				_screenManager = new ScreenManager();
				
			}

		public override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			_police = Content.Load<SpriteFont>("fontPauseMenu");
			_menuBackground = Content.Load<Texture2D>("MenuBackground");
			
		base.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{ 
		
		}

		public bool SourisSurRect(Rectangle rect)
		{
			_mouseState = Mouse.GetState();
			return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		}

		public override void Draw(GameTime gameTime)
		{
			_myGame.GraphicsDevice.Clear(Color.Black);

			SpriteBatch.Begin();

			SpriteBatch.Draw(_menuBackground, new Vector2(0, 0), Color.White);

			SpriteBatch.DrawString(_police, "MENU", new Vector2((float)Game1._WINDOWWIDTH / 2 - 60, 0), Color.White);
			SpriteBatch.DrawString(_police, "Reprendre", new Vector2((float)Game1._WINDOWWIDTH / 2 - 60, 50), Color.White);
			SpriteBatch.DrawString(_police, "Quitter", new Vector2((float)Game1._WINDOWWIDTH / 2 - 60, 100), Color.White);

			Continue = new Rectangle((int)(float)Game1._WINDOWSIZE / 2 - 60, 50, 100, 50);
			Leave = new Rectangle((int)(float)Game1._WINDOWSIZE / 2 - 60, 100, 100, 50);

			_mouseState = Mouse.GetState();
			bool mouseClickOnContinue = Continue.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
			bool mouseClickOnLeave = Leave.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;


			if ( mouseClickOnContinue &&
			_myGame._gameState == "Menu")
			{
				_myGame._gameState = "Game";
				_myGame.IsMouseVisible = false;
				
				_myGame.LoadGameScreen();
			}
			else if (mouseClickOnLeave)
			{
				_myGame.Exit();
			}
			SpriteBatch.End();
		}
	}

