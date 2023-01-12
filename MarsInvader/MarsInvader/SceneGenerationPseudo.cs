using System;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SAE101;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;

public class ScreenGenerationPseudo : GameScreen
{
	private Game1 _myGame;
	private readonly ScreenManager _screenManager;
    private Player player;

	private TiledMap _tiledMap;
	private SpriteSheet spriteSheetAstro;
	private TiledMapTileLayer MapLayer;

	MouseState _mouseState;

	private Texture2D _background, _generationButtonTexture, _continueButtonTexture, _backButtonTexture, _titleTexture;
	private Rectangle _generationButton, _continueButton, _backButton;

	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1

	private String[] name1 = new String[]
{
		"Malin", "Malicieux", "Gourmand", "Chanceux", "Fort", "Courtois", "Habile",
		"Curieux", "Sage", "Agile", "Minutieux", "Agreable", "Aventureux", "Astucieux",
		"Audacieux", "Bienveillant", "Charismatique", "Concentre", "Confiant", "Drole",
		"Efficace", "Habile", "Humble", "Infatigable", "Ingenieux", "Inspire", "Innovateur",
		"Perseverant", "Ponctuel", "Polyvalent", "Ruse", "Responsable", "Spontane", "Sympathique",
		"Habille", "Vert", "Rouge", "Bleu", "Orange", "Multicolore", "Rose", "Violet",
		"Mysterieux"
};

	private String[] name2 = new string[]
	{
		"Renard ", "Ours ", "Chat ", "Zebre ", "Souris ", "Aigle ", "Oisillon ", "Rinoceros ",
		"Chauve-Souris ", "Pivert ", "Chiot ", "Voiture ", "Vaisseau ", "Bateau ", "Abeille ",
		"Araignee ", "Alouette ", "Autruche ", "Antilope ", "Alpaga ",  "Anguille ", "Brebis ",
		"Beluga ", "Crabe ", "Castor ", "Cigale ", "Chouette ", "Capybara ", "Dinosaure ",
		"Daim ", "Dragon de Komodo ", "Elan ", "Ver ", "Faucon ", "Fourmis ", "Gendarme ",
		"Tigre ", "Leopard ", "Guepard ", "Girafe ", "Guepe ", "Bourdon ", "Grenouille ",
		"Hamster ", "Hermine ", "Iguane ", "Lezard "
	};

	private SpriteFont Police;
	private String newPseudo;

    public string[] Name1
    {
        get
        {
            return this.name1;
        }

        set
        {
            this.name1 = value;
        }
    }

    public string[] Name2
    {
        get
        {
            return this.name2;
        }

        set
        {
            this.name2 = value;
        }
    }

    public Player Player
    {
        get
        {
            return this.player;
        }

        set
        {
            this.player = value;
        }
    }

    public ScreenGenerationPseudo(Game1 game) : base(game)
	{
		_myGame = game;
		_screenManager = new ScreenManager();
		this._generationButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 200, 300, 128, 32);
		this._continueButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 500, 128, 32);
		this._backButton = new Rectangle((int)(float)Game1._WINDOWWIDTH / 2 - 64, 600, 128, 32);
		this.newPseudo = playerNameGenerator();
	}

	public override void LoadContent()
	{
		_background = Content.Load<Texture2D>("gameMenuBackground");
		_generationButtonTexture = Content.Load<Texture2D>("pseudoGengenerateButton");
		_continueButtonTexture = Content.Load<Texture2D>("pseudoGenContinueButton");
		_backButtonTexture = Content.Load<Texture2D>("pseudoGenbackButton");
		_titleTexture = Content.Load<Texture2D>("pseudoGenTitle");
		Police = Content.Load<SpriteFont>("fontPauseMenu");
		
		this._tiledMap = Content.Load<TiledMap>("map_V1");
		this.spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		this.MapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");

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
			_myGame._screenGame.playingSound(_myGame._buttonSound);
			_myGame.LoadStartingScreen();
		}

		else if (mouseClickOnContinue && _myGame._gameState == "Pseudo")
		{
			_myGame._screenGame._joueur = new Player(newPseudo, _tiledMap, MapLayer, spriteSheetAstro);
			_myGame._screenGame.playingSound(_myGame._buttonSound);
			_myGame.LoadGameScreen();
		} 

		else if (mouseClickOnGenerate)
        {
			_myGame._screenGame.playingSound(_myGame._buttonSound);
			this.newPseudo = playerNameGenerator();
		}
	}

	public bool SourisSurRect(Rectangle rect)
	{
		_mouseState = Mouse.GetState();
		return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
	}

	public String playerNameGenerator()
	{
		Random r = new Random();
		int randomT1 = r.Next(0, Name2.Length);
		int randomT2 = r.Next(0, Name1.Length);

		return Name2[randomT1] + Name1[randomT2];
	}


	public override void Draw(GameTime gameTime)
	{
		_myGame.GraphicsDevice.Clear(Color.Black);

		_myGame.SpriteBatch.Begin();

		_myGame.SpriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
		_myGame.SpriteBatch.Draw(_titleTexture, new Vector2(Game1._WINDOWWIDTH / 2 - 0.5f*_titleTexture.Width, 200), Color.White);

		_myGame.SpriteBatch.Draw(this._generationButtonTexture, new Vector2(this._generationButton.X, this._generationButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._backButtonTexture, new Vector2(this._backButton.X, this._backButton.Y), Color.White);
		_myGame.SpriteBatch.Draw(this._continueButtonTexture, new Vector2(this._continueButton.X, this._continueButton.Y), Color.White);

		_myGame.SpriteBatch.DrawString(Police, $"Pseudo actuel : {this.newPseudo}", new Vector2(this._generationButton.X + 150, this._generationButton.Y), Color.White);
		

		_myGame.SpriteBatch.End();
	}

}

