#region Using Statements
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Pinball
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Gameplay Data

        ///// <summary>
        ///// The primary gameplay object.
        ///// </summary>
        //private World world;

        /// <summary>
        /// The gameOver text.
        /// </summary>
        private string gameOverString = String.Empty;

        /// <summary>
        /// The position of the gameOver text.
        /// </summary>
        private Vector2 gameOverStringPosition;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            //world = new World();

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (MediaPlayer.Queue.ActiveSong == null || MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(SoundManager.MainSong);

                MediaPlayer.IsRepeating = true;
            }

            World.LoadContent(ScreenManager.GraphicsDevice, ScreenManager.Content);

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            base.LoadContent();
        }


        ///// <summary>
        ///// Unload graphics content used by the game.
        ///// </summary>
        //public override void UnloadContent()
        //{
        //    //world.Dispose();
        //}


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //// if something else has canceled our game, then exit
            //if (world == null)
            //{
            //    if (!IsExiting)
            //    {
            //        ExitScreen();
            //    }
            //    base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            //    return;
            //}

            // update the world
            //if (world != null)
            //{
                if (otherScreenHasFocus || coveredByOtherScreen)
                {
                    World.Update(gameTime, true);
                }
                else if (World.GameExited)
                {
                    // TODO: Clear the Farseer engine
                    //GamePhysics.Physics.Clear();
                    //world.Dispose();

                    if (!IsExiting)
                    {
                        ExitScreen();
                    }
                    base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
                    return;
                }
                else
                {
                    World.Update(gameTime, false);
                    // if the game was just won, then build the winner string
                    if (World.GameOver && String.IsNullOrEmpty(gameOverString))
                    {
                        gameOverString =
                            "Game Over\n\nYour Score Was\n\nPress center button\nto continue.";

                        gameOverStringPosition = new Vector2(
                            120, 200);

                        if (MediaPlayer.Queue.ActiveSong != null)
                        {
                            if (MediaPlayer.Queue.ActiveSong.Name == "Songs\\MainLoop" || MediaPlayer.Queue.ActiveSong.Name == "Songs\\BrickBeat")
                            {
                                MediaPlayer.Stop();
                            }
                        }
                            //ScreenManager.GraphicsDevice.Viewport.X +
                            //    ScreenManager.GraphicsDevice.Viewport.Width / 2 -
                            //    (float)Math.Floor(winnerStringSize.X / 2),
                            //ScreenManager.GraphicsDevice.Viewport.Y +
                            //    ScreenManager.GraphicsDevice.Viewport.Height / 2 -
                            //    (float)Math.Floor(winnerStringSize.Y / 2));
                    }
                 }
            //}           
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (!IsExiting)
            {
                if (!World.GameExited)
                {
                    if (input.PauseGame && !World.GameOver)
                    {
                        SoundManager.SoundEffects["SpaceBeep2"].Play();

                        // If they pressed pause, bring up the pause menu screen.
                        ScreenManager.AddScreen(new PauseMenuScreen());
                    }
                    else if (input.MenuSelect && World.GameOver)
                    {
                        World.GameExited = true;
                        //world = null;
                        if (!IsExiting)
                        {
                            ExitScreen();
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            if (StorageManager.GameDataInstance.HighScores == null || World.Score > StorageManager.GameDataInstance.HighScores[i].Number)
                            {
                                //ScreenManager.AddScreen(new BackgroundScreen());
                                //ScreenManager.AddScreen(new NewHighScoresScreen());

                                LoadingScreen.Load(ScreenManager, false, new BackgroundScreen(), new NewHighScoresScreen());

                                //break;

                                return;
                            }
                        }

                        LoadingScreen.Load(ScreenManager, false, new BackgroundScreen(),
                                         new MainMenuScreen());
                    }
                    else if (!World.GameOver)
                    {
                        World.HandleInput(input);
                    }
                } 
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // draw the world
            if (!IsExiting)
            {
                World.Draw(elapsedTime);
            }

            // draw the user-interface elements of the game (scores, etc.)
            DrawHud((float)gameTime.TotalGameTime.TotalSeconds);

            //// If the game is transitioning on or off, fade it out to black.
            //if (ScreenState == ScreenState.TransitionOn && (TransitionPosition > 0))
            //    ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }

        /// <summary>
        /// Draw the user interface elements of the game (scores, etc.).
        /// </summary>
        /// <param name="totatlTime">The amount of elapsed time, in seconds.</param>
        private void DrawHud(float totalTime)
        {
            //if (world != null)
            //{
                ScreenManager.SpriteBatch.Begin();

                //// draw players 0 - 3 at the top of the screen
                //Vector2 position = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width * 0.2f,
                //    ScreenManager.GraphicsDevice.Viewport.Height * 0.1f);
                //for (int i = 0; i < Math.Min(4, networkSession.AllGamers.Count); i++)
                //{
                //    world.DrawPlayerData(totalTime, networkSession.AllGamers[i],
                //        position, ScreenManager.SpriteBatch, false);
                //    position.X += ScreenManager.GraphicsDevice.Viewport.Width * 0.2f;
                //}
                //// draw players 4 - 7 at the bottom of the screen
                //position = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width * 0.2f,
                //    ScreenManager.GraphicsDevice.Viewport.Height * 0.9f);
                //for (int i = 4; i < Math.Min(8, networkSession.AllGamers.Count); i++)
                //{
                //    world.DrawPlayerData(totalTime, networkSession.AllGamers[i],
                //        position, ScreenManager.SpriteBatch, false);
                //    position.X += ScreenManager.GraphicsDevice.Viewport.Width * 0.2f;
                //}
                //// draw players 8 - 11 at the left of the screen
                //position = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width * 0.13f,
                //    ScreenManager.GraphicsDevice.Viewport.Height * 0.2f);
                //for (int i = 8; i < Math.Min(12, networkSession.AllGamers.Count); i++)
                //{
                //    world.DrawPlayerData(totalTime, networkSession.AllGamers[i],
                //        position, ScreenManager.SpriteBatch, false);
                //    position.Y += ScreenManager.GraphicsDevice.Viewport.Height * 0.2f;
                //}
                //// draw players 12 - 15 at the right of the screen
                //position = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width * 0.9f,
                //    ScreenManager.GraphicsDevice.Viewport.Height * 0.2f);
                //for (int i = 12; i < Math.Min(16, networkSession.AllGamers.Count); i++)
                //{
                //    world.DrawPlayerData(totalTime, networkSession.AllGamers[i],
                //        position, ScreenManager.SpriteBatch, false);
                //    position.Y += ScreenManager.GraphicsDevice.Viewport.Height * 0.2f;
                //}

                // if the game is over, draw the winner text
                if (World.GameOver && !String.IsNullOrEmpty(gameOverString))
                {
                    Vector2 gameOverStringSize = World.PlayerFont.MeasureString(gameOverString);


                    ScreenManager.SpriteBatch.DrawString(World.PlayerFont, gameOverString,
                        gameOverStringPosition, new Color(Color.White, base.TransitionAlpha), 0f, new Vector2(gameOverStringSize.X / 2, gameOverStringSize.Y / 2), 1.0f,
                        SpriteEffects.None, 0f);
                }
                ScreenManager.SpriteBatch.End();
            //}
        }


        #endregion
    }
}
