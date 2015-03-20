using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    abstract class Movable : IEntity
    {
        int direction;
        int speed;

        int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public abstract void Move()
        {
        
        }
    }
}
