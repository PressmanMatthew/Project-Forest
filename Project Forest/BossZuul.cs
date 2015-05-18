using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;

namespace Project_Forest
{
    class BossZuul 
    {
        int x;
        int y;

        int X 
        {
            get { return x; }
            set { x = value; }
        }

        int Y
        {
            get { return y; }
            set { y = value; }
        }

        public BossZuul(int ix, int iy)
        {
            x = ix;
            y = iy;
            g = new GameTime();
        }

        public bool DuelZuul()
        {

        }
    }
}
