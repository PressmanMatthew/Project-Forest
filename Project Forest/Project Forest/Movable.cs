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
        Texture2D texture;
        int direction;
        int speed;
        protected CharacterStates state;

        public int X
        {
            get { return x; }
            set { x = value; coRect.X = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; coRect.Y = value; }
        }

        public Rectangle CoRect
        {
            get { return coRect; }
            set { coRect = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
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

        public CharacterStates State
        {
            get { return state; }
            set { state = value; }
        }

        public Movable(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed)//Added constructor so that it can be base called in children
        {
            x = xpos;
            y = ypos;
            coRect = rect;
            texture = text;
            direction = direct;
            this.speed = speed;
            state = CharacterStates.FaceRight;
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
