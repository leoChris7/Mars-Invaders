using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsInvader
{
    internal class Loot
    {
        private Vector2 lootPos;
        private string lootType;
        private Texture2D lootTexture;

        public Loot(Vector2 lootPos, string lootType, Texture2D lootTexture)
        {
            this.LootPos = lootPos;
            this.LootType = lootType;
            this.LootTexture = lootTexture;
        }

        public Vector2 LootPos
        {
            get
            {
                return this.lootPos;
            }

            set
            {
                this.lootPos = value;
            }
        }

        public string LootType
        {
            get
            {
                return this.lootType;
            }

            set
            {
                this.lootType = value;
            }
        }

        public Texture2D LootTexture
        {
            get
            {
                return this.lootTexture;
            }

            set
            {
                this.lootTexture = value;
            }
        }
    }
}
