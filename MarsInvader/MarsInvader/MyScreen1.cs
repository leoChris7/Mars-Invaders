using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Serialization;
using MarsInvader;
using SAE101;
using System.Collections.Generic;

public class MyScreen1 : GameScreen
	{
		private Game1 _myGame;
		public TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
		private SpriteBatch _spriteBatch { get; set; }

		
		private TiledMapTileLayer mapLayer;
		private Texture2D _target;
		private Target gameTarget;
		private Texture2D _bullet;
		private List<Bullet> Bullets = new List<Bullet> { };

		Player _joueur;
		private int _chrono;
		private float _deltaTime;
		private int _coefficient;
		private float _targetOffset;
		private double _distanceX;
		private double _distanceY;
		private double _angle;
		private float _vitesseBalle;

	public MyScreen1(Game1 game) : base(game)
		{
		// INITIALIZE
		this.gameTarget = new Target(_target);

		_myGame = game;
		_myGame.IsMouseVisible = false;
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

    public override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		
		_bullet = Content.Load<Texture2D>("bullet");
		_tiledMap = Content.Load<TiledMap>("map_V1");
		mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		_target = Content.Load<Texture2D>("cible");

		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		_joueur = new Player("Jed", _tiledMap, mapLayer, spriteSheetAstro);
		base.LoadContent();
	}

	public override void Update(GameTime gameTime)
	{
		_deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

		this._joueur.Deplacer(gameTime);
		this.GameTarget.PlaceTarget();
		this._tiledMapRenderer.Update(gameTime);
		Chrono += 1;

		shootingBullets();

		if (Chrono > 40)
        {
			// Joueur, Cible, Vitesse
			Bullets.Add(new Bullet(_joueur, GameTarget, 100));
			Chrono = 0;
        }

	}
		public override void Draw(GameTime gameTime)
		{
		_tiledMapRenderer.Draw();

		_spriteBatch.Begin();

		_spriteBatch.Draw(_joueur.Perso, _joueur.PositionPerso);
		_spriteBatch.Draw(_target, this.GameTarget.PositionTarget, Color.White);

		// On dessine chaque balle sur l'écran
		foreach(Bullet _playerBullet in this.Bullets)
			_spriteBatch.Draw(_bullet, new Vector2(_playerBullet.BulletPosition.X, _playerBullet.BulletPosition.Y), Color.White);

		_spriteBatch.End();
	}

	public void shootingBullets()
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
			if (Bullets[i].BulletPosition.X > Game1._WINDOWSIZE || Bullets[i].BulletPosition.Y > Game1._WINDOWSIZE ||
			Bullets[i].BulletPosition.X < 0 || Bullets[i].BulletPosition.Y < 0)
			{
				this.Bullets.RemoveAt(i);
				continue;
			}
		}
	}


}

