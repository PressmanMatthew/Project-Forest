using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    abstract class GroundEnemy : Enemy
    {
        public override abstract void Move();

        public override abstract void Attack();
    }
}
