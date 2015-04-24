using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Forest
{
    class View
    {
        List<IEntity> entities; //List of entities to draw to the screen
        ViewStates state; //The current state of the view
        Rectangle currentLevelView;

        //Accessor and mutator for state
        public ViewStates State
        {
            get { return state; }
            set { state = value; }
        }

        public Rectangle CurrentLevelView
        {
            get { return currentLevelView; }
            set { currentLevelView = value; }
        }

        public int X
        {
            get { return currentLevelView.X; }
            set { currentLevelView.X = value; }
        }

        public int Y
        {
            get { return currentLevelView.Y; }
            set { currentLevelView.Y = value; }
        }

        //Constructor that makes objects of the fields
        public View()
        {
            entities = new List<IEntity>();
            state = ViewStates.Stationary;
        }

        /// <summary>
        /// Method for handling the drawing of everything currently on the view
        /// </summary>
        /// <param name="sb">Spritebatch for drawing to the screen</param>
        /// <param name="list">The list of entities to be drawn</param>

        public void DrawEntities(SpriteBatch sb, List<IEntity> list)
 
        {
            entities = list; //Assign the passed in list to the local list

            //Draw all of the entities
            foreach (IEntity entity in entities)
            {
                if (entity is Movable)
                {
                    Movable movable = entity as Movable;

                    if (movable is Damaging)
                    {
                        Damaging damaging = movable as Damaging;

                        if (damaging.Active)
                        {
                            if (damaging is ChainSaw)
                            {
                                ChainSaw chain = damaging as ChainSaw;

                                Rectangle sourceRect = new Rectangle(0, 0, chain.Texture.Width, chain.Texture.Height);
                                if (chain.Direction == 1)
                                {
                                    sb.Draw(chain.Texture, chain.CoRect, sourceRect, Color.White, chain.Rotation, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                                }
                                else
                                {
                                    sb.Draw(chain.Texture, chain.CoRect, sourceRect, Color.White, chain.Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
                                }
                            }
                            else 
                            {
                                sb.Draw(damaging.Texture, damaging.CoRect, Color.White);
                            }
                        }
                        else
                        {

                        }
                    }
                    else if (movable.Direction == 1)
                    if (movable is Enemy)
                    {
                        Enemy enemy = movable as Enemy;
                        if (enemy is Ent)
                        {
                            Ent ent = enemy as Ent;
                            if (ent.Direction == 0)
                            {
                                sb.Draw(ent.Texture, ent.CoRect, Color.White);
                            }
                            else
                            {
                                Rectangle sourceRect = new Rectangle(0, 0, ent.Texture.Width, ent.Texture.Height);
                                sb.Draw(ent.Texture, ent.CoRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                            }
                        }
                    }
                    else if (movable.Direction == 0)
                    {
                        sb.Draw(movable.Texture, movable.CoRect, Color.White);
                    }
                    else
                    {
                        Rectangle sourceRect = new Rectangle(0,0, movable.Texture.Width, movable.Texture.Height);
                        sb.Draw(movable.Texture, movable.CoRect, sourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    }
                    
 
                }
                if (entity is Obstacle)
                {
                    Obstacle obstacle = entity as Obstacle;
                    //TODO: add code for drawing obstacles to the screen. s
                }
            }
        }

 
        public void DrawBackground(SpriteBatch sb, Texture2D ground, int x, int y)
        {
            sb.Draw(ground, new Vector2(x, y), Color.White);
        }

        public void DrawMenu(SpriteBatch sb, GameStates gState, Menu cMenu)
        {
            //if state is in menu state
            if (gState == GameStates.Menu)
            {

                sb.Draw(cMenu.getsetImage, new Rectangle(0, 0, cMenu.getsetImage.Width, cMenu.getsetImage.Height), Color.White);
            }
 
        }


        public void DrawOverlay(SpriteBatch sb, SpriteFont font, string hp)
        {
            sb.DrawString(font, "HP: " + hp, new Vector2(0, 0), Color.Black);
        }
    }
}
