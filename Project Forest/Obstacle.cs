using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    abstract class Obstacle : IEntity
    {

        public int X
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Y
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Microsoft.Xna.Framework.Rectangle CoRect
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsColliding(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
