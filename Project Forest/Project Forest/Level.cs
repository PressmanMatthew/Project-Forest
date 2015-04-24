using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Forest
{
    class Level
    {
        Queue<FightScene> enemySceneQueue;
        Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
        }

        public Level()
        {
            rect = new Rectangle(0, 0, 0, 480);
            enemySceneQueue = new Queue<FightScene>();
        }
        public void AddFightScene(int diff)
        {
            FightScene f = new FightScene();

            for (int i = 0; i < diff/2; i++)
            {
                //use how big diff is to scale the enemies difficulties
                //Ent e = new Ent(0, 0, );
                //f.AddEnemy(e);
            }

            enemySceneQueue.Enqueue(f);
            rect.Width = rect.Width + 1200;
        }

        public FightScene Encounter()
        {
            return enemySceneQueue.Dequeue();
        }
    }
}
