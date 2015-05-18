using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class SummaryMenu:Menu
    {
        public SummaryMenu(Texture2D image, List<MenuOption> menuOptions)
            : base(image, menuOptions)
        {

        }
    }
}
