using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using SAE101;
using MonoGame.Extended.Serialization;

public class MyScreen1 : GameScreen
	{
		private Game1 _myGame;
		private TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
	private SpriteBatch _spriteBatch { get; set; }
	private AnimatedSprite _perso;
	Player _joueur = new Player("Jed");
	private Vector2 _positionPerso;
	private KeyboardState _keyboardState;
	private TiledMapTileLayer mapLayer;

	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1
	public MyScreen1(Game1 game) : base(game)
		{
			_myGame = game;
		}
    public override void Initialize()
    {
		_positionPerso = new Vector2(Game1._WINDOWSIZE/2, Game1._WINDOWSIZE / 2);
		base.Initialize();
    }
    public override void LoadContent()
		{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheet = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		_perso = new AnimatedSprite(spriteSheet);

		_tiledMap = Content.Load<TiledMap>("map_V1");
		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

		mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		base.LoadContent();
	}
		public override void Update(GameTime gameTime)
		{
		//_joueur.Deplacer(gameTime,out _positionPerso);
		float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		_tiledMapRenderer.Update(gameTime);
		_keyboardState = Keyboard.GetState();
		_perso.Update(deltaTime);

		float walkSpeed = deltaTime * _joueur.Speed; // Vitesse de déplacement du sprite
		String animation = "idle";
		if (_keyboardState.IsKeyDown(Keys.Up))
		{
			ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
			ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight - 1);
			animation = "walkNorth";
			if (!IsCollision(tx, ty))
				_positionPerso.Y -= walkSpeed;
		}
		if (_keyboardState.IsKeyDown(Keys.Down))
		{
			ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
			ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
			animation = "walkSouth";
			if (!IsCollision(tx, ty))
				_positionPerso.Y += walkSpeed;
		}
		if (_keyboardState.IsKeyDown(Keys.Left))
		{
			ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth-1);
			ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight );
			animation = "walkWest";
			if (!IsCollision(tx, ty))
				_positionPerso.X -= walkSpeed;
		}
		if (_keyboardState.IsKeyDown(Keys.Right))
		{
			ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth+1);
			ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
			animation = "walkEast";
			if (!IsCollision(tx, ty))
				_positionPerso.X += walkSpeed;
		}
		_perso.Play(animation);

		/*if (_keyboardState.IsKeyDown(Keys.Right))
		{
			_positionPerso.X += _joueur.Speed * deltaTime;
			_perso.Play("walkEast");
		}

		else if (_keyboardState.IsKeyDown(Keys.Left))
		{
			_positionPerso.X -= _joueur.Speed * deltaTime;
			_perso.Play("walkWest");
		}
		else if (_keyboardState.IsKeyDown(Keys.Down))
		{
			_positionPerso.Y += _joueur.Speed * deltaTime;
			_perso.Play("walkSouth");
		}

		else if (_keyboardState.IsKeyDown(Keys.Up))
		{
			_positionPerso.Y -= _joueur.Speed * deltaTime;
			_perso.Play("walkNorth");

		}
		else
			_perso.Play("idle");*/
	}
		public override void Draw(GameTime gameTime)
		{
		_tiledMapRenderer.Draw();
		_spriteBatch.Begin();
		_spriteBatch.Draw(_perso, _positionPerso);
		_spriteBatch.End();




		// on utilise la reference vers
		// Game1 pour chnager le graphisme
	}
	private bool IsCollision(ushort x, ushort y)
	{
		// définition de tile qui peut être null (?)
		TiledMapTile? tile;
		if (mapLayer.TryGetTile(x, y, out tile) == false)
			return false;
		if (!tile.Value.IsBlank)
			return true;
		return false;
	}
}

