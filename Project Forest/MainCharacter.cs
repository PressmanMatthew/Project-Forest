using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class MainCharacter : Movable
    {
        int hp;
        //int fuel; for later use
        //int ammo;
        ChainSaw chainsaw;
        Flame fire;

        public  int HP {get{return hp;} set{hp = value;}}
        public ChainSaw Chainsaw {get{return chainsaw;} set{chainsaw = value;}}
        public Flame Fire { get { return fire; } set { fire = value; } }

        public MainCharacter(int xpos, int ypos, Rectangle rect, Texture2D texture, int direct, int speed, int health, ChainSaw melee, Flame range):
            base(xpos,ypos,rect, texture,direct,speed)//Constructor base from movable
        {
            hp = health;
            //fuel = gas;  for later use
            //ammo = munition;
            chainsaw = melee;
            fire = range;
        }

        public override void Move()
        {
            if(Direction == 0)
            {
                X -= Speed;
            }
            if(Direction == 1)
            {
                X += Speed;
            }
        }

        public override bool IsColliding(IEntity entity)
        {
            base.IsColliding(entity);
            return false;
        }
        public void Attack(Enemy enemy)
        {
            enemy.HP -= 20;
        }

        public void Jump()
        {

        }
        //Burn()  for later use
        //Shoot()

        
    }
}