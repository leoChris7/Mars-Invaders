using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SAE101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsInvader
{
    public class Bullet
    {
        public const int BULLETSIZE = 25;
        public const double PADDING = 0.9;

        private int _shootingSpeed;
        private Texture2D _bulletTexture;
        private Vector2 _bulletPosition;
        private Vector2 _targetPosition;
        private Vector2 _playerPosition;
        public Rectangle _hitBox;

        public Bullet(Player Player, Target Target, int shootingSpeed)
        {
            this.ShootingSpeed = shootingSpeed;
            this.BulletPosition = new Vector2((int)Player.PositionPerso.X, (int)Player.PositionPerso.Y);
            this.TargetPosition = Target.PositionTarget;
            this.PlayerPosition = Player.PositionPerso;
            this._hitBox = new Rectangle((int)Player.PositionPerso.X, (int)Player.PositionPerso.Y, (int)(BULLETSIZE * PADDING), (int)(BULLETSIZE * PADDING));
        }

        public int ShootingSpeed
        {
            get
            {
                return this._shootingSpeed;
            }

            set
            {
                this._shootingSpeed = value;
            }
        }

        public Texture2D BulletTexture
        {
            get
            {
                return this._bulletTexture;
            }

            set
            {
                this._bulletTexture = value;
            }
        }

        public Vector2 BulletPosition
        {
            get
            {
                return this._bulletPosition;
            }

            set
            {
                this._bulletPosition = value;
                this._hitBox = new Rectangle((int)value.X, (int)value.Y, BULLETSIZE, BULLETSIZE);
            }
        }

        public Vector2 TargetPosition
        {
            get
            {
                return this._targetPosition;
            }

            set
            {
                this._targetPosition = value;
            }
        }

        public Vector2 PlayerPosition
        {
            get
            {
                return this._playerPosition;
            }

            set
            {
                this._playerPosition = value;
            }
        }

        public void DirigerVers()
        {
            
        }

    }
}
