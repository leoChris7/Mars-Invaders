using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using SAE101;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Tiled;

namespace SAE101
{
    internal class Player
    {
        private int health;
        private int attack;
        private int points;
        private int speed;
        private String pseudo;
        private AnimatedSprite _perso;
        private Texture2D _textureBalle;
        private Vector2 _positionPerso;
        private KeyboardState _keyboardState;
        public TiledMap _tiledMap;
        private SpriteBatch _spriteBatch { get; set; }
        private MouseState _mouseState;


        public Player(string pseudo,TiledMap _tiledMap, TiledMapTileLayer mapLayer, SpriteSheet spriteSheet)
        {
            this.Pseudo = pseudo;

            this.Health = 100;
            this.Attack = 1;
            this.Speed = 100;
            this.Points = 0;
            this._tiledMap = _tiledMap;
            this.Perso = new AnimatedSprite(spriteSheet);
            this._positionPerso = new Vector2(Game1._WINDOWSIZE / 2, Game1._WINDOWSIZE / 2);
            this.mapLayer = mapLayer;
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                // si la vie est bien supérieure à 0, inférieure ou égale à 100 et n'est pas vide / null
                if ( value > 0 && value <= 100 && !String.IsNullOrEmpty(value.ToString()) )
                    this.health = value;
                else
                    throw new ArgumentOutOfRangeException("La vie a une erreur de valeur, est soit inférieure ou égale à 0 ou supérieure à 100 ou vide / null.");
            }
        }
       

        public int Attack
        {
            get
            {
                return this.attack;
            }

            set
            {
                if ( value > 0 && !String.IsNullOrEmpty(value.ToString()) )
                    this.attack = value;
                else
                    throw new ArgumentOutOfRangeException("L'attaque a une erreur de valeur, est soit inférieure ou égale à 0, soit vide.");
            }
        }

        public int Points
        {
            get
            {
                return this.points;
            }

            set
            {
                if ( value >= 0 && !String.IsNullOrEmpty(value.ToString()) ) 
                    this.points = value;
                else
                    throw new ArgumentOutOfRangeException("Les points ont une erreur de valeur, soit inférieur strictement à 0, soit vide.");
            }
        }

        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                if ( value >= 0 && !String.IsNullOrEmpty(value.ToString()) )
                    this.speed = value;
                else
                    throw new ArgumentNullException("La vitesse a une erreur de valeur, soit inférieure ou égale à 0, soit vide.");
            }
        }

        public string Pseudo
        {
            get
            {
                return this.pseudo;
            }

            set
            {
                if (!String.IsNullOrEmpty(value))
                    this.pseudo = value.ToUpper();
                else
                    throw new ArgumentNullException("Le pseudo a une erreur de valeur, est nul ou vide.");
            }
        }

        public Vector2 PositionPerso
        {
            get
            {
                return this._positionPerso;
            }

            set
            {
                this._positionPerso = value;
            }
        }



        public void ShootingBullets(int bulletSpeed)
        {
           _mouseState = Mouse.GetState();
           Point _mousePosition = _mouseState.Position;
        }


        public AnimatedSprite Perso
        {
            get
            {
                return this._perso;
            }

            set
            {
                this._perso = value;
            }
        }

        public void addHealth(int additionalHealth)
        // Cette méthode permet d'ajouter de la vie, mais surtout de vérifier que la vie ne dépasse pas le nombre de 100
        // Si la vie dépasse 100, elle est remise à 100
        {
            if (this.Health + additionalHealth > 100)
                this.Health = 100;
            else
                this.Health += additionalHealth;
        }

        public int removeHealth(int damage)
        {
            if (this.Health - damage <= 0)
            { 
                this.Health = 0;
                return 0;
            }
            // configurer la fin du jeu car si le joueur a une vie inférieure à 0, il meurt. 
            return 1;
            // le joueur n'a pas une vie inférieure à 0, le jeu continue. 
        }
        /*public void CreationPerso(SpriteSheet spriteSheet)
        {
           _perso = new AnimatedSprite(spriteSheet);
        }*/
        public void Deplacer(GameTime gameTime)

        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            Perso.Update(deltaTime);

            float walkSpeed = deltaTime * this.Speed; // Vitesse de déplacement du sprite
            String animation = "idle";
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(this._positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(this._positionPerso.Y / _tiledMap.TileHeight - 1);
                animation = "walkNorth";
                if (!IsCollision(tx, ty))
                    this._positionPerso.Y -= walkSpeed;
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(this._positionPerso.X / this._tiledMap.TileWidth);
                ushort ty = (ushort)(this._positionPerso.Y / this._tiledMap.TileHeight + 1);
                animation = "walkSouth";
                if (!IsCollision(tx, ty))
                    this._positionPerso.Y += walkSpeed;
            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(this._positionPerso.X / this._tiledMap.TileWidth - 1);
                ushort ty = (ushort)(this._positionPerso.Y / this._tiledMap.TileHeight);
                animation = "walkWest";
                if (!IsCollision(tx, ty))
                    this._positionPerso.X -= walkSpeed;
            }
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(this._positionPerso.X / this._tiledMap.TileWidth + 1);
                ushort ty = (ushort)(this._positionPerso.Y / this._tiledMap.TileHeight);
                animation = "walkEast";
                if (!IsCollision(tx, ty))
                    this._positionPerso.X += walkSpeed;
            }
            Perso.Play(animation);

        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (this.mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

    }
}
