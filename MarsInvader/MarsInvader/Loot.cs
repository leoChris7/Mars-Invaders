using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SAE101;
using System;

namespace MarsInvader
{
    internal class Loot
    {
        private const int LOOTSIZE=16;

        private int lootType;
        private Vector2 lootPos;
        private Texture2D lootTexture;
        private Rectangle lootHitBox;

        public Loot(Vector2 lootPos, int lootType, Texture2D lootTexture1, Texture2D lootTexture2, Texture2D lootTexture3)
        {
            this.LootPos = lootPos;
            this.LootType = lootType;

            if(this.lootType==0)this.LootTexture = lootTexture1;            
            else if (this.lootType == 1) this.LootTexture = lootTexture2;
            else if (this.lootType == 2) this.LootTexture = lootTexture3;
            this.LootHitBox = new Rectangle((int)this.LootPos.X, (int)this.LootPos.Y, LOOTSIZE+6, LOOTSIZE+6 );
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

        public int LootType
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

        public Rectangle LootHitBox
        {
            get
            {
                return this.lootHitBox;
            }

            set
            {
                this.lootHitBox = value;
            }
        }

        
        public bool Recupere(Player joueur,ref bool bulletLootDrop,ref int exp, ref float ChronoDrop)
        /// Cette méthode permet de gérer les drops d'ennemis.
        {
            if (joueur.hitBox.Intersects(this.lootHitBox))
            {
                Console.WriteLine("loot");
                switch (this.LootType)
                {
                    case 0:
                        joueur.addHealth(20);
                        break;

                    case 1:
                        bulletLootDrop = true;
                        ChronoDrop = 0;
                        break;
                    case 2:
                        exp += 50;
                        break;
                }
                return true;
            }
            else 
                return false;
            }
    }

}
