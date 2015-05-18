#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
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
        PauseMenu pauseMenu;
        GameOverMenu gameOverMenu;
        SummaryMenu summaryMenu;

        List<MenuOption> mainMenuOptions;
        List<MenuOption> controlsOptions;
        List<MenuOption> creditsOptions;
        List<MenuOption> pauseOptions;
        List<MenuOption> gameOverOptions;
        List<MenuOption> thereAreNoOptions;
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
        private Texture2D pauseMenuImage;
        private Texture2D gameOverMenuImage;
        private Texture2D summaryImage;

        Level firstLevel;
        FightScene currentFightScene;

        MainCharacter playerCharacter;
        Ent firstEnemy;

        SoundEffect music;
        SoundController musicController;

        ChainSaw chain;
        Flame fire;

        List<IEntity> entities;

        View view;

        KeyboardState kbState;

        Texture2D entTexture;
        Texture2D mainTexture;
        Texture2D chainTexture;
        Texture2D flameTexture;
        Texture2D groundTexture;
        Texture2D backgroundTexture;
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
        Rectangle flameRect;
        Rectangle localEnemyAttackRanRect;

        bool startedAttacking;
        int startingAttackTime;
        int enemyStartingAttackTime;
        bool enemyStartedAttacking;
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
            firstEnemyStartingY = mainCharacterStartingY + mainCharacterStartingRect.Height - 117;
            firstEnemyStartingRect = new Rectangle(firstEnemyStartingX, firstEnemyStartingY, 200, 117);
            chainRect = new Rectangle(mainCharacterStartingX + 30, mainCharacterStartingY + 40, 50, 25);
            flameRect = new Rectangle(mainCharacterStartingX + 30, mainCharacterStartingY + 40, 150, 87);
            firstEnemyAttackRangeRect = new Rectangle(firstEnemyStartingX - 10, firstEnemyStartingY - 10, firstEnemyStartingRect.Width + 20, firstEnemyStartingRect.Height + 20);
            localEnemyAttackRanRect = firstEnemyAttackRangeRect;
 
            entities = new List<IEntity>();
            view = new View();
            view.State = ViewStates.Stationary;

            startedAttacking = false;
            enemyStartingAttackTime = 0;
            enemyStartedAttacking = false;
            gameOver = false;

            //set gamestate to menu state
            gameState = GameStates.Menu;
            menuState = MenuStates.MainMenu;

            mainMenuOptions = new List<MenuOption>();

            menuSelectionArrowRect.X = (GraphicsDevice.Viewport.Width / 2) - 150;
            menuSelectionArrowRect.Width = 50;
            menuSelectionArrowRect.Height = 50;

            mainMenuOptions.Add(new MenuOption("Play", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            mainMenuOptions.Add(new MenuOption("Controls", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 50));
            mainMenuOptions.Add(new MenuOption("Credits", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 100));
            mainMenuOptions.Add(new MenuOption("Quit", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 150));

            controlsOptions = new List<MenuOption>();

            controlsOptions.Add(new MenuOption("Left", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 100));
            controlsOptions.Add(new MenuOption("Right", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 50));
            controlsOptions.Add(new MenuOption("Melee", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            controlsOptions.Add(new MenuOption("MidRange", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 50));
            controlsOptions.Add(new MenuOption("Range", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 100));
            controlsOptions.Add(new MenuOption("Back", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 150));

            creditsOptions = new List<MenuOption>();

            creditsOptions.Add(new MenuOption("Back", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 150));

            pauseOptions = new List<MenuOption>();

            pauseOptions.Add(new MenuOption("Resume", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 50));
            pauseOptions.Add(new MenuOption("Controls", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            pauseOptions.Add(new MenuOption("Main Menu", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 50));

            gameOverOptions = new List<MenuOption>();

            gameOverOptions.Add(new MenuOption("Main Menu", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2)));
            gameOverOptions.Add(new MenuOption("Quit", (GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) + 75));

            mainMenu = new MainMenu(mainMenuImage, mainMenuOptions);
            controlsMenu = new ControlsMenu(controlsMenuImage, controlsOptions);
            creditsMenu = new CreditsMenu(creditsMenuImage, creditsOptions);
            pauseMenu = new PauseMenu(pauseMenuImage, pauseOptions);
            gameOverMenu = new GameOverMenu(gameOverMenuImage, gameOverOptions);
            summaryMenu = new SummaryMenu(summaryImage, thereAreNoOptions = new List<MenuOption>());

            currentMenu = mainMenu;
            mainMenu.CurrentMenuSelection = ArrowSelection.Play;
            controlsMenu.CurrentMenuSelection = ArrowSelection.MoveLeft;
            creditsMenu.CurrentMenuSelection = ArrowSelection.Back;
            pauseMenu.CurrentMenuSelection = ArrowSelection.Resume;
            gameOverMenu.CurrentMenuSelection = ArrowSelection.MainMenu;


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
            chainTexture = this.Content.Load<Texture2D>("ChainSaw200");
            flameTexture = this.Content.Load<Texture2D>("Fire 150x87");
            groundTexture = this.Content.Load<Texture2D>("Ground");
            backgroundTexture = this.Content.Load<Texture2D>("Background");

            //loading textures (INSERT BACKGROUND FILE NAME and Menu FILE NAME)
            mainMenuImage = this.Content.Load<Texture2D>("MainMenu");
            controlsMenuImage = this.Content.Load<Texture2D>("ControlsMenu");
            creditsMenuImage = this.Content.Load<Texture2D>("CreditsMenu");
            pauseMenuImage = this.Content.Load<Texture2D>("PauseMenu");
            gameOverMenuImage = this.Content.Load<Texture2D>("GameOverMenu");
            menuSelectionArrowTexture = this.Content.Load<Texture2D>("ArrowRight");
            summaryImage = this.Content.Load<Texture2D>("OpeningSummary");
            

            arial = this.Content.Load<SpriteFont>("Arial14");

            //music = Content.Load<SoundEffect>("TerrariaMusic");
            //musicController = new SoundController(music);

            chain = new ChainSaw(mainCharacterStartingX + mainCharacterStartingRect.Width - 20, mainCharacterStartingY, chainRect, chainTexture, 0, 2, 50);
            chain.Active = false;

            fire = new Flame(mainCharacterStartingX + mainCharacterStartingRect.Width - 20, mainCharacterStartingY-200, flameRect, flameTexture, 0, 2, 50);
            fire.Active = false;

            playerCharacter = new MainCharacter(mainCharacterStartingX, mainCharacterStartingY, mainCharacterStartingRect, mainTexture, 1, 10, 100, chain, fire);
            firstEnemy = new Ent(firstEnemyStartingX, firstEnemyStartingY, firstEnemyStartingRect, entTexture, 1, 5, 100, firstEnemyAttackRangeRect);

            firstLevel = new Level(firstEnemy);
            currentFightScene = firstLevel.Encounter();

            mainMenu.getsetImage = mainMenuImage;
            controlsMenu.getsetImage = controlsMenuImage;
            creditsMenu.getsetImage = creditsMenuImage;
            pauseMenu.getsetImage = pauseMenuImage;
            gameOverMenu.getsetImage = gameOverMenuImage;
            summaryMenu.getsetImage = summaryImage;

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
            //if(musicController.Music.State == SoundState.Stopped)
            //{
            //    musicController.Play();
            //}
            playerCharacter.Speed = gameTime.ElapsedGameTime.Milliseconds / 3;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKbState = kbState;
 
            kbState = Keyboard.GetState();

            switch (gameState)
            {
                //if gamestate is menu do these things
                #region GameStates.Menu
                case GameStates.Menu:
                    switch (menuState)
                    {
                        #region MenuStates.MainMenu
                        case MenuStates.MainMenu:
                            currentMenu = mainMenu;

                            //Play
                            if (currentMenu.CurrentMenuSelection == ArrowSelection.Play)
                            {
                                menuSelectionArrowRect.Y = mainMenuOptions[0].Position.Y;
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Controls;
                                }
                                if (SingleKeyPress(Keys.Enter))//go to GameState.Game
                                {
                                    //gameState = GameStates.Game;
                                    menuState = MenuStates.Summary;
                                }
                            }
                            //Controls
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Controls)
                            {
                                menuSelectionArrowRect.Y = mainMenuOptions[1].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Play;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Credits;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.Controls;//go to MenuState.Controls
                                }
                            }
                            //Credits
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Credits)
                            {
                                menuSelectionArrowRect.Y = mainMenuOptions[2].Position.Y;
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
                                    menuState = MenuStates.Credits;//go to MenuState.Credits
                                }
                            }
                            //Exit
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Quit)
                            {
                                menuSelectionArrowRect.Y = mainMenuOptions[3].Position.Y;

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
                        #endregion
                        #region MenuStates.Controls
                        case MenuStates.Controls:
                            currentMenu = controlsMenu;

                            //Code to change Controls
                            if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveLeft)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[0].Position.Y;
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }

                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveRight)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[1].Position.Y;

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
                                menuSelectionArrowRect.Y = controlsOptions[2].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if (SingleKeyPress(Keys.Down))
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
                                menuSelectionArrowRect.Y = controlsOptions[3].Position.Y;
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
                                menuSelectionArrowRect.Y = controlsOptions[4].Position.Y;
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
                            //Back
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Back)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[5].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.RangeAttack;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.MainMenu;
                                }
                            }
                            break;
                        #endregion
                        #region MenuStates.Credits
                        case MenuStates.Credits:
                            currentMenu = creditsMenu;
                            menuSelectionArrowRect.Y = creditsOptions[0].Position.Y;
                            //Back
                            if (SingleKeyPress(Keys.Enter))
                            {
                                menuState = MenuStates.MainMenu;//go to MenuStates.MainMenu
                            }
                            break;
                        #endregion
                        case MenuStates.Summary:
                            currentMenu = summaryMenu;
                            if (SingleKeyPress(Keys.Enter))//go to GameState.Game
                            {
                                gameState = GameStates.Game;
                            }
                            break;
                    }
                    break;
                #endregion
                #region GameStates.Game
                case GameStates.Game:
                    #region ViewStates.Moving
                    if (view.State == ViewStates.Moving)
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

                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
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

                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;

                            case CharacterStates.WalkRight:

                                playerCharacter.Direction = 1;

                                playerCharacter.X += playerCharacter.Speed;

                                view.X += playerCharacter.Speed;
                                chain.X += playerCharacter.Speed;

                                if (kbState.IsKeyUp(Keys.Right))
                                {

                                    playerCharacter.State = CharacterStates.FaceRight;

                                }

                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;

                            case CharacterStates.WalkLeft:

                                playerCharacter.Direction = 0;

                                playerCharacter.X -= playerCharacter.Speed;

                                view.X += playerCharacter.Speed;
                                chain.X -= playerCharacter.Speed;

                                if (playerCharacter.X < 0)
                                {
                                    playerCharacter.X += playerCharacter.Speed;
                                    chain.X = playerCharacter.X;
                                }

                                if (kbState.IsKeyUp(Keys.Left))
                                {

                                    playerCharacter.State = CharacterStates.FaceLeft;

                                }
                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;

                            case CharacterStates.MeleeAttack:
                                if (playerCharacter.Direction == 0)
                                {
                                    playerCharacter.Chainsaw.Direction = 0;
                                    playerCharacter.Chainsaw.Rotation -= .0349066f;
                                }
                                else
                                {
                                    playerCharacter.Chainsaw.Direction = 1;
                                    playerCharacter.Chainsaw.Rotation += .0349066f;
                                }
                                if (startedAttacking == false)
                                {
                                    foreach (Enemy enemy in currentFightScene.Enemies)
                                    {
                                        if (playerCharacter.Chainsaw.IsColliding(enemy))
                                        {
                                            playerCharacter.Attack(enemy);
                                        }
                                    }
                                    startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                    startedAttacking = true;
                                    playerCharacter.Chainsaw.Active = true;
                                }
                                else if (startingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                                {
                                    playerCharacter.Chainsaw.Active = false;
                                    startedAttacking = false;
                                    playerCharacter.Chainsaw.Rotation = playerCharacter.Chainsaw.DefaultRotation;
                                    if (playerCharacter.Direction == 0)
                                    {
                                        playerCharacter.State = CharacterStates.FaceLeft;
                                    }
                                    if (playerCharacter.Direction == 1)
                                    {
                                        playerCharacter.State = CharacterStates.FaceRight;
                                    }
                                }
                                if (kbState.IsKeyDown(Keys.Right))
                                {
                                    playerCharacter.Direction = 1;

                                    playerCharacter.X += playerCharacter.Speed;

                                    view.X += playerCharacter.Speed;
                                    chain.X += playerCharacter.Speed;

                                }
                                if (kbState.IsKeyDown(Keys.Left))
                                {
                                    playerCharacter.Direction = 0;

                                    playerCharacter.X -= playerCharacter.Speed;

                                    view.X += playerCharacter.Speed;
                                    chain.X -= playerCharacter.Speed;

                                    if (playerCharacter.X < 0)
                                    {
                                        playerCharacter.X += playerCharacter.Speed;
                                        chain.X = playerCharacter.X;
                                    }


                                }
                                    break;
                            case CharacterStates.RangedAttack:
                                playerCharacter.Fire.X = playerCharacter.X;
                                playerCharacter.Fire.Move();
                                if (playerCharacter.Direction == 0)
                                {
                                    playerCharacter.Fire.Direction = 0;
                                }
                                else
                                {
                                    playerCharacter.Fire.Direction = 1;
                                }
                                if (startedAttacking == false)
                                {
                                    foreach (Enemy enemy in currentFightScene.Enemies)
                                    {
                                        if (playerCharacter.Fire.IsColliding(enemy))
                                        {
                                            playerCharacter.Attack(enemy);
                                        }
                                    }
                                    startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                    startedAttacking = true;
                                    playerCharacter.Fire.Active = true;
                                }
                                else if (startingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                                {
                                    playerCharacter.Fire.Active = false;
                                    startedAttacking = false;
                                    if (playerCharacter.Direction == 0)
                                    {
                                        playerCharacter.State = CharacterStates.FaceLeft;
                                    }
                                    if (playerCharacter.Direction == 1)
                                    {
                                        playerCharacter.State = CharacterStates.FaceRight;
                                    }
                                }
                                break;

                        }

                        if (playerCharacter.X > GraphicsDevice.Viewport.Width / 2)
                        {
                            playerCharacter.X = GraphicsDevice.Viewport.Width / 2;
                            chain.X = GraphicsDevice.Viewport.Width / 2;
                            fire.X = GraphicsDevice.Viewport.Width / 2;
                        }

                        if (view.X >= firstLevel.CurrentFightSceneX)
                        {

                            view.X = firstLevel.CurrentFightSceneX;

                            currentFightScene = firstLevel.Encounter();

                            view.State = ViewStates.Stationary;

                        }

                    }
