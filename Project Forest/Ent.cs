using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class Ent : GroundEnemy
    {
        public Ent(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range)
            :base(xpos,ypos,rect,text,direct,speed,health,range)
        {

        }

        public override void Move() { }

        public override void Move(MainCharacter player)
        {
            if(X > player.X)
            {
                X -= Speed;
                Direction = 0;
            }
            if(X < player.X)
            {
                X += Speed;
                Direction = 1;
            }
        }

        public override void Attack(MainCharacter player)
        {
            player.HP -= 5;
        }
    }
}
