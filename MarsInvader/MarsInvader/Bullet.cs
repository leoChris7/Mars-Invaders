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
        private int _shootingSpeed;
        private Texture2D _bulletTexture;
        private Vector2 _bulletPosition;
        private Vector2 _bulletDirection;
        private Vector2 _targetPosition;
        private Vector2 _playerPosition;

        public Bullet(Player Player, Target Target, int shootingSpeed)
        {
            this.ShootingSpeed = shootingSpeed;
            this.BulletPosition = new Vector2((int)Player.PositionPerso.X, (int)Player.PositionPerso.Y);
            this.TargetPosition = Target.PositionTarget;
            this.PlayerPosition = Player.PositionPerso;
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
