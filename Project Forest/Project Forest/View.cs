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

        //Accessor and mutator for state
        public ViewStates State
        {
            get { return state; }
            set { state = value; }
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

        public void Draw(SpriteBatch sb, List<IEntity> list)
        {
            entities = list; //Assign the passed in list to the local list

            //Draw all of the entities
            foreach (IEntity entity in entities)
            {
                if (entity is Movable)
                {
                    Movable movable = entity as Movable;
                    sb.Draw(movable.Texture, new Vector2(movable.X, movable.Y), Color.White);
                }
                if (entity is Obstacle)
                {
                    Obstacle obstacle = entity as Obstacle;
                    //TODO: add code for drawing obstacles to the screen. s
                }
            }
        }
    }
}
