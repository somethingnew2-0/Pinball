using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace Pinball
{
    class MusicMenuScreen : MenuScreen
    {
        //MenuEntry nowPlayingMenuEntry;
        MenuEntry playlistMenuEntry;
        MenuEntry prevSongMenuEntry;
        MenuEntry playpauseSongMenuEntry;
        MenuEntry nextSongMenuEntry;

        private static int playlistSelection = 0;
        private PlaylistCollection playlists;
        
        private static SongCollection songCollection;
        private static int songIndex = 0;

        public MusicMenuScreen()
            : base(string.Empty)
        {

            //player = ScreenManager.Game.Services.GetService(typeof(MusicPlayerManager)) as MusicPlayerManager;

            MediaLibrary library = new MediaLibrary();
            playlists = library.Playlists;

            // Create our menu entries.
            //nowPlayingMenuEntry = new MenuEntry(string.Empty);
            playlistMenuEntry = new MenuEntry(string.Empty);
            prevSongMenuEntry = new MenuEntry("Previous");
            playpauseSongMenuEntry = new MenuEntry(string.Empty);
            nextSongMenuEntry = new MenuEntry("Next");

            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.
            playlistMenuEntry.Selected += PlaylistMenuEntrySelected;
            prevSongMenuEntry.Selected += PrevSongMenuEntrySelected;
            playpauseSongMenuEntry.Selected += PlayPauseSongMenuEntrySelected;
            nextSongMenuEntry.Selected += NextSongMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            //MenuEntries.Add(nowPlayingMenuEntry);
            MenuEntries.Add(playlistMenuEntry);
            MenuEntries.Add(prevSongMenuEntry);
            MenuEntries.Add(playpauseSongMenuEntry);
            MenuEntries.Add(nextSongMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        /// <summary>
        /// Fills in the latest values for the music screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            if (MediaPlayer.Queue.ActiveSong != null && MediaPlayer.State == MediaState.Playing)
            {
                //nowPlayingMenuEntry.Text = MediaPlayer.Queue.ActiveSong.Name.ToString();
                base.MenuTitle = MediaPlayer.Queue.ActiveSong.Name.ToString();
            }
            else
            {
                //nowPlayingMenuEntry.Text = "No Song Playing";
                base.MenuTitle = "No Song Playing";
            }


            playlistMenuEntry.Text = "Playlist: " + playlists[playlistSelection].Name.ToString();

            if (MediaPlayer.State == MediaState.Playing)
	        {
                playpauseSongMenuEntry.Text = "Stop";
	        }
            else
            {
                playpauseSongMenuEntry.Text = "Play";
            }
        }

        /// <summary>
        /// Event handler for when the playlist menu entry is selected.
        /// </summary>
        void PlaylistMenuEntrySelected(object sender, EventArgs e)
        {
            playlistSelection--;
            if (playlistSelection < 0)
                playlistSelection = playlists.Count - 1;

            songCollection = null;

            songCollection = playlists[playlistSelection].Songs;

            songIndex = 0;

            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Play(songCollection);
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the prev song menu entry is selected.
        /// </summary>
        void PrevSongMenuEntrySelected(object sender, EventArgs e)
        {
            MediaPlayer.MovePrevious(); ;

            songIndex = MediaPlayer.Queue.ActiveSongIndex;

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the play pause menu entry is selected.
        /// </summary>
        void PlayPauseSongMenuEntrySelected(object sender, EventArgs e)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else
            {
                try
                {
                    MediaPlayer.Play(songCollection, songIndex);
                }
                catch (ArgumentNullException)
                {
                    songCollection = playlists[playlistSelection].Songs;

                    songIndex = 0;

                    MediaPlayer.Play(songCollection);
                }
            }

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }

        /// <summary>
        /// Event handler for when the next song menu entry is selected.
        /// </summary>
        void NextSongMenuEntrySelected(object sender, EventArgs e)
        {
            MediaPlayer.MoveNext();

            songIndex = MediaPlayer.Queue.ActiveSongIndex;

            SoundManager.SoundEffects["SpaceBeep3"].Play();

            SetMenuEntryText();
        }
    }
}