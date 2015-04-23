using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Forest
{
    class MenuOption
    {
        string name;
        Rectangle position;

        public Rectangle Position
        {
            get { return position; }
        }

        public string Name
        {
            get { return name; }
        }

        public MenuOption(string name, int x, int y)
        {
            this.name = name;
            position = new Rectangle(x, y, 100, 50);
        }
    }
}
