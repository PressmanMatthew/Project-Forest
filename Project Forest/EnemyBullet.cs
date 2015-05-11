using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class EnemyBullet:Damaging
    {

        public EnemyBullet(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int pwr)
            : base(xpos, ypos, rect, text, direct, speed, pwr)
        {
            Active = true;
        }
        public override void Move()
        {
            if(Direction == 0)
            {
                X += Speed;
            }
            if(Direction == 1)
            {
                X -= Speed;
            }
        }

        public override void doDamage(Enemy e){}            

        public void doDamage(MainCharacter player)
        {
            if(CoRect.Intersects(player.CoRect) && Active == true)
            {
                player.HP -= Dmg;
                Active = false;
            }
        }
    }
}
