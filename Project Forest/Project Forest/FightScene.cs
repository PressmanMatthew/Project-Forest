using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Forest
{
    class FightScene
    {
        List<Enemy> enemies;

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        public FightScene()
        { 
            enemies = new List<Enemy>();
        }

        public void AddEnemy(Enemy e)
        {
            enemies.Add(e);
        }
        public void UpdateList()
        {
            foreach (Enemy e in enemies)
            {
                if (e.HP == 0)
                {
                    enemies.Remove(e);
                }
            }
        }
    }
}
