#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using PhysicsSimulatorView = FarseerPhysics.DebugViewXNA.DebugViewXNA;
using FarseerPhysics.Common;
#endregion

namespace Pinball
{
    /// <summary>
    /// A container for the game-specific logic and code.
    /// </summary>
    public class World : GameComponent
    {
        #region Physics Data

        private static PhysicsSimulator physicsSimulator;

        // TODO: Delete Later
        private static PhysicsSimulatorView simulatorView;

        private static Matrix projection;

        private static Matrix view = Matrix.Identity;

        //// TODO: Delete Later
        //float ballResetTime = 0f;

        #endregion


        #region Constants


        ///// <summary>
        ///// The number of balls in the game.
        ///// </summary>
        //const int numberOfBalls = 1;

        /// <summary>
        /// The length of time it takes for another power-up to spawn.
        /// </summary>
        //const float maximumPowerUpTimer = 10f;



        #endregion


        #region State Data


        ///// <summary>
        ///// If true, the game has been initialized by receiving a WorldSetup packet.
        ///// </summary>
        //private bool initialized = false;
        //public bool Initialized
        //{
        //    get { return initialized; }
        //}

        /// <summary>
        /// If true, the game is over, and somebody has won.
        /// </summary>
        private static bool gameOver = false;
        public static bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        /// <summary>
        /// If true, the game is over, because the game ended before somebody won.
        /// </summary>
        /// <remarks></remarks>
        private static bool gameExited = false;
        public static bool GameExited
        {
            get { return gameExited; }
            set { gameExited = value; }
        }


        #endregion


        #region Gameplay Data

        /// <summary>
        /// The score for this game.
        /// </summary>
        public static long Score { get; set; }

        /// <summary>
        /// The multiplier for each addtion of score.
        /// </summary>
        public static int Multiplier { get; set; }

        /// <summary>
        /// The time left until the Multiplier is expired.
        /// </summary>
        public static float MultiplierTimeLeft { get; set; }

        /// <summary>
        /// The number of balls left in the game.
        /// </summary>
        public static int BallsLeft { get; set; }
        
        /// <summary>
        /// The border rectangle to the game board.
        /// </summary>
        public static Border Border { get; private set; }

        /// <summary>
        /// The background to the game (all non-rebounding objects.
        /// </summary>
        public static Background GameBackground { get; private set; }

        /// <summary>
        /// The foreground to the game (wireframe).
        /// </summary>
        public static Foreground GameForeground { get; private set; }

        /// <summary>
        /// The underground part of the bridge.
        /// </summary>
        public static Underground Underground { get; private set; }

        /// <summary>
        /// The ball in the game.
        /// </summary>
        public static Ball Ball { get; private set; }

        /// <summary>
        /// The right flipper in the game.
        /// </summary>
        public static RightFlipper RightFlipper { get; private set; }

        /// <summary>
        /// The left flipper in the game.
        /// </summary>
        public static LeftFlipper LeftFlipper { get; private set; }

        /// <summary>
        /// The left bumper above the flipper in the game.
        /// </summary>
        public static LeftFlipperBumper LeftFlipperBumper { get; private set; }

        /// <summary>
        /// The right bumper above the flipper in the game.
        /// </summary>
        public static RightFlipperBumper RightFlipperBumper { get; private set; }

        /// <summary>
        /// The top left circle bumper.
        /// </summary>
        public static LeftCircleBumper LeftCircleBumper { get; private set; }

        /// <summary>
        /// The top right circle bumper.
        /// </summary>
        public static RightCircleBumper RightCircleBumper { get; private set; }

        /// <summary>
        /// The sensors in the game.
        /// </summary>
        public static Sensors Sensors { get; private set; }

        /// <summary>
        /// The plunger.
        /// </summary>
        public static Plunger Plunger { get; private set; }
         
        /// <summary>
        /// The arrow lights in the game.
        /// </summary>
        public static ArrowLights ArrowLights { get; private set; }
        
        private static String message = String.Empty;
        /// <summary>
        /// The message displayed below the score.
        /// </summary>
        public static String Message
        {
            get { return message; }
            set { message = value; elapsedMessageTime = 0f; }
        }

