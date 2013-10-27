#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using Microsoft.Xna.Framework.Media;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Common;
#endregion

namespace Pinball
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry controlMenuEntry;
        MenuEntry touchMenuEntry;
        MenuEntry debugMenuEntry;
        MenuEntry musicVolumeMenuEntry;
        MenuEntry soundEffectVolumeMenuEntry;

        //static FlipperStyles currentControl = FlipperStyles.DPad;
        //static bool touch = true;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {                    
            // Create our menu entries.
            //controlMenuEntry = new MenuEntry(string.Empty);
            //touchMenuEntry = new MenuEntry(string.Empty);
            debugMenuEntry = new MenuEntry(string.Empty);
            musicVolumeMenuEntry = new MenuEntry(string.Empty);
            soundEffectVolumeMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            //controlMenuEntry.Selected += ControlMenuEntrySelected;
            //touchMenuEntry.Selected += TouchMenuEntrySelected;
            debugMenuEntry.Selected += DebugMenuEntrySelected;
            musicVolumeMenuEntry.Selected += MusicVolumeMenuEntrySelected;
            soundEffectVolumeMenuEntry.Selected += SoundEffectVolumeMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            //MenuEntries.Add(controlMenuEntry); 
            //if (ZunePadState.TouchablePad)
            //{
            //    MenuEntries.Add(touchMenuEntry);
            //}
            MenuEntries.Add(debugMenuEntry);
            MenuEntries.Add(musicVolumeMenuEntry);
            MenuEntries.Add(soundEffectVolumeMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            //controlMenuEntry.Text = "Control: " + StorageManager.GameDataInstance.ControlOption;
            //touchMenuEntry.Text = "Touch: " + (StorageManager.GameDataInstance.TouchOption ? "on" : "off");
            debugMenuEntry.Text = "Debug View: " + (Settings.EnableDiagnostics ? "on" : "off");
            musicVolumeMenuEntry.Text = "Music Volume: " + Math.Round(MediaPlayer.Volume * 100).ToString() + "%";
            soundEffectVolumeMenuEntry.Text = "Sound Volume: " + Math.Round(SoundManager.Volume * 100).ToString() + "%";
        }

        #endregion

        #region Handle Input

        ///// <summary>
        ///// Event handler for when the control menu entry is selected.
        ///// </summary>
        //void ControlMenuEntrySelected(object sender, EventArgs e)
        //{
        //    StorageManager.GameDataInstance.ControlOption++;

        //    if ((StorageManager.GameDataInstance.ControlOption == FlipperStyles.Touch) && (!ZunePadState.TouchablePad))
        //    {
        //        StorageManager.GameDataInstance.ControlOption = 0;
        //    }

        //    if (StorageManager.GameDataInstance.ControlOption > FlipperStyles.Touch)
        //    {
        //        StorageManager.GameDataInstance.ControlOption = 0;
        //    }

        //    PlayerInput.CurrentStyle = StorageManager.GameDataInstance.ControlOption;

        //    SoundManager.SoundEffects["SpaceBeep3"].Play();

        //    SetMenuEntryText();
        //}


        ///// <summary>
        ///// Event handler for when the touch menu entry is selected.
        ///// </summary> 
        //void TouchMenuEntrySelected(object sender, EventArgs e)
        //{
        //    StorageManager.GameDataInstance.TouchOption = !StorageManager.GameDataInstance.TouchOption;

        //    InputState.Touch = StorageManager.GameDataInstance.TouchOption;

        //    SoundManager.SoundEffects["SpaceBeep3"].Play();

        //    SetMenuEntryText();
        //}

        /// <summary>
        /// Event handler for when the debug menu entry is selected.
        /// </summary>
        void DebugMenuEntrySelected(object sender, EventArgs e)
        {
            Settings.EnableDiagnostics = !Settings.EnableDiagnostics;

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the music volume menu entry is selected.
        /// </summary>
        void MusicVolumeMenuEntrySelected(object sender, EventArgs e)
        {
            if (MediaPlayer.Volume == 1.0f)
            {
                MediaPlayer.Volume = 0f;
            }
            else
            {
                MediaPlayer.Volume += 0.05f;
            }

            StorageManager.GameDataInstance.MusicVolumeOption = MediaPlayer.Volume;

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the sound effect volume menu entry is selected.
        /// </summary>
        void SoundEffectVolumeMenuEntrySelected(object sender, EventArgs e)
        {
            if (SoundManager.Volume == 1.0f)
            {
                SoundManager.Volume = 0f;
            }
            else
            {
                SoundManager.Volume += 0.05f;
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            StorageManager.GameDataInstance.SoundEffectVolumeOption = SoundManager.Volume;

            SetMenuEntryText();
        }
        
        //protected override void OnCancel()
        //{         
        //    if (SelectedEntry == 3)
        //    {
        //        if (MediaPlayer.Volume == 0f)
        //        {
        //            MediaPlayer.Volume = 1.0f;
        //        }
        //        else
        //        {
        //            MediaPlayer.Volume -= 0.05f;
        //        }

        //        StorageManager.GameDataInstance.MusicVolumeOption = MediaPlayer.Volume;

        //        SoundManager.SoundEffects["SpaceBeep3"].Play();

        //        SetMenuEntryText();
        //    }
        //    else if (SelectedEntry == 4)
        //    {
        //        if (SoundManager.Volume == 0f)
        //        {
        //            SoundManager.Volume = 1.0f;
        //        }
        //        else
        //        {
        //            SoundManager.Volume -= 0.05f;
        //        }

        //        StorageManager.GameDataInstance.SoundEffectVolumeOption = SoundManager.Volume;

        //        SoundManager.SoundEffects["SpaceBeep3"].Play();

        //        SetMenuEntryText();
        //    }
        //    else
        //    {
        //        //StorageManager.SaveGameData();

        //        base.OnCancel();
        //    }
        //}

        #endregion

    }
}
