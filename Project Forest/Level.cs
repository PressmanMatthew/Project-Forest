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
        Texture2D entTexture;
        Rectangle entRect;
        Rectangle entAttackRange;
        int entX;
        int entY;

        public Rectangle Rect
        {
            get { return rect; }
        }

        public int CurrentFightSceneX
        {
            get
            {
                return rect.Width - (enemySceneQueue.Count * 800) - ((enemySceneQueue.Count - 1) * 400);
            }
        }

        public Level(Ent ent)
        {
            rect = new Rectangle(0, 0, 0, 480);
            enemySceneQueue = new Queue<FightScene>();

            entTexture = ent.Texture;
            entRect = ent.CoRect;
            entAttackRange = ent.AtkRanRect;
            entX = ent.X;
            entY = ent.Y;

            AddFightScene(2);
            AddFightScene(4);
            AddFightScene(6);
            AddFightScene(8);
        }
        public void AddFightScene(int difficulty)
        {
            FightScene f = new FightScene();

            for (int i = 0; i < difficulty/2; i++)
            {
                //use how big diff is to scale the enemies difficulties
                Ent e = new Ent(entX - (25 * i), entY, entRect, entTexture, 1, 5, 100, entAttackRange);
                f.AddEnemy(e);
            }

            enemySceneQueue.Enqueue(f);
            if (enemySceneQueue.Count == 0)
            {
                rect.Width += 800;
            }
            else
            {
                rect.Width += 1200;
            }
        }

        public FightScene Encounter()
        {
            return enemySceneQueue.Dequeue();
        }
    }
}
