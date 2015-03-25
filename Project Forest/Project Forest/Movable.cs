using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    abstract class Movable : IEntity
    {
        //properlly implemented IEntity Interface
        int x;
        int y;
        Rectangle coRect;
        int direction;
        int speed;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Rectangle CoRect
        {
            get { return coRect; }
            set { coRect = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Movable(int xpos, int ypos, Rectangle rect, int direct, int fast)//Added constructor so that it can be base called in children
        {
            x = xpos;
            y = ypos;
            coRect = rect;
            direction = direct;
            speed = fast;
        }

        public abstract void Move();

        public virtual bool IsColliding(IEntity entity)
        {
            if (coRect.Intersects(entity.CoRect))
            {
                return true;
            }

            return false;
        }
    }
}
