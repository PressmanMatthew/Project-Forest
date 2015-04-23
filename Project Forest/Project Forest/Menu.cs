using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Forest
{
    abstract class Menu
    {
        //texture fields
        private Texture2D image;
        //array
        protected List<MenuOption> menuOptions;

        protected ArrowSelection currentMenuSelection;

        //properties
        public Texture2D getsetImage
        {
            get { return image; }
            set { image = value; }
        }

        public List<MenuOption> MenuOptions
        {
            get { return menuOptions; }
        }

        public ArrowSelection CurrentMenuSelection
        {
            get { return currentMenuSelection; }
            set { currentMenuSelection = value; }
        }

        //constructor
        public Menu(Texture2D image, List<MenuOption> menuOptions)
        {
            this.image = image;
            this.menuOptions = menuOptions;
        }
        
        
    }
}
