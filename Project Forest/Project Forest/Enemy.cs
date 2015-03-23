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
        Texture2D texture;
        Rectangle agroRect;

        int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        Rectangle AgroRect
        {
            get { return AgroRect; }
            set { AgroRect = value; }
        }
        public abstract void Move()
        {

        }
        public abstract void Attack()
        {

        }
    }
}
