using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using SAE101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsInvader
{
    internal class Alien
    {
        private int health;
        private AnimatedSprite alienTexture;
        private Vector2 positionAlien;
        private int attack;
        private int speed;
        private TiledMap tiledMap;
        private int niveau;
        public Rectangle hitBox;

        public Alien( int Niveau,TiledMap _tiledMap, SpriteSheet spriteSheet)
        {
            Random rnd = new Random();
            this.Health = 100;
            this.Attack = 1;
            this.Speed = 50;
            this.TiledMap = _tiledMap;
            this.AlienTexture = new AnimatedSprite(spriteSheet);

            int randomPos = rnd.Next(50, Game1._WINDOWSIZE - 50);

            this.PositionAlien = new Vector2(randomPos, randomPos); 
            this.hitBox = new Rectangle(randomPos, randomPos, 24, 24);
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value <= 100 && !String.IsNullOrEmpty(value.ToString()))
                    this.health = value;
                else
                    throw new ArgumentOutOfRangeException("La vie a une erreur de valeur, est soit inférieure ou égale à 0 ou supérieure à 100 ou vide / null.");
            }
        }

        public AnimatedSprite AlienTexture
        {
            get
            {
                return this.alienTexture;
            }

            set
            {
                this.alienTexture = value;
            }
        }

        public Vector2 PositionAlien
        {
            get
            {
                return this.positionAlien;
            }

            set
            {
                this.positionAlien = value;
                this.hitBox = new Rectangle((int)value.X, (int)value.Y, 24, 24);
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

        public TiledMap TiledMap
        {
            get
            {
                return this.tiledMap;
            }

            set
            {
                this.tiledMap = value;
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
                if (value > 0 &&value<5 && !String.IsNullOrEmpty(value.ToString()))
                    this.speed = value;
                else
                    throw new ArgumentNullException("La vitesse a une erreur de valeur, soit inférieure ou égale à 0, soit vide.");

            }
        }

        public void directionAlien(GameTime gameTime, Vector2 Position )
        {
            String animation = "idle"; 

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            alienTexture.Update(deltaTime);

            float walkSpeed = deltaTime * this.Speed; // Vitesse de déplacement du sprite

            if (this.PositionAlien.X - Position.X >= 0)//alien est a droite
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if (this.PositionAlien.X- Position.X  > this.PositionAlien.Y- Position.Y   )//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                        animation = "walkWest";
                    }
                    else
                    {
                        this.positionAlien.Y -= walkSpeed;
                        animation = "walkNorth";
                    }
                }
                else//alien est au dessus a droite
                {
                    if (this.PositionAlien.X - Position.X >  Position.Y - this.PositionAlien.Y)//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                        animation = "walkWest";
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                        animation = "walkSouth";
                    }
                }
            }
            else//alien est a gauche
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if  ( Position.X- this.PositionAlien.X  >  this.PositionAlien.Y - Position.Y)//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                        animation = "walkEast";
                    }
                    else
                    {
                        this.positionAlien.Y -= walkSpeed;
                        animation = "walkNorth";
                    }
                }
                else//alien est au dessus a droite
                {
                    if (Position.X - this.PositionAlien.X > Position.Y - this.PositionAlien.Y )//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                        animation = "walkEast";
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                        animation = "walkSouth";
                    }
                }
                
            }
            alienTexture.Play(animation);


        }
    }


    
}
