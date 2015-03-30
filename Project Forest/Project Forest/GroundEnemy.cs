using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    abstract class GroundEnemy : Enemy
    {
        public GroundEnemy(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range)
            :base(xpos,ypos,rect,text,direct,speed,health,range)
        {
           
        }
        public override abstract void Move(MainCharacter player);

        public override abstract void Attack(MainCharacter player);
    }
}
