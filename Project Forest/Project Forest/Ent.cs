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
        public static Texture2D staticTexture;
        public static int direction;
        public static Rectangle boundsRect;
        public static Rectangle rangeRect;
        public static int xPosition;
        public static int yPosition;

        public Ent(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range)
            :base(xpos,ypos,rect,text,direct,speed,health,range)
        {

        }

        public Ent(int speed, int health)
            :base(xPosition, yPosition, boundsRect, staticTexture, direction, speed, health, rangeRect)
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
            player.HP -= 20;
        }
    }
}
