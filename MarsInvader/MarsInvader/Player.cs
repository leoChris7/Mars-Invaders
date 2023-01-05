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
        private Vector2 _positionPerso;
        private KeyboardState _keyboardState;
        private SpriteBatch _spriteBatch { get; set; }

        public Player(string pseudo)
        {
            this.Pseudo = pseudo;
            this.Health = 100;
            this.Attack = 1;
            this.Speed = 100;
            this.Points = 0;
            this._positionPerso = new Vector2(Game1._WINDOWSIZE / 2, Game1._WINDOWSIZE / 2);
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
                if ( this.speed >= 0 && !String.IsNullOrEmpty(value.ToString()) )
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
        public void CreationPerso()
        {
            
        }
        public void Deplacer(GameTime gameTime, out Vector2 _positionPerso)

        {
            _positionPerso = new Vector2(20, 340);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            _perso.Update(deltaTime);

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _positionPerso.X += this.Speed * deltaTime;
                _perso.Play("walkEast");
            }

            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _positionPerso.X -= this.Speed * deltaTime;
                _perso.Play("walkWest");
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _positionPerso.Y += this.Speed * deltaTime;
                _perso.Play("walkSouth");
            }

            else if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _positionPerso.Y -= this.Speed * deltaTime;
                _perso.Play("walkNorth");

            }
            else
                _perso.Play("idle");

        }
        
    }
}
