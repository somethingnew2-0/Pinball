#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Pinball
{
    /// <summary>
    /// Base class for screens that contain a menu of options. The user can
    /// move up and down to select an entry, or cancel to back out of the screen.
    /// </summary>
    public abstract class MenuScreen : GameScreen
    {
        #region Fields

        List<MenuEntry> menuEntries = new List<MenuEntry>();
        protected int SelectedEntry = 0;

        #endregion

        #region Properties


        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }

        public string MenuTitle { get; set; }

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreen(string menuTitle)
        {
            this.MenuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (input.MenuUp)
            {
                SoundManager.SoundEffects["SpaceBeep1"].Play();

                SelectedEntry--;

                if (SelectedEntry < 0)
                    SelectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (input.MenuDown)
            {
                SoundManager.SoundEffects["SpaceBeep1"].Play();

                SelectedEntry++;

                if (SelectedEntry >= menuEntries.Count)
                    SelectedEntry = 0;
            }

            // Accept or cancel the menu?
            if (input.MenuSelect)
            {
                //SoundEffectManager.SoundEffects["Whoosh"].Play();

                OnSelectEntry(SelectedEntry);
            }
            else if (input.MenuCancel)
            {
                OnCancel();
            }
        }


        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex)
        {
            menuEntries[SelectedEntry].OnSelectEntry();
        }

        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel()
        {
            SoundManager.SoundEffects["SpaceBeep2"].Play();

            ExitScreen();
        }


        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, EventArgs e)
        {
            OnCancel();
        }


        #endregion

        #region Draw
        
        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Calculate the total hieght of the menu items
            float tempHeight = 0.0f;
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];
                tempHeight += menuEntry.GetHeight(this);
            }
            // Calculate the New Starting Position for the Menu Items
            // This will draw the menu from the bottom up.
            Vector2 position = new Vector2(45, (ScreenManager.GraphicsDevice.Viewport.Height - 3) - tempHeight);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry menuEntry = menuEntries[i];

                bool isSelected = IsActive && (i == SelectedEntry);

                menuEntry.Draw(this, position, isSelected, gameTime);

                position.Y += menuEntry.GetHeight(this);
            }

            // Draw the menu title.
            Vector2 titlePosition = new Vector2(ScreenManager.GraphicsDevice.Viewport.Width - (font.MeasureString(MenuTitle).X + 50), 10);
            //Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Vector2 titleOrigin = Vector2.Zero;

            Color titleColor = new Color(192, 192, 192, TransitionAlpha);
            float titleScale = 1.05f;

            titlePosition.Y -= transitionOffset * 100;

            Vector2 titleSize = font.MeasureString(MenuTitle);

            if (titleSize.X > 180)
            {
                titleScale = 0.9f;

                titlePosition.X = 20f;
            }
            if (titleSize.X > 200)
            {
                titleScale = 0.8f;

                titlePosition.X = 20f;
            }
            if (titleSize.X > 240)
            {
                titleScale = 0.7f;

                titlePosition.X = 20f;
            }
            if (titleSize.X > 280)
            {
                titleScale = 0.6f;

                titlePosition.X = 20f;
            }

            spriteBatch.DrawString(font, MenuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }


        #endregion
    }
}
