using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    abstract class Enemy : Movable
    {
        int hp;
        Rectangle atkRanRect;

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public Rectangle AtkRanRect
        {
            get { return atkRanRect; }
            set { atkRanRect = value; }
        }

        public Enemy(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range)
            :base(xpos,ypos,rect,text,direct,speed)
        {
            hp = health;
            atkRanRect = range;
        }
        public override abstract void Move();

        public abstract void Move(MainCharacter player);

        public abstract void Attack(MainCharacter player);
    }
}
