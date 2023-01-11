using System;
using System.Collections.Generic;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using SAE101;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Audio;

public class ScreenGameOver : GameScreen
{
	private Game1 _myGame;
	private readonly ScreenManager _screenManager;

	MouseState _mouseState;

	private Texture2D _gameOverMainMenuTexture, _gameOverRestartTexture, _gameOverBackground;
	private Rectangle _gameOverMainMenuButton, _gameOverRestartButton;
	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1

	public ScreenGameOver(Game1 game) : base(game)
	{
		_myGame = game;
		_screenManager = new ScreenManager();
		this._gameOverMainMenuButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 300, 128, 32);
		this._gameOverRestartButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 200, 128, 32);
	}

	public override void LoadContent()
	{
		_gameOverBackground = Content.Load<Texture2D>("gameover");
		_gameOverMainMenuTexture = Content.Load<Texture2D>("gameoverMenuPrincipal");
		_gameOverRestartTexture = Content.Load<Texture2D>("gameoverRecommencer");
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{


		_mouseState = Mouse.GetState();
		bool mouseClickOnRestart = _gameOverRestartButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnMainMenu = _gameOverMainMenuButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;

		if (mouseClickOnRestart &&
			_myGame._gameState == "GameOver")
		{
			_myGame._gameState = "Game";
			_myGame.LoadGameScreen();
		}

		else if (mouseClickOnMainMenu)
		{
			_myGame._gameState = "GeneralMenu";
			_myGame.LoadStartingScreen();

		}
	}

	public bool SourisSurRect(Rectangle rect)
	{
		_mouseState = Mouse.GetState();
		return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
	}

	public override void Draw(GameTime gameTime)
	{
		_myGame.GraphicsDevice.Clear(Color.Black);

		_myGame.SpriteBatch.Begin();

		_myGame.SpriteBatch.Draw(_gameOverBackground, new Vector2(0, 0), Color.White);

		_myGame.SpriteBatch.Draw(this._gameOverRestartTexture, new Vector2(this._gameOverRestartButton.X, this._gameOverRestartButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._gameOverMainMenuTexture, new Vector2(this._gameOverMainMenuButton.X, this._gameOverMainMenuButton.Y), Color.White);

		_myGame.SpriteBatch.End();
	}
}

