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
        Texture2D chainTexture;
        Texture2D groundTexture;

        int mainCharacterStartingX;
        int mainCharacterStartingY;
        Rectangle mainCharacterStartingRect;

        int firstEnemyStartingX;
        int firstEnemyStartingY;
        Rectangle firstEnemyStartingRect;
        Rectangle firstEnemyAttackRangeRect;
        Rectangle chainRect;
        Rectangle localEnemyAttackRanRect;

        bool startedAttacking;
        int startingAttackTime;
        bool gameOver;

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
            mainCharacterStartingX = (GraphicsDevice.Viewport.Width / 3);
            mainCharacterStartingY = (GraphicsDevice.Viewport.Height / 6) * 3;
            mainCharacterStartingRect = new Rectangle(mainCharacterStartingX, mainCharacterStartingY, 50, 100);

            firstEnemyStartingX = ((GraphicsDevice.Viewport.Width / 3) * 2 + 50);
            firstEnemyStartingY = (GraphicsDevice.Viewport.Height / 6) * 3;
            firstEnemyStartingRect = new Rectangle(firstEnemyStartingX, firstEnemyStartingY, 120, 70);
            chainRect = new Rectangle(mainCharacterStartingX + 30, mainCharacterStartingY + 40, 50, 25);
            firstEnemyAttackRangeRect = new Rectangle(firstEnemyStartingX - 10, firstEnemyStartingY - 10, firstEnemyStartingRect.Width + 20, firstEnemyStartingRect.Height + 20);
            localEnemyAttackRanRect = firstEnemyAttackRangeRect;
            firstLevel = new Level();
            entities = new List<IEntity>();
            view = new View();
            model = new Model();
            mainMenu = new Menu();

            startedAttacking = false;
            gameOver = false;

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
            entTexture = this.Content.Load<Texture2D>("Ent 200");
            chainTexture = this.Content.Load<Texture2D>("ChainSaw");
            groundTexture = this.Content.Load<Texture2D>("Ground");

            playerCharacter = new MainCharacter(mainCharacterStartingX, mainCharacterStartingY, mainCharacterStartingRect, mainTexture, 1, 10, 100, chain);
            firstEnemy = new Ent(firstEnemyStartingX, firstEnemyStartingY, firstEnemyStartingRect, entTexture, 1, 5, 100, firstEnemyAttackRangeRect);
            chain = new ChainSaw(mainCharacterStartingX, mainCharacterStartingY, chainRect, chainTexture, 0, 2, 50);

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
            playerCharacter.Speed = gameTime.ElapsedGameTime.Milliseconds / 3;

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
                switch (firstEnemy.State)
                {
                    case CharacterStates.FaceRight:
                        if (playerCharacter.X > firstEnemy.X && !firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.WalkRight;
                        }
                        if (playerCharacter.X < firstEnemy.X && !firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.WalkLeft;
                        }
                        break;
                    case CharacterStates.FaceLeft:
                        if (playerCharacter.X > firstEnemy.X && !firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.WalkRight;
                        }
                        if (playerCharacter.X < firstEnemy.X && !firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.WalkLeft;
                        }
                        break;
                    case CharacterStates.WalkRight:
                        firstEnemy.Move(playerCharacter);
                        localEnemyAttackRanRect.X += firstEnemy.Speed;
                        localEnemyAttackRanRect.Y += firstEnemy.Speed;
                        firstEnemy.AtkRanRect = localEnemyAttackRanRect;
                        if (firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.MeleeAttack;
                        }
                        break;
                    case CharacterStates.WalkLeft:
                        firstEnemy.Move(playerCharacter);
                        localEnemyAttackRanRect.X += firstEnemy.Speed;
                        localEnemyAttackRanRect.Y += firstEnemy.Speed;
                        firstEnemy.AtkRanRect = localEnemyAttackRanRect;
                        if (firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                        {
                            firstEnemy.State = CharacterStates.MeleeAttack;
                        }
                        break;
                    case CharacterStates.MeleeAttack:
                        if (startedAttacking == false)
                        {
                            firstEnemy.Attack(playerCharacter);
                            startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                        }
                        else if (startingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                        {
                            startedAttacking = false;
                            if (!firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X < firstEnemy.X)
                            {
                                firstEnemy.State = CharacterStates.WalkLeft;
                            }
                            if (!firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X > firstEnemy.X)
                            {
                                firstEnemy.State = CharacterStates.WalkRight;
                            }
                        }
                        break;
                }
                if (playerCharacter.HP < 1)
                {
                    gameOver = true;
                }
                if (gameOver)
                {
                    this.Exit();
                }
            }

            entities.Clear();
            entities.Add(playerCharacter);
            entities.Add(firstEnemy);

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

            view.DrawEntities(spriteBatch, entities);

            view.DrawBackground(spriteBatch, groundTexture, 0, mainCharacterStartingY + mainCharacterStartingRect.Height);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
