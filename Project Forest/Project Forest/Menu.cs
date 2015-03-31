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
        protected List<Keys> keys;

        //properties
        public Texture2D getsetImage
        {
            get { return image; }
            set { image = value; }
        }

        public List<Keys> getKeys
        {
            get { return keys; }
        }

        //constructor
        public Menu(Texture2D image, List<Keys> keys)
        {
            this.image = image;
            this.keys = keys;
        }
        
        
    }
}
