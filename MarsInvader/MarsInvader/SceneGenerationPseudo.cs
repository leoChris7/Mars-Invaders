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

public class ScreenGenerationPseudo : GameScreen
{
	private Game1 _myGame;
	private readonly ScreenManager _screenManager;

	MouseState _mouseState;

	private Texture2D _background, _generationButtonTexture, _continueButtonTexture, _backButtonTexture, _titleTexture;
	private Rectangle _generationButton, _continueButton, _backButton;
	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1

	public ScreenGenerationPseudo(Game1 game) : base(game)
	{
		_myGame = game;
		_screenManager = new ScreenManager();
		this._generationButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 300, 128, 32);
		this._continueButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 200, 128, 32);
		this._backButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 200, 128, 32);
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("gameover");
		_generationButtonTexture = Content.Load<Texture2D>("pseudoGengenerateButton");
		_continueButtonTexture = Content.Load<Texture2D>("pseudoGenContinueButton");
		_backButtonTexture = Content.Load<Texture2D>("pseudoGenbackButton");
		_titleTexture = Content.Load<Texture2D>("pseudoGenTitle");
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{


		_mouseState = Mouse.GetState();
		bool mouseClickOnBack = _backButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnGenerate = _generationButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnContinue = _continueButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;

		if (mouseClickOnBack &&
			_myGame._gameState == "Pseudo")
		{
			_myGame._previousGameState = _myGame._gameState;
			_myGame._gameState = "GeneralMenu";
			
			_myGame.LoadStartingScreen();
		}

		else if (mouseClickOnContinue)
		{
			_myGame._previousGameState = _myGame._gameState;
			_myGame._gameState = "Game";

			_myGame.LoadGameScreen();
		}

		else if (mouseClickOnGenerate)
        {
			_myGame._previousGameState = _myGame._gameState;
			_myGame._gameState = "GeneralMenu";
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

		_myGame.SpriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
		_myGame.SpriteBatch.Draw(_titleTexture, new Vector2(Game1._WINDOWWIDTH / 2 - 50, 0), Color.White);

		_myGame.SpriteBatch.Draw(this._generationButtonTexture, new Vector2(this._generationButton.X, this._generationButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._backButtonTexture, new Vector2(this._backButton.X, this._backButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._continueButtonTexture, new Vector2(this._continueButton.X, this._continueButton.Y), Color.White);

		_myGame.SpriteBatch.DrawString(_myGame._screenGame.Police, $"Vous avez survecu {Math.Round(_myGame._screenGame.ChronoGeneral, 0)} secondes!", new Vector2(Game1._WINDOWWIDTH / 2 - 200, Game1._WINDOWSIZE / 2), Color.White);
		_myGame.SpriteBatch.DrawString(_myGame._screenGame.Police, $"Vous avez tue {_myGame._screenGame.aliensTue} aliens.", new Vector2(Game1._WINDOWWIDTH / 2 - 150, Game1._WINDOWSIZE / 2 + 50), Color.White);

		_myGame.SpriteBatch.End();
	}
}

