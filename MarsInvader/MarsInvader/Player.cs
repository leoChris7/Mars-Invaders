using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
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

        public void addHealth(int additionalHealth)
        // Cette méthode permet d'ajouter de la vie, mais surtout de vérifier que la vie ne dépasse pas le nombre de 100
        // Si la vie dépasse 100, elle est remise à 100
        {
            if (this.Health + additionalHealth > 100)
                this.Health = 100;
            
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
    }
}
