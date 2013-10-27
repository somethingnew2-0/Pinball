#region Using Statements
using System;
#endregion

namespace Pinball
{
    /// <summary>
    /// The main menu screen is the first thing displayed when the game starts up.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        #region Initialization


        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("")
        {
            TransitionOffTime = TimeSpan.Zero;

            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Play");
            MenuEntry musicMenuEntry = new MenuEntry("Music");
            MenuEntry highScoresMenuEntry = new MenuEntry("High Scores");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            musicMenuEntry.Selected += MusicMenuEntrySelected;
            highScoresMenuEntry.Selected += HighScoresMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(musicMenuEntry);
            MenuEntries.Add(highScoresMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["Whoosh"].Play();

            LoadingScreen.Load(ScreenManager, true, new GameplayScreen());
        }

        /// <summary>
        /// Event handler for when the Music menu entry is selected.
        /// </summary>
        void MusicMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["SpaceBeep3"].Play();

            ScreenManager.AddScreen(new MusicMenuScreen());
        }

        /// <summary>
        /// Event handler for when the High Scores menu entry is selected.
        /// </summary>
        void HighScoresMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["SpaceBeep3"].Play();

            ScreenManager.AddScreen(new HighScoresScreen());
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, EventArgs e)
        {
            SoundManager.SoundEffects["SpaceBeep3"].Play();

            ScreenManager.AddScreen(new OptionsMenuScreen());
        }


        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel()
        {
            const string message = "Are you sure you\nwant to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            SoundManager.SoundEffects["SpaceBeep2"].Play();

            ScreenManager.AddScreen(confirmExitMessageBox);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, EventArgs e)
        {
            StorageManager.SaveGameData();

            ScreenManager.Game.Exit();
        }


        #endregion
    }
}
