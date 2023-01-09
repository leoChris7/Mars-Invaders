using System;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Serialization;
using SAE101;
using System.Collections.Generic;

public class ScreenStarting : GameScreen
{
	private Game1 _myGame;
	private Texture2D _mainBackground, _begin, _options, _leaderboard, _leave;
	private SpriteBatch _spriteBatch { get; set; }

	// Dimensions de la fenêtre
	public const int _WINDOWSIZE = 800;
	public const int _WINDOWWIDTH = 1000;

	/// Dimensions des choix du menu
	public const int _BUTTONWIDTH = 128;
	public const int _BUTTONHEIGHT = 64;


	public Rectangle _beginButton = new Rectangle(450, 200, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _leaderboardButton = new Rectangle(450, 300, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _optionsRectButton = new Rectangle(450, 400, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _leaveButton = new Rectangle(450, 500, _BUTTONWIDTH, _BUTTONHEIGHT);

	public ScreenStarting(Game1 game) : base(game)
	{
		// INITIALIZE
		_myGame = game;
		_myGame._gameState = "GeneralMenu";
		Content.RootDirectory = "Content";
		game.IsMouseVisible = true;
	}


	public override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());

		_mainBackground = Content.Load<Texture2D>("MainMenu");
		_options = Content.Load<Texture2D>("optionsGeneralMenu");
		_begin = Content.Load<Texture2D>("beginGeneralMenu");
		_leaderboard = Content.Load<Texture2D>("leaderboardGeneralMenu");
		_leave = Content.Load<Texture2D>("leaveGeneralMenu");

		base.LoadContent();
	}



	public override void Update(GameTime gameTime)
	{

	}
	public override void Draw(GameTime gameTime)
	{
		// On réinitialise le fond
		_myGame.GraphicsDevice.Clear(Color.Black);
		
		// On commence à dessiner les boutons
		_spriteBatch.Begin();

		_spriteBatch.Draw(_mainBackground, new Vector2(0, 0), Color.White);
		_spriteBatch.Draw(_begin, new Vector2(450, 200), Color.White);
		_spriteBatch.Draw(_leaderboard, new Vector2(450, 300), Color.White);
		_spriteBatch.Draw(_options, new Vector2(450, 400), Color.White);
		_spriteBatch.Draw(_leave, new Vector2(450, 500), Color.White);

		_spriteBatch.End();
	}


}

