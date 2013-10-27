using System;
using System.Collections.Generic;
using System.Text;

namespace Pinball
{
    public class HighScoresScreen : MenuScreen
    {
        MenuEntry[] entries = new MenuEntry[5];
        MenuEntry backMenuEntry;

        public HighScoresScreen()
            : base("High Scores")
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if (StorageManager.GameDataInstance.HighScores != null)
                {
                    if ((StorageManager.GameDataInstance.HighScores.Count - 1) >= i)
                    {
                        entries[i] = new MenuEntry(StorageManager.GameDataInstance.HighScores[i].Intials + "  " + StorageManager.GameDataInstance.HighScores[i].Number.ToString());  
                    }
                    else
                    {
                        break;
                    }
                }
            }

            backMenuEntry = new MenuEntry("Back");

            backMenuEntry.Selected += OnCancel;

            for (int i = 0; i < entries.Length; i++)
			{
                if (entries[i] != null)
                {
                    MenuEntries.Add(entries[i]); 
                }
                else
                {
                    break;
                }
			}

            MenuEntries.Add(backMenuEntry);
        }

    }

    [Serializable]
    public struct Score
    {
        public String Intials;
        public long Number;
    }
}