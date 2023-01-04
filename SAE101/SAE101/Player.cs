using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SAE101;

namespace SAE101
{
    internal class Player
    {
        private int health;
        private int attack;
        private int points;
        private int speed;
        private String pseudo;

        private Texture2D _texturePlayer;

        public Player(string pseudo)
        {
            this.Pseudo = pseudo;

            this.Health = 100;
            this.Attack = 1;
            this.Speed = 1;
            this.Points = 0;
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
                    throw new ArgumentOutOfRangeException();
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
                    throw new ArgumentOutOfRangeException();
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
                    throw new ArgumentOutOfRangeException("Les points du joueur sont soit inférieur à 0, soit vide. ");
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
                if ( this.speed != 0 && !String.IsNullOrEmpty(value.ToString()) )
                    this.speed = value;
                else
                    throw new ArgumentNullException("La vitesse du joueur est soit égale à 0, soit vide ou nulle.");
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
                    throw new ArgumentNullException("Le pseudo est nul ou vide.");
            }
        }
    }
}
