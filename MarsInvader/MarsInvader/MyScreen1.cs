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
	public SpriteBatch _spriteBatch { get; set; }
	private Vector2 _positionPerso;
	private AnimatedSprite _perso;
	private KeyboardState _keyboardState;
	private int _vitesse;
	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1
	public MyScreen1(Game1 game) : base(game)
		{
			_myGame = game;
		}
    public override void Initialize()
    {
		_positionPerso = new Vector2(20, 340);
		_vitesse = 100;
		base.Initialize();
    }
    public override void LoadContent()
		{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheet = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		_perso = new AnimatedSprite(spriteSheet);
		
		_tiledMap = Content.Load<TiledMap>("map_V1");
		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		base.LoadContent();
	}
		public override void Update(GameTime gameTime)
		{
		
		float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		_tiledMapRenderer.Update(gameTime);
		_keyboardState = Keyboard.GetState();

		if (_keyboardState.IsKeyDown(Keys.Right))
		{
			_positionPerso.X += _vitesse * deltaTime;
			_perso.Play("walkEast");
		}

		else if (_keyboardState.IsKeyDown(Keys.Left))
		{
			_positionPerso.X -= _vitesse * deltaTime;
			_perso.Play("walkWest");
		}
		else if (_keyboardState.IsKeyDown(Keys.Down))
		{
			_positionPerso.Y += _vitesse * deltaTime;
			_perso.Play("walkSouth");
		}

		else if (_keyboardState.IsKeyDown(Keys.Up))
		{
			_positionPerso.Y -= _vitesse * deltaTime;
			_perso.Play("walkNorth");

		}
		else
			_perso.Play("idle");
	}
		public override void Draw(GameTime gameTime)
		{
		_spriteBatch.Begin();
		_tiledMapRenderer.Draw();
		
		_spriteBatch.Draw(_perso, _positionPerso);
		_spriteBatch.End();
		// on utilise la reference vers
		// Game1 pour chnager le graphisme
	}
	}

