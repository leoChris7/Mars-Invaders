using System;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Media;

public class ScreenGameOver : GameScreen
{
	ScreenManager _screenManager;
	MouseState _mouseState;

	private Game1 _myGame;
	private Texture2D _gameOverMainMenuTexture, _gameOverRestartTexture, _gameOverBackground, _saveButtonTexture;
	private Rectangle _gameOverMainMenuButton, _gameOverRestartButton, _saveButton;

	public ScreenGameOver(Game1 game) : base(game)
	{
		_myGame = game;
		_screenManager = _myGame._screenManager;

		this._gameOverMainMenuButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 300, 128, 32);
		this._gameOverRestartButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 200, 128, 32);
		this._saveButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 700, 128, 32);
	}

	public override void LoadContent()
	{
		MediaPlayer.Stop();
		_gameOverBackground = Content.Load<Texture2D>("gameover");
		_gameOverMainMenuTexture = Content.Load<Texture2D>("gameoverMenuPrincipal");
		_gameOverRestartTexture = Content.Load<Texture2D>("gameoverRecommencer");
		_saveButtonTexture = Content.Load<Texture2D>("saveButton");
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		_mouseState = Mouse.GetState();
		bool mouseClickOnRestart = _gameOverRestartButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnMainMenu = _gameOverMainMenuButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnSave = _saveButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;

		if (mouseClickOnRestart &&
			_myGame._gameState == "GameOver")
		{
			_myGame._gameState = "Game";
			_myGame._screenGame._joueur.Health = 100;
			_myGame.LoadGameScreen();
		}

		else if (mouseClickOnMainMenu)
		{
			_myGame._gameState = "GeneralMenu";
			_myGame.LoadStartingScreen();
		}

		else if (mouseClickOnSave)
		{
			// Ajouter le joueur dans les meilleurs
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
		_myGame.SpriteBatch.Draw(this._saveButtonTexture, new Vector2(this._saveButton.X, this._saveButton.Y), Color.White);

		_myGame.SpriteBatch.DrawString(_myGame._screenGame.Police, $"Vous avez survecu {Math.Round(_myGame._screenGame.ChronoGeneral,0)} secondes!", new Vector2(Game1._WINDOWWIDTH / 2 - 200, Game1._WINDOWSIZE / 2), Color.White);
		_myGame.SpriteBatch.DrawString(_myGame._screenGame.Police, $"Vous avez tue {_myGame._screenGame.aliensTue} aliens.", new Vector2(Game1._WINDOWWIDTH / 2 - 150, Game1._WINDOWSIZE / 2 + 50), Color.White);
		_myGame.SpriteBatch.DrawString(_myGame._screenGame.Police, $"Vous avez atteint le niveau {_myGame._screenGame._joueur.Niveau}", new Vector2(Game1._WINDOWWIDTH / 2 - 150, Game1._WINDOWSIZE / 2 + 100), Color.White);

		_myGame.SpriteBatch.End();
	}
}

