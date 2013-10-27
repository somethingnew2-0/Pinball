using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;

namespace Pinball
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Fields

        readonly GraphicsDeviceManager graphics;

        readonly ScreenManager screenManager;
        readonly SoundManager soundPlayer;
        readonly StorageManager storageManager;
        readonly World world;

        #endregion

        #region Initialization


        /// <summary>
        /// The main game constructor.
        /// </summary>
        public Game()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;

            // Create the screen manager and sound effect player component
            screenManager = new ScreenManager(this);
            soundPlayer = new SoundManager(this);
            storageManager = new StorageManager(this);
            world = new World(this);

            Components.Add(screenManager);
            Components.Add(soundPlayer);
            Components.Add(storageManager);
            Components.Add(world);
            
            // Load the game data
            StorageManager.LoadGameData();

            // Activate the first screens.
            screenManager.AddScreen(new GameplayScreen());
            //screenManager.AddScreen(new BackgroundScreen());
            //screenManager.AddScreen(new MainMenuScreen());
        }


        #endregion

        #region Draw


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The real drawing happens inside the screen manager component.
            base.Draw(gameTime);
        }

        #endregion
    }
}
