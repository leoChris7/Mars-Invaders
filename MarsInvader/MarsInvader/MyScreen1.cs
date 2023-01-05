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
		public TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
		private SpriteBatch _spriteBatch { get; set; }
		Player _joueur;
		private TiledMapTileLayer mapLayer;
		private Texture2D _cible;

	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1
	public MyScreen1(Game1 game) : base(game)
		{
			_myGame = game;
		}
    public override void Initialize()
    {
		base.Initialize();
    }
    public override void LoadContent()
		{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());

		_cible = Content.Load<Texture2D>("cible");
		_tiledMap = Content.Load<TiledMap>("map_V1");
		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		_joueur  = new Player("Jed",_tiledMap, mapLayer, spriteSheetAstro);
		base.LoadContent();
	}
	public override void Update(GameTime gameTime)
	{
		_joueur.Deplacer(gameTime);
		_tiledMapRenderer.Update(gameTime);
	}
		public override void Draw(GameTime gameTime)
		{
		_tiledMapRenderer.Draw();
		_spriteBatch.Begin();
		_spriteBatch.Draw(_joueur.Perso, _joueur.PositionPerso);
		_spriteBatch.End();




		// on utilise la reference vers
		// Game1 pour chnager le graphisme
	}
	
}

