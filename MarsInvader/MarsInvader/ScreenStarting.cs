using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using Microsoft.Xna.Framework.Media;

public class ScreenStarting : GameScreen
{
	private Game1 _myGame;
	private Texture2D _mainBackground, _begin, _options, _leaderboard, _leave, _controles;

	public Rectangle _beginButton = new Rectangle(450, 200, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _leaderboardButton = new Rectangle(450, 300, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _optionsRectButton = new Rectangle(450, 400, _BUTTONWIDTH, _BUTTONHEIGHT);
	public Rectangle _leaveButton = new Rectangle(450, 500, _BUTTONWIDTH, _BUTTONHEIGHT);

	// Dimensions de la fenêtre
	public const int _WINDOWSIZE = 800;
	public const int _WINDOWWIDTH = 1000;

	/// Dimensions des choix du menu
	public const int _BUTTONWIDTH = 128;
	public const int _BUTTONHEIGHT = 64;

	public ScreenStarting(Game1 game) : base(game)
	{
		_myGame = game;
		_myGame._gameState = "GeneralMenu";
		Content.RootDirectory = "Content";
		game.IsMouseVisible = true;
	}


	public override void LoadContent()
	{
		MediaPlayer.Stop();
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());

		_mainBackground = Content.Load<Texture2D>("MainMenu");
		_options = Content.Load<Texture2D>("optionsGeneralMenu");
		_begin = Content.Load<Texture2D>("beginGeneralMenu");
		_leaderboard = Content.Load<Texture2D>("leaderboardGeneralMenu");
		_leave = Content.Load<Texture2D>("leaveGeneralMenu");
		_controles = Content.Load<Texture2D>("Controles");

		base.LoadContent();
	}

	public static bool SourisSurRect(Rectangle rect)
	{
		MouseState _mouseState = Mouse.GetState();
		return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
	}

	public int soundButtons()
    {
		MouseState _mouseState = Mouse.GetState();

		if (SourisSurRect(_myGame._mutedSounds) && _myGame.soundMuted == false)
		{
			_myGame.soundMuted = true;
			return 1;
		}

		else if (SourisSurRect(_myGame._mutedSounds) && _myGame.soundMuted == true)
		{
			_myGame.soundMuted = false;
			_myGame._screenGame.playingSound(_myGame._buttonSound);
			return 1;
		}

		if (SourisSurRect(_myGame._mutedMusic) && _myGame.musicMuted == false)
		{
			_myGame.musicMuted = true;
			return 1;
		}
		else if (SourisSurRect(_myGame._mutedMusic) && _myGame.musicMuted == true)
		{
			_myGame.musicMuted = false;
			_myGame._screenGame.playingSound(_myGame._buttonSound);
			return 1;
		}
		return 0;
	}

	public override void Update(GameTime gameTime)
	{
		soundButtons();
	}
	public override void Draw(GameTime gameTime)
	{
		// On réinitialise le fond
		_myGame.GraphicsDevice.Clear(Color.Black);
		
		// On commence à dessiner les boutons
		_myGame.SpriteBatch.Begin();

		_myGame.SpriteBatch.Draw(_mainBackground, new Vector2(0, 0), Color.White);
		_myGame.SpriteBatch.Draw(_begin, new Vector2(450, 200), Color.White);
		_myGame.SpriteBatch.Draw(_leaderboard, new Vector2(450, 300), Color.White);
		_myGame.SpriteBatch.Draw(_options, new Vector2(450, 400), Color.White);
		_myGame.SpriteBatch.Draw(_leave, new Vector2(450, 500), Color.White);
		_myGame.SpriteBatch.Draw(_controles, new Vector2(780, 20), Color.White);

		_myGame.SpriteBatch.Draw(_myGame._volumeTexture, new Vector2(_myGame._mutedSounds.X, _myGame._mutedSounds.Y), Color.White);
		_myGame.SpriteBatch.Draw(_myGame._musicalNoteTexture, new Vector2(_myGame._mutedMusic.X, _myGame._mutedMusic.Y), Color.White);

		if (_myGame.musicMuted)
			_myGame.SpriteBatch.Draw(_myGame._redCrossTexture, new Vector2(_myGame._mutedMusic.X, _myGame._mutedMusic.Y), Color.White);
		if (_myGame.soundMuted)
			_myGame.SpriteBatch.Draw(_myGame._redCrossTexture, new Vector2(_myGame._mutedSounds.X, _myGame._mutedSounds.Y), Color.White);

		_myGame.SpriteBatch.End();

	}


}

