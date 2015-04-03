using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    abstract class Damaging : Movable
    {
        int dmg;
        bool active;
        
        public int Dmg
        {
            get { return Dmg; }
            set { Dmg = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Damaging(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int fast, int pwr)
            : base(xpos, ypos, rect, text, direct, fast)
        {
            active = false;
            dmg = pwr;
        }

        public override abstract void Move();

        public abstract void doDamage(Enemy e);

    }
}
