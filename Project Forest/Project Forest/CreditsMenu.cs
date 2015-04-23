using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Forest
{
    class CreditsMenu:Menu
    {
        public CreditsMenu(Texture2D image, List<MenuOption> menuOptions)
            : base(image, menuOptions)
        {

        }
    }
}
