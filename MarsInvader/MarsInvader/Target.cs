using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsInvader
{
    public class Target
    {
        private Vector2 _positionTarget;
        private MouseState _mouseState;
        private Texture2D _targetTexture;

        public const int SIZETARGET = 50;

        public Target(Texture2D targetTexture)
        {
            this.PositionTarget = new Vector2(0, 0);
            _mouseState = new MouseState();
            this.TargetTexture = targetTexture;
        }
        public void PlaceTarget()
        {
            _mouseState = Mouse.GetState();
            Point _mousePosition = _mouseState.Position;
            this.PositionTarget = new Vector2(_mousePosition.X - SIZETARGET / 2, _mousePosition.Y - SIZETARGET / 2);
        }


        public Vector2 PositionTarget
        {
            get
            {
                return this._positionTarget;
            }

            set
            {
                this._positionTarget = value;
            }
        }

        public MouseState MouseState
        {
            get
            {
                return this._mouseState;
            }

            set
            {
                this._mouseState = value;
            }
        }


        public Texture2D TargetTexture
        {
            get
            {
                return this._targetTexture;
            }

            set
            {
                this._targetTexture = value;
            }
        }
    }



}