        /// <summary>
        /// The elapsed time for a message.
        /// </summary>
        private static float elapsedMessageTime = 0f;

        /// <summary>
        /// Boolean that is set so a sound effect is only played once
        /// </summary>
        private static bool playOnce = true;

        ///// <summary>
        ///// The current power-up in the game.
        ///// </summary>
        //PowerUp powerUp = null;

        ///// <summary>
        ///// The amount of time left until the next power-up spawns.
        ///// </summary>
        //float powerUpTimer = maximumPowerUpTimer / 2f;

        public static PlayerInput PlayerInput { get; private set; }


        #endregion


        #region Graphics Data


        /// <summary>
        /// The sprite batch used to draw the objects in the world.
        /// </summary>
        private static SpriteBatch spriteBatch;

        /// <summary>
        /// The sprite used to draw the player names.
        /// </summary>
        private static SpriteFont playerFont;
        public static SpriteFont PlayerFont
        {
            get { return playerFont; }
        }


        #endregion


        #region Static Methods

        /// <summary>
        /// Resets the ball along with other settings so it can easily be replayed.
        /// </summary>
        public static void ResetBall()
        {
            // Make the lost ball a normal ball
            Ball.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat2;

            // Set the ball to be at the right spot with no initial velocity
            Ball.Body.Position = new Vector2(217, 273);
            Ball.Body.LinearVelocity = Vector2.Zero;
            Ball.Body.AngularVelocity = 0f;
            
            // Make the Multiplier 1 again.
            World.Multiplier = 1;

            Sensors.ChuteBlockerEnabled = false;
            Sensors.LeftAlleyBlockerEnabled = true;
            Sensors.RightAlleyBlockerEnabled = true;

            for (int i = 0; i < 5; i++)
            {
                ArrowLights.PulsateArrow[i] = false;
            }

            GameLogic.ArrowDeadTime = 10f;
            GameLogic.ArrowTimeLeft = 20f;
            GameLogic.JackpotTimeLeft = 0f;
            GameLogic.NumberofConsecutiveArrows = 0;

            World.Message = "Press Bottom of Pad\nto Launch";
        }

        public static void ResetWorld()
        {
            // reset the game status
            gameOver = false;
            gameExited = false;

            Border.LostBall = false;

            Score = 0;
            BallsLeft = 3;
            
            ResetBall();
        }

        #endregion


        #region Initialization


        /// <summary>
        /// Construct a new World object and initialize the objects
        /// </summary>
        public World(Game game) : base(game)
        {
            // TODO: Set the orignal positions of all the objects

            physicsSimulator = new PhysicsSimulator(new Vector2(0f, 150f));

            //physicsSimulator.Iterations = 75;

            //physicsSimulator.AllowedPenetration = .001f;

            //physicsSimulator.MaxContactsToDetect = 10;

            //physicsSimulator.MaxContactsToResolve = 4;

            // TODO: Delete Later
            simulatorView = new PhysicsSimulatorView(physicsSimulator);

            //PhysicsSimulator.EnableDiagnostics = false;

            Border = new Border(physicsSimulator, 280, 380, 20, new Vector2(120, 180));

            GameBackground = new Background(physicsSimulator, new Vector2(120, 160));

            GameForeground = new Foreground(physicsSimulator, new Vector2(120, 160));

            Underground = new Underground(physicsSimulator, new Vector2(120, 160));

            //for (int i = 0; i < numberOfBalls; i++)
            //{
            Ball = new Ball(physicsSimulator, new Vector2(217, 273));
            //}

            LeftFlipper = new LeftFlipper(physicsSimulator, new Vector2(69, 303));

            RightFlipper = new RightFlipper(physicsSimulator, new Vector2(162, 303));

            LeftFlipperBumper = new LeftFlipperBumper(physicsSimulator, new Vector2(58, 251));

            RightFlipperBumper = new RightFlipperBumper(physicsSimulator, new Vector2(170, 251));

            LeftCircleBumper = new LeftCircleBumper(physicsSimulator, new Vector2(134, 54));

            RightCircleBumper = new RightCircleBumper(physicsSimulator, new Vector2(180, 76));

            Sensors = new Sensors(physicsSimulator, new Vector2(120, 160));

            Plunger = new Plunger(physicsSimulator, new Vector2(217, 298));

            ArrowLights = new ArrowLights();
        }


