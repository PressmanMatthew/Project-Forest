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
            if (enemies.Count > 0)
            {
                foreach (Enemy e in enemies)
                {
                    if (e.HP <= 0)
                    {
                        e.X = -100;
                        e.Y = -100;
                        enemies.Remove(e);
                    }
                }
            }
        }
    }
}
