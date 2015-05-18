using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class RangedEnemy:Enemy
    {
        private EnemyBullet bullet;

        public EnemyBullet Bullet { get { return bullet; } set { bullet = value; } }
        public RangedEnemy(int xpos, int ypos, Rectangle rect, Texture2D text, int direct, int speed, int health, Rectangle range, EnemyBullet bull)
            :base(xpos,ypos,rect,text,direct,speed,health,range)
        {
            bullet = bull;
        }

        public override void Move() { }

        public override void Move(MainCharacter player)
        {
            if (X > player.X && player.CoRect.Intersects(AtkRanRect) != true)
            {
                X -= Speed;
            }
            if (X < player.X && player.CoRect.Intersects(AtkRanRect) != true)
            {
                X += Speed;
            }
        }

        public override void Attack(MainCharacter player)
        {
            if (player.CoRect.Intersects(AtkRanRect))
            {
                bullet = new EnemyBullet(X, Y, bullet.CoRect, bullet.Texture, Direction, 8, 2);
            }
        }
    }
}
