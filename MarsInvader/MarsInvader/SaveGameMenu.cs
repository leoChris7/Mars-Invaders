using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE101;

namespace MarsInvader
{
    public class SaveGameMenu
    {
        private ScreenGame _screenGame;

        public SaveGameMenu(ScreenGame myScreen1)
        {
            this.MyScreen1 = myScreen1;
        }

        public ScreenGame MyScreen1
        {
            get
            {
                return this._screenGame;
            }

            set
            {
                this._screenGame = value;
            }
        }
    }
}
