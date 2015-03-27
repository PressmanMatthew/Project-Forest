using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    class Ent : GroundEnemy
    {
        public override void Move(MainCharacter player)
        {
            if(X > player.X)
            {
                X--;
            }
            if(X < player.X)
            {
                X++;
            }
        }

        public override void Attack(MainCharacter player)
        {
            player.HP -= 2;
        }
    }
}
