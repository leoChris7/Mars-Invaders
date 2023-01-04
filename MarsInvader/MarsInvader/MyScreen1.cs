using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using SAE101;
public class MyScreen1 : GameScreen
	{
		private Game1 _myGame;
		private TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
		// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
		// défini dans Game1
	public MyScreen1(Game1 game) : base(game)
		{
			_myGame = game;
		}
		public override void LoadContent()
		{
			base.LoadContent();
			_tiledMap = Content.Load<TiledMap>("map_V1");
			_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

		}
		public override void Update(GameTime gameTime)
		{
		_tiledMapRenderer.Update(gameTime);
	}
		public override void Draw(GameTime gameTime)
		{
		_tiledMapRenderer.Draw();
		// on utilise la reference vers
	// Game1 pour chnager le graphisme
		}
	}

