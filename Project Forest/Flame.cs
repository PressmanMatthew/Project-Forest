using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Project_Forest
{
    class Flame : Damaging
    {
        bool visible;

        bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public Flame(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int fast, int pwr)
            : base(xpos, ypos, rect, text, direct, fast, pwr)
        {
            Active = false;
            Dmg = pwr;
        
        }

        public override void Move()
        {
            
        }

        public void doDamage()
        { 
            
        }
    }
}
