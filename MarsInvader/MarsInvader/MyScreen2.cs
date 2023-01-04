using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using SAE101;

public class MyScreen2 : GameScreen
	{
		private SpriteFont _police;
		private Game1 _myGame;
	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1
		public SpriteBatch SpriteBatch { get; set; }
		public MyScreen2(Game1 game) : base(game)
		{
			_myGame = game;
		}
		public override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			_police = Content.Load<SpriteFont>("fontPauseMenu");
			base.LoadContent();
		}
		public override void Update(GameTime gameTime)
		{ }
		public override void Draw(GameTime gameTime)
		{

		_myGame.GraphicsDevice.Clear(Color.Blue); // on utilise la reference vers
												  // Game1 pour chnager le graphisme
			SpriteBatch.Begin();
			SpriteBatch.DrawString(_police, "MENU", new Vector2((float)Game1._WINDOWSIZE/2, (float)Game1._WINDOWSIZE/2), Color.White);
			SpriteBatch.End();

		}
	}

