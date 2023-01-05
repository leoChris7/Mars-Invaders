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

        public Alien( int Niveau,TiledMap _tiledMap, SpriteSheet spriteSheet)
        {
            Random rnd = new Random();
            this.Health = 100;
            this.Attack = 1;
            this.Speed = 100;
            this.TiledMap = _tiledMap;
            this.AlienTexture = new AnimatedSprite(spriteSheet);
            this.PositionAlien = new Vector2(rnd.Next(50, Game1._WINDOWSIZE -50), rnd.Next(50, Game1._WINDOWSIZE - 50));

        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value > 0 && value <= 100 && !String.IsNullOrEmpty(value.ToString()))
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
    }


    
}
