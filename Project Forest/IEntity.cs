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
        //removed uneeded content
        int X{ get; set;}

        int Y { get; set;}

        Rectangle CoRect{ get; set;}
        bool IsColliding(IEntity entity);
    }
}
