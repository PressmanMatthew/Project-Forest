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

        CreditsMenu creditsMenu;
        ControlsMenu controlsMenu;
        MainMenu mainMenu;
        List<MenuOption> mainMenuOptions;
        List<MenuOption> controlsOptions;
        List<MenuOption> creditsOptions;
        Menu currentMenu; 
        ButtonController buttons;
        Rectangle menuSelectionArrowRect;

        //field that holds current game state
        private GameStates gameState;
        private MenuStates menuState;

        //texture fields
        private Texture2D backgroundImage;
        private Texture2D mainMenuImage;
        private Texture2D controlsMenuImage;
        private Texture2D creditsMenuImage;

        Level firstLevel;

        MainCharacter playerCharacter;
        Ent firstEnemy;

        ChainSaw chain;

        List<IEntity> entities;

        View view;

        KeyboardState kbState;

        Texture2D entTexture;
        Texture2D mainTexture;
        Texture2D chainTexture;
        Texture2D groundTexture;
        Texture2D menuSelectionArrowTexture;
        SpriteFont arial;

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

        KeyboardState previousKbState;
 

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
            mainCharacterStartingRect = new Rectangle(mainCharacterStartingX, mainCharacterStartingY, 72, 100);

            firstEnemyStartingX = ((GraphicsDevice.Viewport.Width / 3) * 2 + 50);
            firstEnemyStartingY = (GraphicsDevice.Viewport.Height / 6) * 3;
            firstEnemyStartingRect = new Rectangle(firstEnemyStartingX, firstEnemyStartingY, 120, 70);
            chainRect = new Rectangle(mainCharacterStartingX + 30, mainCharacterStartingY + 40, 50, 25);
            firstEnemyAttackRangeRect = new Rectangle(firstEnemyStartingX - 10, firstEnemyStartingY - 10, firstEnemyStartingRect.Width + 20, firstEnemyStartingRect.Height + 20);
            localEnemyAttackRanRect = firstEnemyAttackRangeRect;
            firstLevel = new Level();
 
            entities = new List<IEntity>();
            view = new View();

            startedAttacking = false;
            gameOver = false;

            //set gamestate to menu state
            gameState = GameStates.Menu;
            menuState = MenuStates.MainMenu;

            mainMenuOptions = new List<MenuOption>();

            menuSelectionArrowRect.X = (GraphicsDevice.Viewport.Width / 2) - 200;
            menuSelectionArrowRect.Width = 50;
            menuSelectionArrowRect.Height = 50;

            mainMenuOptions.Add(new MenuOption("Play", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 100));
            mainMenuOptions.Add(new MenuOption("Controls", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 50));
            mainMenuOptions.Add(new MenuOption("Credits", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            mainMenuOptions.Add(new MenuOption("Quit", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 50));

            controlsOptions = new List<MenuOption>();

            controlsOptions.Add(new MenuOption("Left", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 150));
            controlsOptions.Add(new MenuOption("Right", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 100));
            controlsOptions.Add(new MenuOption("Melee", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 50));
            controlsOptions.Add(new MenuOption("MidRange", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            controlsOptions.Add(new MenuOption("Range", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 50));
            controlsOptions.Add(new MenuOption("Back",(GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 100));

            creditsOptions = new List<MenuOption>();

            creditsOptions.Add(new MenuOption("Back", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) -25 ));

            mainMenu = new MainMenu(mainMenuImage, mainMenuOptions);
            //keys.Clear();



            controlsMenu = new ControlsMenu(controlsMenuImage, controlsOptions);
            creditsMenu = new CreditsMenu(creditsMenuImage, creditsOptions);

            currentMenu = mainMenu;
            mainMenu.CurrentMenuSelection = ArrowSelection.Play;
            controlsMenu.CurrentMenuSelection = ArrowSelection.MoveLeft;
            creditsMenu.CurrentMenuSelection = ArrowSelection.Back;

            buttons = new ButtonController();
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

 
            mainTexture = this.Content.Load<Texture2D>("Main Character200");
            entTexture = this.Content.Load<Texture2D>("Ent 200");
            chainTexture = this.Content.Load<Texture2D>("ChainSaw");
            groundTexture = this.Content.Load<Texture2D>("Ground");

            //loading textures (INSERT BACKGROUND FILE NAME and Menu FILE NAME)
            mainMenuImage = this.Content.Load<Texture2D>("MainMenu");
            controlsMenuImage = this.Content.Load<Texture2D>("ControlsMenu");
            creditsMenuImage = this.Content.Load<Texture2D>("CreditsMenu");
            menuSelectionArrowTexture = this.Content.Load<Texture2D>("ArrowRight");


            arial = this.Content.Load<SpriteFont>("Arial14");

            playerCharacter = new MainCharacter(mainCharacterStartingX, mainCharacterStartingY, mainCharacterStartingRect, mainTexture, 1, 10, 100, chain);
            firstEnemy = new Ent(firstEnemyStartingX, firstEnemyStartingY, firstEnemyStartingRect, entTexture, 1, 5, 100, firstEnemyAttackRangeRect);
            chain = new ChainSaw(mainCharacterStartingX, mainCharacterStartingY, chainRect, chainTexture, 0, 2, 50);

            mainMenu.getsetImage = mainMenuImage;
            controlsMenu.getsetImage = controlsMenuImage;
            creditsMenu.getsetImage = creditsMenuImage;

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

            previousKbState = kbState;
 
            kbState = Keyboard.GetState();

            switch (gameState)
            {
                //if gamestate is menu do these things
                case GameStates.Menu:
                    switch (menuState)
                    {
                        case MenuStates.MainMenu:
                            currentMenu = mainMenu;

                            if (currentMenu.CurrentMenuSelection == ArrowSelection.Play)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) - 100);
                                if(SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Controls;
                                }
                                if(SingleKeyPress(Keys.Enter))
                                {
                                    gameState = GameStates.Game;
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Controls)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) -50);
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Play;
                                }
                                if(SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Credits;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.Controls;
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Credits)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2));
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Controls;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Quit;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.Credits;
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Quit)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) + 50);

                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Credits;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    this.Exit();
                                }
                            }
                            break;

                        case MenuStates.Controls:
                            currentMenu = controlsMenu;

                            if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveLeft)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2)-150);                                
                                if(SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if(SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }

                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveRight)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) - 100);

                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveLeft;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MeleeAttack;

                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MeleeAttack)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) -50);
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if(SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MidRangeAttack;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MidRangeAttack)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2));
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MeleeAttack;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.RangeAttack;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.RangeAttack)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2)+50);
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MidRangeAttack;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Back;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }
                            else if(currentMenu.CurrentMenuSelection == ArrowSelection.Back)
                            {
                                menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) + 100);
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.RangeAttack;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.MainMenu;
                                }
                            }

                            //////if user presses...Back
                            ////if (SingleKeyPress(controls.getKeys[0]))
                            ////{
                            ////    //set drawing
                            ////    menuState = MenuStates.MainMenu;
                            ////}
                            break;
                        case MenuStates.Credits:
                            currentMenu = creditsMenu;
                            menuSelectionArrowRect.Y = ((GraphicsDevice.Viewport.Height / 2) - 25);
                            //if user presses...Back
                            if (SingleKeyPress(Keys.Enter))
                            {
                                menuState = MenuStates.MainMenu;
                            }
                            break;

                    }
                    break;
                case GameStates.Game:
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
                                playerCharacter.Direction = 1;
                                playerCharacter.X += playerCharacter.Speed;
                                if (kbState.IsKeyUp(Keys.Right))
                                {
                                    playerCharacter.State = CharacterStates.FaceRight;
                                }
                                break;
                            case CharacterStates.WalkLeft:
                                playerCharacter.Direction = 0;
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
                                firstEnemy.AtkRanRect = localEnemyAttackRanRect;
                                if (firstEnemy.AtkRanRect.Intersects(playerCharacter.CoRect))
                                {
                                    firstEnemy.State = CharacterStates.MeleeAttack;
                                }
                                break;
                            case CharacterStates.WalkLeft:
                                firstEnemy.Move(playerCharacter);
                                localEnemyAttackRanRect.X -= firstEnemy.Speed;
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
                                    startedAttacking = true;
                                }
                                else if (startingAttackTime + 3 == (int)gameTime.TotalGameTime.TotalSeconds)
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
                    }
                    break;
            }

            if(gameState == GameStates.Menu)
            {
                
            }

            if (playerCharacter.HP < 1)
            {
                gameOver = true;
            }
            if (gameOver)
            {
                this.Exit();
            }

            System.Diagnostics.Debug.WriteLine(menuSelectionArrowRect);

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

            view.DrawBackground(spriteBatch, groundTexture, 0, mainCharacterStartingY + mainCharacterStartingRect.Height);
 
            view.DrawEntities(spriteBatch, entities);

            view.DrawOverlaw(spriteBatch, arial, playerCharacter.HP.ToString());

            view.DrawMenu(spriteBatch, gameState, currentMenu, menuSelectionArrowRect, menuSelectionArrowTexture);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool SingleKeyPress(Keys key)
        {
            if (previousKbState.IsKeyUp(key) && kbState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }
    }
}
