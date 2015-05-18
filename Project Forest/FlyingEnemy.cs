using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class FlyingEnemy:RangedEnemy
    {
        public FlyingEnemy(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range, EnemyBullet bull)
            : base(xpos,ypos,rect,text,direct,speed,health,range,bull)
        {

        }

        public override void Move() { }

        public override void Move(MainCharacter player)
        {
            if (X > player.X)
            {
                X -= Speed;
            }
            if (X < player.X)
            {
                X += Speed;
            }
        }

        public override void Attack(MainCharacter player)
        {
            if (player.CoRect.Intersects(AtkRanRect))//Deteremine best way
            {
                Bullet = new EnemyBullet(X, Y, Bullet.CoRect, Bullet.Texture, 2, 8, 2);//Note decide what number direction is down
            }
        }
    }
}
