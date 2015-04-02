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

<<<<<<< HEAD
        public void DrawEntities(SpriteBatch sb, List<IEntity> list)
=======
        public void Draw(SpriteBatch sb, List<IEntity> list)
>>>>>>> origin/Menu
        {
            entities = list; //Assign the passed in list to the local list

            //Draw all of the entities
            foreach (IEntity entity in entities)
            {
                if (entity is Movable)
                {
                    Movable movable = entity as Movable;
<<<<<<< HEAD
                    sb.Draw(movable.Texture, movable.CoRect, Color.White);
=======
                    sb.Draw(movable.Texture, new Vector2(movable.X, movable.Y), Color.White);
>>>>>>> origin/Menu
                }
                if (entity is Obstacle)
                {
                    Obstacle obstacle = entity as Obstacle;
                    //TODO: add code for drawing obstacles to the screen. s
                }
            }
        }

<<<<<<< HEAD
        public void DrawBackground(SpriteBatch sb, Texture2D ground, int x, int y)
        {
            sb.Draw(ground, new Vector2(x, y), Color.White);
=======
        public void DrawMenu(SpriteBatch sb, GameStates gState, Menu cMenu)
        {
            //if state is in menu state
            if (gState == GameStates.Menu)
            {

                sb.Draw(cMenu.getsetImage, new Rectangle(0, 0, cMenu.getsetImage.Width, cMenu.getsetImage.Height), Color.White);
            }
>>>>>>> origin/Menu
        }
    }
}
