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
        Texture2D texture;

        public  int HP {get{return hp;} set{hp = value;}}
        public ChainSaw Chainsaw {get{return chainsaw;} set{chainsaw = value;}}

        public Texture2D Texture {get{return texture;} set{texture = value;}}

        public MainCharacter(int xpos, int ypos, Rectangle rect, int direct, int fast, int health, ChainSaw melee, Texture2D look):
            base(xpos,ypos,rect,direct,fast)//Constructor base from movable
        {
            hp = health;
            //fuel = gas;  for later use
            //ammo = munition;
            chainsaw = melee;
            texture = look;
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
        public void Attack()
        {

        }

        public void Jump()
        {

        }
        //Burn()  for later use
        //Shoot()

        
    }
}