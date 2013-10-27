using System;
using System.Collections.Generic;
using System.Text;

namespace Pinball
{
    public class NewHighScoresScreen : MenuScreen
    {
        MenuEntry firstIntialMenuEntry;
        MenuEntry middleIntialMenuEntry;
        MenuEntry lastIntialMenuEntry;
        MenuEntry doneMenuEntry;

        char[] alphabetArray = "_ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        int firstIndex = 0;
        int middleIndex = 0;
        int lastIndex = 0;

        public NewHighScoresScreen()
            : base("New High Score\nEnter your intials.")
        {
            // Create our menu entries.
            firstIntialMenuEntry = new MenuEntry("_");
            middleIntialMenuEntry = new MenuEntry(" _");
            lastIntialMenuEntry = new MenuEntry("  _");

            SetMenuEntryText();

            doneMenuEntry = new MenuEntry("Done");

            // Hook up menu event handlers.
            firstIntialMenuEntry.Selected += FirstIntialMenuEntrySelected;
            middleIntialMenuEntry.Selected += MiddleIntialMenuEntrySelected;
            lastIntialMenuEntry.Selected += LastIntialMenuEntrySelected;
            doneMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(firstIntialMenuEntry);
            MenuEntries.Add(middleIntialMenuEntry);
            MenuEntries.Add(lastIntialMenuEntry);
            MenuEntries.Add(doneMenuEntry);
        }
        
        /// <summary>
        /// Fills in the latest values for the music screen menu text.
        /// </summary>
        private void SetMenuEntryText()
        {
            firstIntialMenuEntry.Text = alphabetArray[firstIndex].ToString();

            middleIntialMenuEntry.Text = alphabetArray[middleIndex].ToString();

            lastIntialMenuEntry.Text = alphabetArray[lastIndex].ToString();
        }

        /// <summary>
        /// Event handler for when the first intial menu entry is selected.
        /// </summary>
        void FirstIntialMenuEntrySelected(object sender, EventArgs e)
        {
            firstIndex++;

            if (firstIndex > 26)
            {
                firstIndex = 0;
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the middle intial menu entry is selected.
        /// </summary>
        void MiddleIntialMenuEntrySelected(object sender, EventArgs e)
        {
            middleIndex++;

            if (middleIndex > 26)
            {
                middleIndex = 0;
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the last intial menu entry is selected.
        /// </summary>
        void LastIntialMenuEntrySelected(object sender, EventArgs e)
        {
            lastIndex++;

            if (lastIndex > 26)
            {
                lastIndex = 0;
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        //protected override void OnCancel()
        //{
        //    SoundManager.SoundEffects["SpaceBeep3"].Play();

        //    if (SelectedEntry == 0)
        //    {
        //        firstIndex--;

        //        if (firstIndex < 0)
        //        {
        //            firstIndex = alphabetArray.GetLength(0) - 1;
        //        }

        //        SetMenuEntryText();
        //    }
        //    else if (SelectedEntry == 1)
        //    {
        //        middleIndex--;

        //        if (middleIndex < 0)
        //        {
        //            middleIndex = alphabetArray.GetLength(0) - 1;
        //        }

        //        SetMenuEntryText();
        //    }
        //    else if (SelectedEntry == 2)
        //    {
        //        lastIndex--;

        //        if (lastIndex < 0)
        //        {
        //            lastIndex = alphabetArray.GetLength(0) - 1;
        //        }

        //        SetMenuEntryText();
        //    }
        //    else if (SelectedEntry == 3)             
        //    {
        //            //try
        //            //{
        //        if (StorageManager.GameDataInstance.HighScores != null)
        //        {
        //            for (int i = 0; i < 5; i++)
        //            {                            
        //                    if (StorageManager.GameDataInstance.HighScores[i].Equals(null))
        //                    {
        //                        Score score = new Score();
        //                        score.Number = World.Score;
        //                        score.Intials = firstIntialMenuEntry.Text + middleIntialMenuEntry.Text + lastIntialMenuEntry.Text;
                                
        //                        StorageManager.GameDataInstance.HighScores.Add(score);
        //                    }
        //                    else
        //                    {
        //                        Score tempScore = StorageManager.GameDataInstance.HighScores[i];

        //                        if (World.Score > tempScore.Number)
        //                        {
        //                            tempScore.Number = World.Score;
        //                            tempScore.Intials = firstIntialMenuEntry.Text + middleIntialMenuEntry.Text + lastIntialMenuEntry.Text;

        //                            StorageManager.GameDataInstance.HighScores.Insert(i, tempScore);

        //                            StorageManager.GameDataInstance.HighScores.RemoveAt(StorageManager.GameDataInstance.HighScores.Count - 1);

        //                            //StorageManager.GameDataInstance.HighScores[i] = tempScore;

        //                            break;
        //                        }
        //                    } 

        //                }
        //            }
        //            else
        //            {
        //                StorageManager.GameDataInstance.HighScores = new List<Score>(5);

        //                Score score = new Score();
        //                score.Number = World.Score;
        //                score.Intials = firstIntialMenuEntry.Text + middleIntialMenuEntry.Text + lastIntialMenuEntry.Text;
                        
        //                StorageManager.GameDataInstance.HighScores.Add(score);
        //            }
        //            //}
        //            //catch (Exception)
        //            //{
                        
        //            //}


        //        StorageManager.SaveGameData();

        //        ExitScreen();

        //        LoadingScreen.Load(ScreenManager, false, new BackgroundScreen(),
        //         new MainMenuScreen());
        //    }
        //}
    }
}