#endregion
                    #region ViewStates.Stationary
                    if (view.State == ViewStates.Stationary)
                    {
                        #region Player Finite State Machine
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
                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
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
                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;
                            case CharacterStates.WalkRight:
                                playerCharacter.Direction = 1;
                                playerCharacter.X += playerCharacter.Speed;
                                chain.X += playerCharacter.Speed;
                                if (playerCharacter.X > (GraphicsDevice.Viewport.Width - playerCharacter.CoRect.Width))
                                {
                                    playerCharacter.X -= playerCharacter.Speed;
                                    chain.X = playerCharacter.X;
                                }
                                if (kbState.IsKeyUp(Keys.Right))
                                {
                                    playerCharacter.State = CharacterStates.FaceRight;
                                }
                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;
                            case CharacterStates.WalkLeft:
                                playerCharacter.Direction = 0;
                                playerCharacter.X -= playerCharacter.Speed;
                                chain.X -= playerCharacter.Speed;
                                if (playerCharacter.X < 0)
                                {
                                    playerCharacter.X += playerCharacter.Speed;
                                    chain.X = playerCharacter.X;
                                }
                                if (kbState.IsKeyUp(Keys.Left))
                                {
                                    playerCharacter.State = CharacterStates.FaceLeft;
                                }
                                if (kbState.IsKeyDown(Keys.Z))
                                {
                                    playerCharacter.State = CharacterStates.MeleeAttack;
                                }
                                if (kbState.IsKeyDown(Keys.X))
                                {
                                    playerCharacter.State = CharacterStates.RangedAttack;
                                }
                                break;
                            case CharacterStates.MeleeAttack:
                                if (playerCharacter.Direction == 0)
                                {
                                    chain.Direction = 0;
                                    chain.Rotation += .0349066f;
                                }
                                else
                                {
                                    chain.Direction = 1;
                                    chain.Rotation -= .0349066f;
                                }
                                if (startedAttacking == false)
                                {
                                    foreach (Enemy enemy in currentFightScene.Enemies)
                                    {
                                        if (playerCharacter.Chainsaw.IsColliding(enemy))
                                        {
                                            playerCharacter.Attack(enemy);
                                        }
                                    }
                                    startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                    startedAttacking = true;
                                    chain.Active = true;
                                }
                                else if (startingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                                {
                                    chain.Active = false;
                                    startedAttacking = false;
                                    chain.Rotation = chain.DefaultRotation;
                                    if (playerCharacter.Direction == 0)
                                    {
                                        playerCharacter.State = CharacterStates.FaceLeft;
                                    }
                                    if (playerCharacter.Direction == 1)
                                    {
                                        playerCharacter.State = CharacterStates.FaceRight;
                                    }
                                }
                                if (kbState.IsKeyDown(Keys.Right))
                                {
                                    playerCharacter.Direction = 1;

                                    playerCharacter.X += playerCharacter.Speed;

                                    view.X += playerCharacter.Speed;
                                    chain.X += playerCharacter.Speed;

                                    if (playerCharacter.X > (GraphicsDevice.Viewport.Width - playerCharacter.CoRect.Width))
                                    {
                                        playerCharacter.X -= playerCharacter.Speed;
                                        chain.X = playerCharacter.X;
                                    }
                                }
                                if (kbState.IsKeyDown(Keys.Left))
                                {
                                    playerCharacter.Direction = 0;

                                    playerCharacter.X -= playerCharacter.Speed;

                                    view.X += playerCharacter.Speed;
                                    chain.X -= playerCharacter.Speed;

                                    if (playerCharacter.X < 0)
                                    {
                                        playerCharacter.X += playerCharacter.Speed;
                                        chain.X = playerCharacter.X;
                                    }


                                }
                                break;
                            case CharacterStates.RangedAttack:
                                playerCharacter.Fire.Move();
                                if (playerCharacter.Direction == 0)
                                {
                                    playerCharacter.Fire.Direction = 0;
                                }
                                else
                                {
                                    playerCharacter.Fire.Direction = 1;
                                }
                                if (startedAttacking == false)
                                {
                                    playerCharacter.Fire.X = playerCharacter.X;
                                    foreach (Enemy enemy in currentFightScene.Enemies)
                                    {
                                        if (playerCharacter.Fire.IsColliding(enemy))
                                        {
                                            playerCharacter.Attack(enemy);
                                        }
                                    }
                                    startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                    startedAttacking = true;
                                    playerCharacter.Fire.Active = true;
                                }
                                else if (startingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                                {
                                    playerCharacter.Fire.Active = false;
                                    startedAttacking = false;
                                    if (playerCharacter.Direction == 0)
                                    {
                                        playerCharacter.State = CharacterStates.FaceLeft;
                                    }
                                    if (playerCharacter.Direction == 1)
                                    {
                                        playerCharacter.State = CharacterStates.FaceRight;
                                    }
                                }
                                break;
                        }
                        #endregion
                        #region Ground Enemy State Machine
                        
                        for (int q = 0; q < currentFightScene.Enemies.Count; q++)
                        {
                            if (currentFightScene.Enemies[q] is Ent)
                            {
                                switch (currentFightScene.Enemies[q].State)
                                {
                                    case CharacterStates.FaceRight:
                                        if (playerCharacter.X > currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                        }

                                        if (playerCharacter.X < currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                        }
                                        break;
                                    case CharacterStates.FaceLeft:
                                        if (playerCharacter.X > currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                        }
                                        if (playerCharacter.X < currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                        }
                                        break;
                                    case CharacterStates.WalkRight:
                                        currentFightScene.Enemies[q].Move(playerCharacter);
                                        localEnemyAttackRanRect.X += currentFightScene.Enemies[q].Speed;
                                        currentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                                        if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.MeleeAttack;
                                        }
                                        break;
                                    case CharacterStates.WalkLeft:
                                        currentFightScene.Enemies[q].Move(playerCharacter);
                                        localEnemyAttackRanRect.X -= currentFightScene.Enemies[q].Speed;
                                        currentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                                        if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.MeleeAttack;
                                        }
                                        break;
                                    case CharacterStates.MeleeAttack:
                                        if (enemyStartedAttacking == false)
                                        {
                                            if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                            {
                                                currentFightScene.Enemies[q].Attack(playerCharacter);
                                            }
                                            enemyStartingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                            enemyStartedAttacking = true;
                                        }
                                        else if (enemyStartingAttackTime + 3 == (int)gameTime.TotalGameTime.TotalSeconds)
                                        {
                                            enemyStartedAttacking = false;
                                            if (!currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X < currentFightScene.Enemies[q].X)
                                            {
                                                currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                            }
                                            if (!currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X > currentFightScene.Enemies[q].X)
                                            {
                                                currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        #endregion
                        #region Ranged Enemies State Machine

                        for (int q = 0; q < currentFightScene.Enemies.Count; q++)
                        {
                            if (currentFightScene.Enemies[q] is RangedEnemy || currentFightScene.Enemies[q] is FlyingEnemy)
                            {
                                switch (currentFightScene.Enemies[q].State)
                                {
                                    case CharacterStates.FaceRight:
                                        if (playerCharacter.X > currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                        }

                                        if (playerCharacter.X < currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                        }
                                        break;
                                    case CharacterStates.FaceLeft:
                                        if (playerCharacter.X > currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                        }
                                        if (playerCharacter.X < currentFightScene.Enemies[q].X && !currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                        }
                                        break;
                                    case CharacterStates.WalkRight:
                                        currentFightScene.Enemies[q].Move(playerCharacter);
                                        localEnemyAttackRanRect.X += currentFightScene.Enemies[q].Speed;
                                        currentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                                        if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.RangedAttack;
                                        }
                                        break;
                                    case CharacterStates.WalkLeft:
                                        currentFightScene.Enemies[q].Move(playerCharacter);
                                        localEnemyAttackRanRect.X -= currentFightScene.Enemies[q].Speed;
                                        currentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                                        if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                        {
                                            currentFightScene.Enemies[q].State = CharacterStates.RangedAttack;
                                        }
                                        break;
                                    case CharacterStates.RangedAttack:
                                        if (enemyStartedAttacking == false)
                                        {
                                            if (currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                                            {
                                                currentFightScene.Enemies[q].Attack(playerCharacter);
                                            }
                                            enemyStartingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                                            enemyStartedAttacking = true;
                                        }
                                        else if (enemyStartingAttackTime + 1 == (int)gameTime.TotalGameTime.TotalSeconds)
                                        {
                                            enemyStartedAttacking = false;
                                            if (!currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X < currentFightScene.Enemies[q].X)
                                            {
                                                currentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                                            }
                                            if (!currentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X > currentFightScene.Enemies[q].X)
                                            {
                                                currentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        #endregion
                        #region Old Code
                        //switch (firstcurrentFightScene.Enemies[q].State)
                        //{
                        //    case CharacterStates.FaceRight:
                        //        if (playerCharacter.X > firstcurrentFightScene.Enemies[q].X && !firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                        //        }
                        //        if (playerCharacter.X < firstcurrentFightScene.Enemies[q].X && !firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                        //        }
                        //        break;
                        //    case CharacterStates.FaceLeft:
                        //        if (playerCharacter.X > firstcurrentFightScene.Enemies[q].X && !firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                        //        }
                        //        if (playerCharacter.X < firstcurrentFightScene.Enemies[q].X && !firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                        //        }
                        //        break;
                        //    case CharacterStates.WalkRight:
                        //        firstcurrentFightScene.Enemies[q].Move(playerCharacter);
                        //        localEnemyAttackRanRect.X += firstcurrentFightScene.Enemies[q].Speed;
                        //        firstcurrentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                        //        if (firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.MeleeAttack;
                        //        }
                        //        break;
                        //    case CharacterStates.WalkLeft:
                        //        firstcurrentFightScene.Enemies[q].Move(playerCharacter);
                        //        localEnemyAttackRanRect.X -= firstcurrentFightScene.Enemies[q].Speed;
                        //        firstcurrentFightScene.Enemies[q].AtkRanRect = localEnemyAttackRanRect;
                        //        if (firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect))
                        //        {
                        //            firstcurrentFightScene.Enemies[q].State = CharacterStates.MeleeAttack;
                        //        }
                        //        break;
                        //    case CharacterStates.MeleeAttack:
                        //        if (startedAttacking == false)
                        //        {
                        //            firstcurrentFightScene.Enemies[q].Attack(playerCharacter);
                        //            startingAttackTime = (int)gameTime.TotalGameTime.TotalSeconds;
                        //            startedAttacking = true;
                        //        }
                        //        else if (startingAttackTime + 3 == (int)gameTime.TotalGameTime.TotalSeconds)
                        //        {
                        //            startedAttacking = false;
                        //            if (!firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X < firstcurrentFightScene.Enemies[q].X)
                        //            {
                        //                firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkLeft;
                        //            }
                        //            if (!firstcurrentFightScene.Enemies[q].AtkRanRect.Intersects(playerCharacter.CoRect) && playerCharacter.X > firstcurrentFightScene.Enemies[q].X)
                        //            {
                        //                firstcurrentFightScene.Enemies[q].State = CharacterStates.WalkRight;
                        //            }
                        //        }
                        //        break;
                        //}
#endregion
                    }
                    #endregion
                    //Press 'P' to Pause
                    if (SingleKeyPress(Keys.P))
                    {
                        gameState = GameStates.Pause;
                        menuState = MenuStates.PauseMenu;
                    }
                    if (SingleKeyPress(Keys.G))
                    {
                        gameState = GameStates.GameOver;
                        menuState = MenuStates.GameOverMenu;
                        //currentMenu = gameOverMenu;
                    }
                    break;
                #endregion
                #region GameStates.Pause
                //GameState.Pause is accessed by pressing 'P' while in GameState.Game
                case GameStates.Pause://GameState.Pause uses two Menu states: PauseMenu and Controls    
                    switch (menuState)
                    {
                        #region MenuStates.PauseMenu
                        case MenuStates.PauseMenu://MenuStates.Pause uses 4 ArrowSelections: Resume, MainMenu, Controls, and Exit

                            currentMenu = pauseMenu;

                            //Resume
                            if (currentMenu.CurrentMenuSelection == ArrowSelection.Resume)
                            {
                                menuSelectionArrowRect.Y = pauseOptions[0].Position.Y;
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Controls;
                                }

                                if (SingleKeyPress(Keys.Enter))//Changes gameStates from GameState.Pause to GameState.Game
                                {
                                    gameState = GameStates.Game;
                                }
                            }
                            //Controls
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Controls)
                            {
                                menuSelectionArrowRect.Y = pauseOptions[1].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Resume;
                                }
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MainMenu;
                                }
                                if (SingleKeyPress(Keys.Enter))//Changes menuState from PauseMenu to Controls
                                {
                                    menuState = MenuStates.Controls;
                                    currentMenu = controlsMenu;//idk if this line is important
                                }
                            }
                            //MainMenu
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MainMenu)
                            {
                                menuSelectionArrowRect.Y = pauseOptions[2].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.Controls;
                                }
                                if (SingleKeyPress(Keys.Enter))//Changes gameStates from Pause to Menu and menuState from PauseMenu to MainMenu 
                                {
                                    gameState = GameStates.Menu;
                                    menuState = MenuStates.MainMenu;
                                }
                            }
                            break;
                        #endregion
                        #region MenuStates.Controls
                        case MenuStates.Controls://MenuState.Controls uses a shit ton of ArrowSelections to change game controls(same as in GameState.Menu/MenuState.Controls) and the ArrowSelection Back
                            currentMenu = controlsMenu;

                            //Code to change controls.... idk maybe we just want to be able to view controls in GameState.Pause?
                            if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveLeft)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[0].Position.Y;
                                if (SingleKeyPress(Keys.Down))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    //
                                }
                            }

                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.MoveRight)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[1].Position.Y;

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
                                menuSelectionArrowRect.Y = controlsOptions[2].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.MoveRight;
                                }
                                if (SingleKeyPress(Keys.Down))
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
                                menuSelectionArrowRect.Y = controlsOptions[3].Position.Y;
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
                                menuSelectionArrowRect.Y = controlsOptions[4].Position.Y;
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
                            //Back
                            else if (currentMenu.CurrentMenuSelection == ArrowSelection.Back)
                            {
                                menuSelectionArrowRect.Y = controlsOptions[5].Position.Y;
                                if (SingleKeyPress(Keys.Up))
                                {
                                    currentMenu.CurrentMenuSelection = ArrowSelection.RangeAttack;
                                }
                                if (SingleKeyPress(Keys.Enter))
                                {
                                    menuState = MenuStates.PauseMenu;//Changes menuState from Controls to PauseMenu
                                }
                            }
                            break;
                        #endregion
                    }
                    break;
                #endregion
                #region GameStates.GameOver
                case GameStates.GameOver://GameState.GameOver uses two Menu states: PauseMenu and Controls    

                    menuState = MenuStates.GameOverMenu;
                    currentMenu = gameOverMenu;
                    Console.WriteLine(currentMenu.getsetImage);
                    //MainMenu
                    if (currentMenu.CurrentMenuSelection == ArrowSelection.MainMenu)
                    {
                        menuSelectionArrowRect.Y = gameOverOptions[0].Position.Y;
                        if (SingleKeyPress(Keys.Down))
                        {
                            currentMenu.CurrentMenuSelection = ArrowSelection.Quit;
                        }
                        if (SingleKeyPress(Keys.Enter))//Changes gameStates from GameOver to Menu and menuState from GameOverMenu to MainMenu 
                        {
                            gameState = GameStates.Menu;
                            menuState = MenuStates.MainMenu;

                            playerCharacter = new MainCharacter(mainCharacterStartingX, mainCharacterStartingY, mainCharacterStartingRect, mainTexture, 1, 10, 100, chain, fire);
                            firstEnemy = new Ent((GraphicsDevice.Viewport.Width / 3) * 2 + 50, firstEnemyStartingY, firstEnemyStartingRect, entTexture, 1, 5, 100, firstEnemyAttackRangeRect);
                            firstEnemy.AtkRanRect = new Rectangle(firstEnemy.X - 10, firstEnemy.Y-10, firstEnemy.CoRect.Width + 20, firstEnemy.CoRect.Height + 20);

                            //level new level parameter(ent)
                            currentFightScene = firstLevel.Encounter();
                            firstLevel = new Level(firstEnemy);
                            gameOver = false;
                            view.State = ViewStates.Stationary;
                        }
                    }
                    //Quit
                    else if (currentMenu.CurrentMenuSelection == ArrowSelection.Quit)
                    {
                        menuSelectionArrowRect.Y = gameOverOptions[1].Position.Y;
                        if (SingleKeyPress(Keys.Up))
                        {
                            currentMenu.CurrentMenuSelection = ArrowSelection.MainMenu;
                        }
                        if (SingleKeyPress(Keys.Enter))//Exits
                        {
                            this.Exit();
                        }
                    }
                    break;
                #endregion
            }

            currentFightScene.UpdateList();

            if (playerCharacter.HP < 1)
            {
                gameOver = true;
            }
            if (gameOver)
            {
                gameState = GameStates.GameOver;
                menuState = MenuStates.GameOverMenu;
            }

            int i = 0;
            //foreach (Enemy enemy in currentFightScene.Enemies)
            for (int q = 0; q < currentFightScene.Enemies.Count; q++)
            {
                Console.WriteLine(i + ": X - " + currentFightScene.Enemies[q].X + "; Y - " + currentFightScene.Enemies[q].Y);
                i++;
            }

            entities.Clear();
            entities.Add(playerCharacter);
            entities.Add(chain);
            entities.Add(fire);

            if (currentFightScene.Enemies.Count > 0)
            {
                //foreach (Enemy enemy in currentFightScene.Enemies)
                for (int q = 0; q < currentFightScene.Enemies.Count; q++ )
                {
                    //currentFightScene.Enemies[q].Move(playerCharacter);
                    entities.Add(currentFightScene.Enemies[q]);
                }
            }
            else
            {
                if (firstLevel.EnemySceneQueue.Count > 0)
                {
                    view.State = ViewStates.Moving;
                }
                else
                {
                    gameOver = true;
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            spriteBatch.Begin();

            view.DrawBackground(spriteBatch, groundTexture, backgroundTexture, 0, mainCharacterStartingY + mainCharacterStartingRect.Height, firstLevel, playerCharacter, GraphicsDevice, kbState);
 
            view.DrawEntities(spriteBatch, entities);

            view.DrawOverlay(spriteBatch, arial, playerCharacter.HP.ToString());

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
