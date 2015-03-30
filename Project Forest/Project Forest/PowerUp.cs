using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    abstract class PowerUp : IEntity
    {
        int power;
        int x;
        int y;
        Rectangle coRect;

        int Power
        {
            get { return power; }
            set { power = value; }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public Rectangle CoRect
        {
            get
            {
                return coRect;
            }
            set
            {
                coRect = value;
            }
        }

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
