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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public class ScreenGame : GameScreen
{
	private Game1 _myGame;
	public TiledMap _tiledMap;
	private TiledMapRenderer _tiledMapRenderer;
	private SpriteBatch _spriteBatch { get; set; }
	public Player _joueur;
	private List<Alien> _aliens;
	Coeur[] _coeur = new Coeur[5];
	public Texture2D _coeurFull;
	public Texture2D _coeurHigh;
	public Texture2D _coeurHalf;
	public Texture2D _coeurLow;
	public Texture2D _coeurVide;

	public SpriteSheet spriteSheetAstro;
	public SpriteSheet spriteSheetAlien1;
	public SpriteSheet spriteSheetAlien2;
	public SpriteSheet spriteSheetAlien3;
	public SpriteSheet spriteSheetAlien4;

	private TiledMapTileLayer mapLayer;
	private Texture2D _cible;
	private Texture2D _target;
	private Target gameTarget;
	private Texture2D _bullet;
	private List<Bullet> Bullets = new List<Bullet> { };
	private Vector2 ExpPos;
	private Vector2 NivPos;

	private SoundEffect _bulletSound, _gameOverSoundEffect, _buttonSound;
	private Song _gameMusic;


		private Rectangle _resumeButton, _optionsButton, _mainMenuButton;
		private float _chronoGeneral;
		public int aliensTue;
		public int Exp;
		public int ExpLvlUp;
		private float _chronoBullet;
		private float _deltaTime;
		private int _coefficient;
		private float _targetOffset;
		private double _distanceX;
		private double _distanceY;
		private double _angle;
		private float _vitesseBalle;
		private SpriteFont _police;
		public bool respawn;
		MouseState _mouseState;
	public double fireSpeed;
	KeyboardState keyboardState;


	public ScreenGame(Game1 game) : base(game)
		{
		// INITIALIZE
		respawn = false;
		this.gameTarget = new Target(_target);
		_myGame = game;
		ChronoGeneral = 0;
		ChronoBullet = 0;
		ExpLvlUp = 10;
		fireSpeed = 1.2;
		Exp = 0;
		aliensTue = 0;
		ExpPos = new Vector2(Game1._WINDOWSIZE + 10 , 150);
		NivPos = new Vector2(Game1._WINDOWSIZE + 10, 200);

	}

	public bool SourisSurRect(Rectangle rect)
	{
		_mouseState = Mouse.GetState();
		return rect.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
	}

	public Target GameTarget
	{
		get
		{
			return this.gameTarget;
		}

		set
		{
			this.gameTarget = value;
		}
	}

    public float ChronoBullet
    {
        get
        {
            return this._chronoBullet;
        }

        set
        {
			// On vérifie que le chrono est supérieur ou égal à 0 et qu'il ne soit pas nul ou vide
			if (value >= 0 && !String.IsNullOrEmpty(value.ToString()))
				this._chronoBullet = value;
			else
				throw new ArgumentException("Le chrono doit être supérieur ou égal à 0 et non null.");
        }
    }

    public int Coefficient
    {
        get
        {
            return this._coefficient;
        }

        set
        {
			// On vérifie que le coefficient est égal à 1 ou -1 et qu'il ne soit pas nul ou vide. 
			if ((value == 1 || value == -1) && !String.IsNullOrEmpty(value.ToString()))
				this._coefficient = value;
			else
				throw new ArgumentException("Le coefficient du vecteur doit être égal à 1 ou -1 et non nul.");
        }
    }

    internal List<Alien> Aliens
    {
        get
        {
            return this._aliens;
        }

        set
        {
            this._aliens = value;
        }
    }

    public TiledMapTileLayer MapLayer
    {
        get
        {
            return this.mapLayer;
        }

        set
        {
            this.mapLayer = value;
        }
    }

    public float ChronoGeneral
    {
        get
        {
            return this._chronoGeneral;
        }

        set
        {
            this._chronoGeneral = value;
        }
    }

    public SpriteFont Police
    {
        get
        {
            return this._police;
        }

        set
        {
            this._police = value;
        }
    }

    public override void LoadContent()
	{
		if (_myGame._previousGameState != "Menu")

        {
			Exp = 0;
			ExpLvlUp = 10;
			aliensTue = 0;
			respawn = false;
			ChronoGeneral = 0;
			ChronoBullet = 0;
			fireSpeed = 1.2;

			_spriteBatch = new SpriteBatch(GraphicsDevice);

		_coeurFull = Content.Load<Texture2D>("coeurFull");
		_coeurHigh = Content.Load<Texture2D>("coeurHigh");
		_coeurHalf = Content.Load<Texture2D>("coeurHalf");
		_coeurLow = Content.Load<Texture2D>("coeurLow");
		_coeurVide = Content.Load<Texture2D>("coeurVide");

		_cible = Content.Load<Texture2D>("cible");
		_bullet = Content.Load<Texture2D>("bullet");
		_tiledMap = Content.Load<TiledMap>("map_V1");
		_target = Content.Load<Texture2D>("cible");

		 spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		 spriteSheetAlien1 = Content.Load<SpriteSheet>("alienLV1.sf", new JsonContentLoader());
		 spriteSheetAlien2 = Content.Load<SpriteSheet>("alienLV2.sf", new JsonContentLoader());
		 spriteSheetAlien3 = Content.Load<SpriteSheet>("alienLV3.sf", new JsonContentLoader());
		 spriteSheetAlien4 = Content.Load<SpriteSheet>("alienLV4.sf", new JsonContentLoader());
		Police = Content.Load<SpriteFont>("fontPauseMenu");

		_buttonSound = Content.Load<SoundEffect>("buttonSound");
		_bulletSound = Content.Load<SoundEffect>("bulletFired");
		_gameOverSoundEffect = Content.Load<SoundEffect>("gameOverSoundEffect");
		_gameMusic = Content.Load<Song>("gameMusic");

		if (!(MediaPlayer.State == MediaState.Playing) && _myGame._gameState == "Game")
			playingMusic();
			

		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		MapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		this.Aliens = new List<Alien>();
			 List<Bullet> Bullets = new List<Bullet> ();

			for (int i = 0; i < 10; i++)
			{
				Aliens.Add(new Alien(1, _tiledMap, spriteSheetAlien1));
			}

			for (int i = 0; i < 5; i++)
			{
				_coeur[i] = new Coeur(5, i, _coeurVide);

			}
			base.LoadContent();
		}
	}




	public override void Update(GameTime gameTime)
	{
		//LevelUp
		keyboardState = Keyboard.GetState();
		if ((keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.L)))
		{
			Exp+=100;
		}
		//stat up
		if ((keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.OemPlus)))
		{
			_joueur.LevelUp(ref fireSpeed);
		}
		//godmode
		if ((keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.G)))
		{
			_joueur.Health=10000;
		}
		//Healt normal
		if ((keyboardState.IsKeyDown(Keys.LeftControl) && keyboardState.IsKeyDown(Keys.OemMinus)))
		{
			_joueur.Health = 100;
		}
		playingMusic();

		if (_myGame._gameState == "Menu")
		{
			_mouseState = Mouse.GetState();
			bool mouseClickOnContinue = _resumeButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
			bool mouseClickOnOptions = _optionsButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
			bool mouseClickOnMainMenu = _mainMenuButton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed;
		

			if (mouseClickOnContinue &&
				_myGame._gameState == "Menu")
			{
				_myGame.IsMouseVisible = false;

				_myGame.LoadGameScreen();
			}
			else if (mouseClickOnMainMenu)
			{
				//gameReset();
				_myGame._previousGameState = _myGame._gameState;
				_myGame.LoadStartingScreen();

			}
		}
		else if (_myGame._gameState == "Game")
		{


			_joueur.Deplacer(gameTime);
			_joueur.Niveau = _joueur.NiveauCalcul(ref Exp, ref ExpLvlUp, _joueur.Niveau, ref fireSpeed);
			for (int i = 0; i < this.Aliens.Count; i++)
			{
				// On update 


				this.Aliens[i].updateAlien(gameTime, _joueur.PositionPerso, _joueur.Niveau);

				for (int j = i + 1; j < this.Aliens.Count; j++)
				{
					if (this.Aliens[i].hitBox.Intersects(this.Aliens[j].hitBox))
					{
						_aliens[i].directionOppAlien(gameTime, _joueur.PositionPerso);
						break;
					}
				}

				// si les aliens touchent le joueur, enlever de la vie au joueur
				if (this.Aliens[i].hitBox.Intersects(this._joueur.hitBox) && this.Aliens[i].TouchedPlayer == false)
				{
					_joueur.removeHealth(this.Aliens[i].Attack);
					// Période d'invincibilité
					this.Aliens[i].TouchedPlayer = true;
					if (_joueur.Health <= 0)
					{
						_myGame._gameState = "GameOver";
						MediaPlayer.Stop();
						playingSound(_gameOverSoundEffect);
						_myGame.LoadGameOverScreen();
					}
				}
			}


		for (int i = 0; i < 5; i++)
		{
			_coeur[i].VieCalcul(i, _joueur, _coeurFull, _coeurHigh, _coeurHalf, _coeurLow, _coeurVide);
		}

		_tiledMapRenderer.Update(gameTime);
		_deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			
		this._joueur.Deplacer(gameTime);
		this.GameTarget.PlaceTarget();
		this._tiledMapRenderer.Update(gameTime);
		
		if (_myGame._gameState == "Game")
			this.ChronoGeneral += _deltaTime;
		
		// On tire une balle
		shootingBullet();

		// On vérifie que la collection de balles n'est pas vide avant d'executer la méthode, pour éviter une éxécution inutile.
		if (this.Bullets != new List<Bullet>())
			bulletManagement();

		if (respawn)
			{
				for (int j = 0; j < _aliens[j].nbAliensSpawn(1, _aliens); j++)
				{
					Aliens.Add(new Alien(1, _tiledMap, spriteSheetAlien1));
                    
				}
				for (int j = 0; j < _aliens[j].nbAliensSpawn(2, _aliens); j++)
				{
					Aliens.Add(new Alien(2, _tiledMap, spriteSheetAlien2));
				}
				for (int j = 0; j < _aliens[j].nbAliensSpawn(3, _aliens); j++)
				{
					Aliens.Add(new Alien(3, _tiledMap, spriteSheetAlien3));
				}
				for (int j = 0; j < _aliens[j].nbAliensSpawn(4, _aliens); j++)
				{
					Aliens.Add(new Alien(4, _tiledMap, spriteSheetAlien4));
				}
			}

		}
    }
		public override void Draw(GameTime gameTime)
		{
		// On réinitialise le fond en couleur CornflowereBlue
		_myGame.GraphicsDevice.Clear(Color.Black);

		// On dessine la map
		_tiledMapRenderer.Draw();

		// On commence à dessiner les objets
		_spriteBatch.Begin();

		_spriteBatch.Draw(_joueur.Perso, _joueur.PositionPerso);

		// On dessine les aliens
		for (int i = 0; i < this.Aliens.Count; i++)
		{
			_spriteBatch.Draw(_aliens[i].AlienTexture, _aliens[i].PositionAlien);
		}
		for (int i = 0; i < 5; i++)
		{
			_spriteBatch.Draw(_coeur[i].VieTexture, _coeur[i].PositionCoeur, Color.White);
		}
		_spriteBatch.DrawString( Police, Exp+"Exp / "+ExpLvlUp +"Exp", ExpPos, Color.White);
		_spriteBatch.DrawString(Police, "Player level : " + _joueur.Niveau, NivPos, Color.White);
		_spriteBatch.DrawString(Police, "Temps : " + Math.Round(ChronoGeneral,2), new Vector2(810, 750), Color.White);
		_spriteBatch.DrawString(Police, $"Session : {_joueur.Pseudo}", new Vector2(0, 770), Color.White);

		// On dessine la cible
		_spriteBatch.Draw(_target, this.GameTarget.PositionTarget, Color.White);

		// On dessine chaque balle sur l'écran
		foreach(Bullet _playerBullet in this.Bullets)
			_spriteBatch.Draw(_bullet, new Vector2(_playerBullet.BulletPosition.X, _playerBullet.BulletPosition.Y), Color.White);

		_spriteBatch.End();
		
	}

	public void shootingBullet()
    /// Cette méthode permet de tirer des balles
	{
		this.ChronoBullet += _deltaTime;
		if (ChronoBullet > fireSpeed)
		{
			// Joueur, Cible, Vitesse
			Bullets.Add(new Bullet(_joueur, GameTarget, 400));
			playingSound(_bulletSound, 0.3f, 0, 0);
			ChronoBullet = 0.1f;
		}
	}

	public void playingSound(SoundEffect Sound)
    {
		if (_myGame.soundMuted == false)
			Sound.Play();
    }

	public void playingSound(SoundEffect Sound, float volume, float pitch, float pan)
	{
		if (_myGame.soundMuted == false)
			Sound.Play(volume, pitch, pan);
	}

	public void playingMusic()
	/// Cette méthode permet de gérer la musique du jeu
    {	
		if (!_myGame.musicMuted)
		{ 
		if (!(MediaPlayer.State == MediaState.Playing) && _myGame._gameState == "Game")
		{ 
			MediaPlayer.Play(_gameMusic);
			MediaPlayer.Volume = 1.5f;
		}

		if (_myGame._gameState == "Menu" || _myGame._gameState == "GeneralMenu")
			MediaPlayer.Volume = 0.5f;
		}
	}


	public int bulletManagement()
	/// Cette méthode gère le jet de balles prenant en compte le joueur ainsi que la cible
	{
		for (int i = 0; i < Bullets.Count; i++)
		{
			_targetOffset = Target.SIZETARGET * 0.5f;
			_distanceX = (this.Bullets[i].TargetPosition.X + _targetOffset) - (this.Bullets[i].PlayerPosition.X + 0.5 * Player.PLAYERSIZE);
			_distanceY = (this.Bullets[i].TargetPosition.Y + _targetOffset) - (this.Bullets[i].PlayerPosition.Y + 0.5 * Player.PLAYERSIZE);
			_angle = Math.Atan((_distanceY) / (_distanceX));

			// On inverse le vecteur si la cible se trouve à gauche du joueur
			if (_distanceX < 0)
				this.Coefficient = -1;
			else
				this.Coefficient = 1;

			// On ajoute le vecteur à la position actuelle de chaque balle pour qu'elle continue sa trajectoire
			_vitesseBalle = this.Bullets[i].ShootingSpeed * _deltaTime;
			Bullets[i].BulletPosition +=
				this.Coefficient * new Vector2(
					(float)Math.Cos(_angle) * _vitesseBalle,
					(float)Math.Sin(_angle) * _vitesseBalle
					);
			// La balle disparaît de la liste quand elle disparaît de l'écran
			if (Bullets[i].BulletPosition.X + Bullet.BULLETSIZE > Game1._WINDOWSIZE || Bullets[i].BulletPosition.Y + Bullet.BULLETSIZE > Game1._WINDOWSIZE ||
			Bullets[i].BulletPosition.X - Bullet.BULLETSIZE < 0 || Bullets[i].BulletPosition.Y - Bullet.BULLETSIZE < 0)
			{
				this.Bullets.RemoveAt(i);
				continue;
			}

			foreach (Alien _alien in _aliens)
				{
					if (this.Bullets[i]._hitBox.Intersects(_alien.hitBox))
				{
						_alien.Health -= 50;
					
						this.Bullets.RemoveAt(i);
						return 0;
						
					}
					if (_alien.Health <= 0)
					{
						respawn = true;
						this.Aliens.Remove(_alien);
						Exp += _alien.Niveau;
						aliensTue++;
						return 0;
					}
			}	
		}
		return 1;
	}
	
}

