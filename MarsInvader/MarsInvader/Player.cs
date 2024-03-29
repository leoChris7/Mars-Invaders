﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MarsInvader;

namespace SAE101
{
    internal class Coeur
    {
        private int numCoeur;
        private Texture2D vieTexture;
        private Vector2 positionCoeur;
        public Coeur(int numCoeur , int coeur,Texture2D _coeurVide)
        {
            this.NumCoeur = numCoeur;
            
            this.PositionCoeur = new Vector2(Game1._WINDOWSIZE + 20 + (coeur * 34), 50);
            this.VieTexture = _coeurVide;
        }

        public int NumCoeur
        {
            get
            {
                return this.numCoeur;
            }

            set
            {
                this.numCoeur = value;
            }
        }
        public Texture2D VieTexture
        {
            get
            {
                return this.vieTexture;
            }

            set
            {
                this.vieTexture = value;
            }
        }

        public Vector2 PositionCoeur
        {
            get
            {
                return this.positionCoeur;
            }

            set
            {
                this.positionCoeur = value;
            }
        }

        public Texture2D VieCalcul(int coeur, Player Joueur, Texture2D _coeurFull, Texture2D _coeurHigh, Texture2D _coeurHalf, Texture2D _coeurLow, Texture2D _coeurVide)
        {


            if ((coeur * 20 + 19) < Joueur.Health)
            {
                vieTexture = _coeurFull;
            }
            else if ((coeur * 20 + 14) < Joueur.Health)
            {
                vieTexture = _coeurHigh;
            }
            else if ((coeur * 20 + 9) < Joueur.Health)
            {
                vieTexture = _coeurHalf;
            }
            else if ((coeur * 20 + 4) < Joueur.Health)
            {
                vieTexture = _coeurLow;
            }
            else
                vieTexture = _coeurVide;
            return vieTexture ;
        }
        
    }

    public class Player
    {
        public const int PLAYERSIZE = 32;
        public const int MAXPLAYERHEALTH = 100;

        private int health;
        private int attack;
        private int points;
        private int speed;
        private int niveau;
        private String pseudo;

        private AnimatedSprite _perso;
        public Rectangle hitBox;

        private Vector2 _positionPerso;
        private TiledMapTileLayer mapLayer;
        private KeyboardState _keyboardState;
        public TiledMap _tiledMap;
        private MouseState _mouseState;
        private Game1 _myGame;

        public Player(Game1 game) 
        {
            // INITIALIZE
            _myGame = game;
        }
        public Player(string pseudo, TiledMap _tiledMap, TiledMapTileLayer mapLayer, SpriteSheet spriteSheet)
        {
            this.Pseudo = pseudo;
            this.Health = Player.MAXPLAYERHEALTH;
            this.Attack = 1;
            this.Speed = 100;
            this.Points = 0;
            this._tiledMap = _tiledMap;
            this.Niveau = 0;
            this.Perso = new AnimatedSprite(spriteSheet);
            this._positionPerso = new Vector2(Game1._WINDOWSIZE / 2, Game1._WINDOWSIZE / 2);
            this.hitBox = new Rectangle(Game1._WINDOWSIZE / 2, Game1._WINDOWSIZE / 2, Player.PLAYERSIZE, Player.PLAYERSIZE);
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
                if (!String.IsNullOrEmpty(value.ToString()))
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
                if (value > 0 && !String.IsNullOrEmpty(value.ToString()))
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
                if (value >= 0 && !String.IsNullOrEmpty(value.ToString()))
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
                if (value >= 0 && !String.IsNullOrEmpty(value.ToString()))
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

        public int Niveau
        {
            get
            {
                return this.niveau;
            }

            set
            {
                this.niveau = value;
            }
        }

        public void addHealth(int additionalHealth)
        /// Cette méthode permet d'ajouter de la vie, mais surtout de vérifier que la vie ne dépasse pas le nombre de 100
        /// Si la vie dépasse 100, elle est remise à 100
        {
            if (this.Health + additionalHealth > Player.MAXPLAYERHEALTH)
                this.Health = Player.MAXPLAYERHEALTH;
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
            this.Health -= damage;
            return 1;
            // le joueur n'a pas une vie inférieure à 0, le jeu continue. 
        }
        
        public int NiveauCalcul(ref int Exp, ref int ExpLvlUp,  int niveau, ref double fireSpeed)
        {
            
            if (Exp>=ExpLvlUp)
            {
                Exp -= ExpLvlUp;
                niveau++;
                ExpLvlUp=(int)Math.Round(ExpLvlUp * 1.5,0);
                LevelUp(ref  fireSpeed);
                
            }
            return niveau;
        }

        public void LevelUp(ref double fireSpeed)
        /// Augmenter le niveau du joueur
        {
            if (this.Niveau > 0 && niveau <=10)
            {
                fireSpeed -= 0.1;
                this.Attack += 2;
                this.Speed += 2;
                this.Health += 20;
                if (this.Health > 100)
                    this.Health = 100;
            }
             else if (niveau>10)
            {
                fireSpeed -= 0.01;
                this.Health = 100;
            }     
        }

        public void Deplacer(GameTime gameTime)
        /// Cette méthode gère les déplacements et l'animation du joueur
        /// Elle ne retourne rien mais modifie les champs de position du personnage

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
            this.hitBox = new Rectangle((int)PositionPerso.X, (int)PositionPerso.Y, Player.PLAYERSIZE, Player.PLAYERSIZE);

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
