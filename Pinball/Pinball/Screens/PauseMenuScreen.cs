#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace Pinball
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class PauseMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public PauseMenuScreen()
            : base("Paused")
        {
            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry musicMenuEntry = new MenuEntry("Music");
            MenuEntry optionGameMenuEnrty = new MenuEntry("Options");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");
            
            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            optionGameMenuEnrty.Selected += OptionGameMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(optionGameMenuEnrty);
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, EventArgs e)
        {
            const string message = "Are you sure you\nwant to quit this\ngame?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            SoundManager.SoundEffects["SpaceBeep2"].Play();

            ScreenManager.AddScreen(confirmQuitMessageBox);
        }

        /// <summary>
        /// Event handler for when the Music menu entry is selected.
        /// </summary>
        void MusicMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["SpaceBeep3"].Play();

            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(new MusicMenuScreen());
        }

        /// <summary>
        /// Event handler for when the option menu entry is selected.
        /// </summary>
        void OptionGameMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["SpaceBeep3"].Play();

            ScreenManager.AddScreen(new BackgroundScreen());
            ScreenManager.AddScreen(new OptionsMenuScreen());
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void ConfirmQuitMessageBoxAccepted(object sender, EventArgs e)
        {
            // TODO: Clear the Farseer engine
            //GamePhysics.Physics.Clear();

            LoadingScreen.Load(ScreenManager, false, new BackgroundScreen(),
                                                     new MainMenuScreen());
        }

        //protected override void OnCancel()
        //{
        //    SoundEffectManager.SoundEffects["Whoosh"].Play();

        //    base.OnCancel();
        //}

        #endregion

        #region Draw


        /// <summary>
        /// Draws the pause menu screen. This darkens down the gameplay screen
        /// that is underneath us, and then chains to the base MenuScreen.Draw.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
            
            base.Draw(gameTime);
        }


        #endregion
    }
}
