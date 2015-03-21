using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    interface IEntity
    {
        int x;
        int y;
        Rectangle coRect;

        int X
        {
            get { return x; }
            set { x = value;
                  coRect.X = x; }
        }

        int Y
        {
            get { return y; }
            set { y = value;
                  coRect.Y = y; }
        }

        Rectangle CoRect
        {
            get { return coRect; }
            set { coRect = value; }
        }
        public bool IsColliding(IEntity entity)
        {
            if(coRect.Intersects(entity.CoRect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