        /// <summary>
        /// 
        /// Loads the content for a World object.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device used for this game.</param>
        /// <param name="contentManager">The content manager used for this game.</param>
        public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {

            // safety-check the parameters, as they must be valid
            if (graphicsDevice == null)
            {
                throw new ArgumentNullException("graphicsDevice");
            }
            if (contentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // create the spritebatch
            spriteBatch = new SpriteBatch(graphicsDevice);

            // load the font
            playerFont = contentManager.Load<SpriteFont>("Fonts/Font");

            // load the gameplay-object textures
            Background.LoadContent(contentManager);
            Foreground.LoadContent(contentManager);
            Ball.LoadContent(contentManager);
            LeftFlipper.LoadContent(contentManager);
            RightFlipper.LoadContent(contentManager);
            LeftCircleBumper.LoadContent(contentManager);
            RightCircleBumper.LoadContent(contentManager);
            LeftFlipperBumper.LoadContent(contentManager);
            RightFlipperBumper.LoadContent(contentManager);
            Plunger.LoadContent(contentManager);
            ArrowLights.LoadContent(contentManager);
            Sensors.LoadContent(contentManager);
            //SoundEffectManager.LoadContent(contentManager);

            // TODO: Delete Later along with DrawingSystem class and PhysicsSimulatorView class
            //simulatorView.LoadContent(graphicsDevice, contentManager);

            ResetWorld();
        }


        #endregion


        #region Updating Methods


        /// <summary>
        /// Update the world.  Not an override to the Game Component.
        /// </summary>
        /// <param name="gameTime">The current time snapshot.</param>
        /// <param name="paused">If true, the game is paused.</param>
        public static void Update(GameTime gameTime, bool paused)
        {
            if (gameOver)
            {
                //// TODO: Clear the Farseer engine
                //GamePhysics.Physics.Clear();

            }
            else
            {
                //// if the game is in progress, update the state of it
                //if (initialized)
                //{

                    // process the local player's input
                    if (!paused)
                    {
                        if (playOnce)
                        {
                            SoundManager.SoundEffects["Start"].Play();

                            playOnce = false;
                        }
 
                        float elaspedTime = gameTime.ElapsedGameTime.Milliseconds * 0.001f;

                        physicsSimulator.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f,
                                        (1f / 30f)));

                        GameLogic.Update(elaspedTime);

                        elapsedMessageTime += elaspedTime;
                    }
                //}
            }
        }

        public static void HandleInput(InputState input)
        {
            PlayerInput = new PlayerInput(input);

            LeftFlipper.HandleInput(PlayerInput.LeftHit);

            RightFlipper.HandleInput(PlayerInput.RightHit);

            Plunger.HandleInput(PlayerInput.BottomHit);
        }

        #endregion


        #region Drawing Methods

        /// <summary>
        /// Draws the objects in the world.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        public static void Draw(float elapsedTime)
        {
            spriteBatch.Begin();

            GameBackground.Draw(elapsedTime, spriteBatch);

            DrawPlayerData(spriteBatch); 

            ArrowLights.Draw(elapsedTime, spriteBatch);

            LeftCircleBumper.Draw(elapsedTime, spriteBatch);

            RightCircleBumper.Draw(elapsedTime, spriteBatch);

            LeftFlipperBumper.Draw(elapsedTime, spriteBatch);

            RightFlipperBumper.Draw(elapsedTime, spriteBatch);

            Plunger.Draw(elapsedTime, spriteBatch);

            Sensors.Draw(elapsedTime, spriteBatch);

            //foreach (var ball in balls)
            //{
            Ball.Draw(elapsedTime, spriteBatch);
            //}
            
            RightFlipper.Draw(elapsedTime, spriteBatch);

            LeftFlipper.Draw(elapsedTime, spriteBatch);

            // TODO: Delete Later
            if (Settings.EnableDiagnostics)
            {
                float aspect = (float)spriteBatch.GraphicsDevice.Viewport.Width / spriteBatch.GraphicsDevice.Viewport.Width;

                projection = Matrix.CreateOrthographic(40 * aspect, 40, 0, 1);

                simulatorView.RenderDebugData(ref projection, ref view);
            }
            //// draw the asteroids
            //foreach (Asteroid asteroid in asteroids)
            //{
            //    if (asteroid.Active)
            //    {
            //        asteroid.Draw(elapsedTime, spriteBatch);
            //    }
            //}

            //// draw the powerup
            //if ((powerUp != null) && powerUp.Active)
            //{
            //    powerUp.Draw(elapsedTime, spriteBatch);
            //}

            GameForeground.Draw(elapsedTime, spriteBatch);
            
            spriteBatch.End();

        }


