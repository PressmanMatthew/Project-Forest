using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class ChainSaw : Damaging
    {
        public ChainSaw(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int pwr)
            : base(xpos, ypos, rect, text, direct, speed, pwr)
        {
        }

        public override void Move()
        {
            while(this.Active)
            {
                Y++;
            }
        }
    
        public override void doDamage(Enemy e)
        {
            e.HP -= Dmg;
        }
    }
}
