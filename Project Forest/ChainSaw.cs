using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project_Forest
{

    class ChainSaw : Damaging
    {
        float defaultRotation; //Default Rotation for swinging the chainsaw
        float rotation; //Rotation in Radians

        public float DefaultRotation
        {
            get { return defaultRotation; }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                if (value >= 6.283185f)
                {
                    rotation = value - 6.283185f;
                }
                else if (value < 0)
                {
                    rotation = value + 6.283185f;
                }
                else
                {
                    rotation = value;
                }
            }
        }

        public ChainSaw(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int pwr)
            : base(xpos, ypos, rect, text, direct, speed, pwr)
        {
            defaultRotation = 5.2359877f;
            rotation = defaultRotation;
        }

        public override void Move()
        {
            while (this.Active)
            {
                Y++;
            }
        }

        public override void doDamage(Enemy e)
        {
            e.HP -= Dmg;
        }
    }
}