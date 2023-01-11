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
using Microsoft.Xna.Framework.Media;

public class ScreenMenu : GameScreen
{
	private Game1 _myGame;
	private Texture2D _menuBackground, _resumeButtonTexture, _optionsButtonTexture, _mainMenuButtonTexture;
	private Rectangle _resumeButton, _mainMenuButton, _optionsButton;
	private SoundEffect _buttonSound;
	MouseState _mouseState;

	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1

	public ScreenMenu(Game1 game) : base(game)
	{
		_myGame = game;

		this._resumeButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 200, 128, 32);
		this._optionsButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 300, 128, 32);
		this._mainMenuButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 400, 128, 32);

		
	}

	public override void LoadContent()
	{
		_menuBackground = Content.Load<Texture2D>("gameMenuBackground");
		_resumeButtonTexture = Content.Load<Texture2D>("gameMenuResume");
		_optionsButtonTexture = Content.Load<Texture2D>("gameMenuOptions");
		_mainMenuButtonTexture = Content.Load<Texture2D>("gameMenuBackToMainMenu");
		_buttonSound = Content.Load<SoundEffect>("buttonSound");
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		_mouseState = Mouse.GetState();
		bool mouseClickOnContinue = _resumeButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnOptions = _optionsButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		bool mouseClickOnMainMenu = _mainMenuButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;

		if (mouseClickOnContinue &&
			_myGame._gameState == "Menu")
		{
			_myGame.IsMouseVisible = false;

			_buttonSound.Play();
			_myGame.LoadGameScreen();
		}
		else if (mouseClickOnMainMenu)
		{
			//gameReset();
			_myGame._previousGameState = _myGame._gameState;
			_buttonSound.Play();
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
		_myGame.SpriteBatch.Draw(_menuBackground, new Vector2(0, 0), Color.White);
		_myGame.SpriteBatch.Draw(this._resumeButtonTexture, new Vector2(this._resumeButton.X, this._resumeButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._optionsButtonTexture, new Vector2(this._optionsButton.X, this._optionsButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._mainMenuButtonTexture, new Vector2(this._mainMenuButton.X, this._mainMenuButton.Y), Color.White);
		_myGame.SpriteBatch.End();
	}
}

