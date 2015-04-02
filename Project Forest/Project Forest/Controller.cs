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

        CreditsMenu credits;
        ControlsMenu controls;
        MainMenu mainMenu;
        List<Keys> mainMenuKeys;
        List<Keys> otherMenuKeys;
        Menu currentMenu;
        ButtonController buttons;

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
        Model model;

        KeyboardState previousKbState;
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
            //firstEnemy = new Ent();
            //chain = new ChainSaw();
            entities = new List<IEntity>();
            view = new View();
            model = new Model();

            //set gamestate to menu state
            gameState = GameStates.Menu;
            menuState = MenuStates.MainMenu;

            mainMenuKeys = new List<Keys>();
            mainMenuKeys.Add(Keys.F1);//controls
            mainMenuKeys.Add(Keys.F2);//credits
            mainMenuKeys.Add(Keys.Enter);//game
            mainMenuKeys.Add(Keys.Escape);//exit

            mainMenu = new MainMenu(mainMenuImage, mainMenuKeys);
            //keys.Clear();

            otherMenuKeys = new List<Keys>();
            otherMenuKeys.Add(Keys.Back);

            controls = new ControlsMenu(controlsMenuImage, otherMenuKeys);
            credits = new CreditsMenu(creditsMenuImage, otherMenuKeys);

            currentMenu = mainMenu;

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

            //loading textures (INSERT BACKGROUND FILE NAME and Menu FILE NAME)
            mainMenuImage = this.Content.Load<Texture2D>("mainmenu");
            controlsMenuImage = this.Content.Load<Texture2D>("controls");
            creditsMenuImage = this.Content.Load<Texture2D>("credits");

            mainMenu.getsetImage = mainMenuImage;
            controls.getsetImage = controlsMenuImage;
            credits.getsetImage = creditsMenuImage;

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
            playerCharacter.Speed = gameTime.ElapsedGameTime.Milliseconds / 3;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKbState = kbState;

            kbState = Keyboard.GetState();

            //setting images based on states
            switch (gameState)
            {
                //if gamestate is menu do these things
                case GameStates.Menu:
                    switch (menuState)
                    {
                        case MenuStates.MainMenu:
                            currentMenu = mainMenu;

                            //if user presses...Enter
                            if (SingleKeyPress(mainMenu.getKeys[2]))
                            {
                                //change state to start state
                                gameState = GameStates.Game;
                            }
                            //if user presses...F1 - controls
                            else if (SingleKeyPress(mainMenu.getKeys[0]))
                            {
                                menuState = MenuStates.Controls;

                            }
                            //if user presses...F2 - credits
                            else if (SingleKeyPress(mainMenu.getKeys[1]))
                            {
                                menuState = MenuStates.Credits;
                            }
                            //if user presses...Escape
                            else if (SingleKeyPress(mainMenu.getKeys[3]))
                            {
                                this.Exit();
                            }
                            break;

                        case MenuStates.Controls:
                            currentMenu = controls;

                            //if user presses...Back
                            if (SingleKeyPress(controls.getKeys[0]))
                            {
                                //set drawing
                                menuState = MenuStates.MainMenu;
                            }
                            break;
                        case MenuStates.Credits:
                            currentMenu = credits;

                            //if user presses...Back
                            if (SingleKeyPress(credits.getKeys[0]))
                            {
                                //set drawing
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
                                if (playerCharacter.X > firstEnemy.X)
                                {
                                    firstEnemy.State = CharacterStates.WalkRight;
                                }
                                if (playerCharacter.X < firstEnemy.X)
                                {
                                    firstEnemy.State = CharacterStates.WalkLeft;
                                }
                                break;
                            case CharacterStates.FaceLeft:
                                if (playerCharacter.X > firstEnemy.X)
                                {
                                    firstEnemy.State = CharacterStates.WalkRight;
                                }
                                if (playerCharacter.X < firstEnemy.X)
                                {
                                    firstEnemy.State = CharacterStates.WalkLeft;
                                }
                                break;
                            case CharacterStates.WalkRight:
                                break;
                            case CharacterStates.WalkLeft:
                                break;
                            case CharacterStates.MeleeAttack:
                                break;
                        }
                    }
                break;
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

            view.Draw(spriteBatch, entities);
            view.DrawMenu(spriteBatch, gameState, currentMenu);

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