        /// <summary>
        /// Draw the specified player's data in the screen - gamertag, etc.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch object used to draw.</param>
        public static void DrawPlayerData(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException("spriteBatch");
            }



            if (!gameOver)
            {
                int yPosition = 309;
                for (int i = 1; i <= BallsLeft; i++)
                {
                    spriteBatch.Draw(Ball.Texture, new Vector2(7, yPosition), null, Color.White, 0f,
                        new Vector2(Ball.Texture.Width / 2, Ball.Texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
                            
                    yPosition -= 17;
                }


                string multiplierString = "X" + World.Multiplier.ToString();


                    //+ "\nX: " + ball.Body.LinearVelocity.X.ToString()
                    //+ "\nY: " + ball.Body.LinearVelocity.Y.ToString();



                spriteBatch.DrawString(playerFont, multiplierString, new Vector2(6, 242), Color.White, 0f,
                    playerFont.MeasureString(multiplierString) / 2, 0.8f, SpriteEffects.None, 0f);


                //spriteBatch.DrawString(playerFont,
                ////    "X: " + Ball.Body.LinearVelocity.X.ToString() + "\nY: " + Ball.Body.LinearVelocity.Y.ToString() + "\nA: " + Ball.Body.AngularVelocity.ToString(),
                //"L A: " + Math.Floor(LeftFlipper.Body.AngularVelocity).ToString() +
                //"\nR A: " + Math.Floor(RightFlipper.Body.AngularVelocity).ToString(),
                //new Vector2(80, 220), Color.White);

                if (elapsedMessageTime < 5f)
                {
                    spriteBatch.DrawString(playerFont, message, new Vector2(114, 227), Color.White, 0f,
                        playerFont.MeasureString(message) / 2,
                        0.55f, SpriteEffects.None, 0f);
                }

                if (GameLogic.JackpotTimeLeft > 0f)
                {
                    spriteBatch.DrawString(playerFont, Math.Round(GameLogic.JackpotTimeLeft, 1).ToString(), new Vector2(114, 251), Color.White, 0f,
                        playerFont.MeasureString(Math.Round(GameLogic.JackpotTimeLeft, 1).ToString()) / 2,
                        0.53f, SpriteEffects.None, 0f);

                    spriteBatch.DrawString(playerFont, StorageManager.GameDataInstance.JackpotAmount.ToString(), new Vector2(114, 239), Color.White, 0f,
                        playerFont.MeasureString(StorageManager.GameDataInstance.JackpotAmount.ToString()) / 2,
                        0.53f, SpriteEffects.None, 0f);                                            
                }



            }

            // draw the score
            string scoreString = World.Score.ToString();

            Vector2 scoreStringSize = playerFont.MeasureString(scoreString);
            Vector2 scoreStringPosition = new Vector2(114,
                185 + (0.9f * scoreStringSize.Y));

            spriteBatch.DrawString(playerFont, scoreString, scoreStringPosition,
                Color.White, 0f, new Vector2(scoreStringSize.X / 2f,
                scoreStringSize.Y / 2f), 1f, SpriteEffects.None, 0f);

        }


        #endregion


    }
}
