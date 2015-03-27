using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Forest
{
    abstract class Damaging : Movable
    {
        int dmg;
        bool active;
        
        int Dmg
        {
            get { return Dmg; }
            set { Dmg = value; }
        }

        bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public abstract void Move();
    }
}
