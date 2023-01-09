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

public class ScreenGame : GameScreen
	{
		private Game1 _myGame;
		public TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
		private SpriteBatch _spriteBatch { get; set; }
		Player _joueur;
		private List<Alien> _aliens;
		Coeur[] _coeur = new Coeur[5];
		public Texture2D _coeurFull;
		public Texture2D _coeurHigh;
		public Texture2D _coeurHalf;
		public Texture2D _coeurLow;
		public Texture2D _coeurVide;
		public SpriteSheet spriteSheetAlien1;
		public SpriteSheet spriteSheetAlien2;
		public SpriteSheet spriteSheetAlien3;
		public SpriteSheet spriteSheetAlien4;

	private Texture2D _cible;
		private TiledMapTileLayer mapLayer;
		private Texture2D _target;
		private Target gameTarget;
		private Texture2D _bullet;
		private List<Bullet> Bullets = new List<Bullet> { };

		private int _chrono;
		private float _deltaTime;
		private int _coefficient;
		private float _targetOffset;
		private double _distanceX;
		private double _distanceY;
		private double _angle;
		private float _vitesseBalle;

	public ScreenGame(Game1 game) : base(game)
		{
		// INITIALIZE
		this.gameTarget = new Target(_target);
		this.Aliens = new List<Alien>();
		_myGame = game;
		Chrono = 0;
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

    public int Chrono
    {
        get
        {
            return this._chrono;
        }

        set
        {
			// On vérifie que le chrono est supérieur ou égal à 0 et qu'il ne soit pas nul ou vide
			if (value >= 0 && !String.IsNullOrEmpty(value.ToString()))
				this._chrono = value;
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

    public override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		
		_bullet = Content.Load<Texture2D>("bullet");
		_coeurFull = Content.Load<Texture2D>("coeurFull");
		_coeurHigh = Content.Load<Texture2D>("coeurHigh");
		_coeurHalf = Content.Load<Texture2D>("coeurHalf");
		_coeurLow = Content.Load<Texture2D>("coeurLow");
		_coeurVide = Content.Load<Texture2D>("coeurVide");
		SpriteSheet spriteSheetAlien1 = Content.Load<SpriteSheet>("alienLV1.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien2 = Content.Load<SpriteSheet>("alienLV2.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien3 = Content.Load<SpriteSheet>("alienLV3.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien4 = Content.Load<SpriteSheet>("alienLV4.sf", new JsonContentLoader());

		_cible = Content.Load<Texture2D>("cible");
		_bullet = Content.Load<Texture2D>("bullet");
		_tiledMap = Content.Load<TiledMap>("map_V1");
		_target = Content.Load<Texture2D>("cible");

		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		_joueur  = new Player("Jed",_tiledMap, mapLayer, spriteSheetAstro);
		for (int i = 0; i < 10; i++)
		{
			Aliens.Add(new Alien(1, _tiledMap , spriteSheetAlien1));
		}

		for (int i = 0; i < 5; i++)
		{
			_coeur[i] = new Coeur(5,i,  _coeurVide);

		}

		_joueur = new Player("Jed", _tiledMap, mapLayer, spriteSheetAstro);
		base.LoadContent();
	}



	public override void Update(GameTime gameTime)
	{
		// barreVie = new Rectangle(Game1._WINDOWSIZE +50, 100, _joueur.Health, 10);
		_joueur.Deplacer(gameTime);

		for (int i = 0; i < this.Aliens.Count; i++)
		{
			// On update 
			for (int j = 0; j < _aliens[i].nbAliensSpawn(1, _aliens); j++)
			{
				Aliens.Add(new Alien(1, _tiledMap, spriteSheetAlien1/*, spriteSheetAlien2, spriteSheetAlien3, spriteSheetAlien4*/));
			}
			for (int j = 0; j < _aliens[i].nbAliensSpawn(2, _aliens); j++)
			{
				Aliens.Add(new Alien(2, _tiledMap, spriteSheetAlien2));
			}
			for (int j = 0; j < _aliens[i].nbAliensSpawn(3, _aliens); j++)
			{
				Aliens.Add(new Alien(3, _tiledMap, spriteSheetAlien3));
			}
			for (int j = 0; j < _aliens[i].nbAliensSpawn(4, _aliens); j++)
			{
				Aliens.Add(new Alien(4, _tiledMap, spriteSheetAlien4));
			}

			this.Aliens[i].updateAlien(gameTime, _joueur.PositionPerso);

			for (int j = i+1; j < this.Aliens.Count; j++)
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
			}
		}

		for (int i = 0; i < 5; i++)
		{
			_coeur[i].VieCalcul(i,_joueur, _coeurFull, _coeurHigh, _coeurHalf, _coeurLow, _coeurVide);

		}

		_tiledMapRenderer.Update(gameTime);
		_deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

		this._joueur.Deplacer(gameTime);
		this.GameTarget.PlaceTarget();
		this._tiledMapRenderer.Update(gameTime);
		Chrono += 1;

		shootingBullets();

		if (Chrono > 40)
        {
			// Joueur, Cible, Vitesse
			Bullets.Add(new Bullet(_joueur, GameTarget, 400));
			Chrono = 0;
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

		// On dessine la cible
		_spriteBatch.Draw(_target, this.GameTarget.PositionTarget, Color.White);

		// On dessine chaque balle sur l'écran
		foreach(Bullet _playerBullet in this.Bullets)
			_spriteBatch.Draw(_bullet, new Vector2(_playerBullet.BulletPosition.X, _playerBullet.BulletPosition.Y), Color.White);

		_spriteBatch.End();
	}


	public int shootingBullets()
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
					Console.WriteLine(Aliens.Count);
					if (this.Bullets[i]._hitBox.Intersects(_alien.hitBox))
				{
						_alien.Health -= 50;
					
						this.Bullets.RemoveAt(i);
						return 0;
						
					}
					if (_alien.Health <= 0)
					{
						this.Aliens.Remove(_alien);
					return 0;
					}
			}	
		}
		return 1;
	}
	
}

