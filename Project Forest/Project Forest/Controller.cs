#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Project_Forest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Controller : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level firstLevel;

        MainCharacter playerCharacter;
        Ent firstEnemy;

        ChainSaw chain;

        List<IEntity> entities;

        View view;
        Model model;
        Menu mainMenu;

        KeyboardState kbState;
        Texture2D entTexture;
        Texture2D mainTexture;

        public Controller()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            firstLevel = new Level();
            firstEnemy = new Ent();
            chain = new ChainSaw();
            entities = new List<IEntity>();
            view = new View();
            model = new Model();
            mainMenu = new Menu();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainTexture = this.Content.Load<Texture2D>("Main Character");

            playerCharacter = new MainCharacter((GraphicsDevice.Viewport.Width / 3) * 2, GraphicsDevice.Viewport.Height / 6,
                new Rectangle((GraphicsDevice.Viewport.Width / 3) * 2, GraphicsDevice.Viewport.Height / 6, 50, 100), mainTexture, 1, 5, 100, chain);

            entities.Add(playerCharacter);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            kbState = Keyboard.GetState();

            if (view.State == ViewStates.Stationary)
            {
                switch (playerCharacter.State)
                {
                    case CharacterStates.FaceRight:
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                            playerCharacter.State = CharacterStates.WalkRight;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            playerCharacter.State = CharacterStates.WalkLeft;
                        }
                        break;
                    case CharacterStates.FaceLeft:
                        if (kbState.IsKeyDown(Keys.Right))
                        {
                            playerCharacter.State = CharacterStates.WalkRight;
                        }
                        if (kbState.IsKeyDown(Keys.Left))
                        {
                            playerCharacter.State = CharacterStates.WalkLeft;
                        }
                        break;
                    case CharacterStates.WalkRight:
                        playerCharacter.X += playerCharacter.Speed;
                        if (kbState.IsKeyUp(Keys.Right))
                        {
                            playerCharacter.State = CharacterStates.FaceRight;
                        }
                        break;
                    case CharacterStates.WalkLeft:
                        playerCharacter.X -= playerCharacter.Speed;
                        if (kbState.IsKeyUp(Keys.Left))
                        {
                            playerCharacter.State = CharacterStates.FaceLeft;
                        }
                        break;
                    case CharacterStates.MeleeAttack:
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();

            view.Draw(spriteBatch, entities);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
