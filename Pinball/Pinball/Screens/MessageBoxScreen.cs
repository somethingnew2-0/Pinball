#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Pinball
{
    /// <summary>
    /// A popup message box screen, used to display "are you sure?"
    /// confirmation messages.
    /// </summary>
    class MessageBoxScreen : GameScreen
    {
        #region Fields

        string message;

        string usageTextOk = "Play Button = Ok";
        string usageTextCancel = "Back Button = Cancel";

        bool includeUsageTextOk = false;
        bool includeUsageTextCancel = false;

        #endregion

        #region Events

        public event EventHandler<EventArgs> Accepted;
        public event EventHandler<EventArgs> Cancelled;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, true)
        { 
            
        }


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            if (includeUsageText)
            {
                this.message = message;
                this.includeUsageTextOk = true;
                this.includeUsageTextOk = true;

                //if (ZunePadState.ClickableCenterPad)
                //{
                    usageTextOk = "Play Button  or  Press\n  Center Pad = Ok";
                //}
            }
            else
            {
                this.includeUsageTextCancel = false;
                this.includeUsageTextOk = false;

                this.message = message;
            }

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public MessageBoxScreen(string message, bool includeUsageOk, bool includeUsageCancel)
        {

            this.message = message;

            if (includeUsageOk)
            {
                this.includeUsageTextOk = true;

                //if (ZunePadState.ClickableCenterPad)
                //{
                //    usageTextOk = "Play Button  or  Press\n  Center Pad = Ok";
                //}
            }
            else
            {
                this.includeUsageTextOk = false;
            }

            if (includeUsageCancel)
            {
                this.includeUsageTextCancel = true;
            }
            else
            {
                this.includeUsageTextCancel = false;
            }

            this.includeUsageTextCancel = includeUsageCancel;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            PlayerIndex playerIndex;
            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                // Raise the accepted event, then exit the message box.
                if (Accepted != null)
                    Accepted(this, EventArgs.Empty);

                SoundManager.SoundEffects["SpaceBeep3"].Play();

                ExitScreen();
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                // Raise the cancelled event, then exit the message box.
                if (Cancelled != null)
                    Cancelled(this, EventArgs.Empty);

                SoundManager.SoundEffects["SpaceBeep2"].Play();

                ExitScreen();
            }
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            //ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            //Usage Text
            // Calculate the hieght of the text
            

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = new Vector2(10, 20);

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = new Color(255, 255, 255, TransitionAlpha);

            spriteBatch.Begin();

            // Draw the message box text.
            spriteBatch.DrawString(font, 
                wordwrap(ScreenManager.GraphicsDevice.Viewport.Width,message,font), 
                textPosition, color);

            // Draw Usage Text
            if (this.includeUsageTextOk || this.includeUsageTextCancel)
            {
                // Display both Usage Lines
                spriteBatch.DrawString(font, usageTextOk, new Vector2(10, 250), color);
                spriteBatch.DrawString(font, usageTextCancel, new Vector2(10, 300), color);
            }
            else if (this.includeUsageTextOk)
            {
                // Display only the Ok Usage Line
                spriteBatch.DrawString(font, usageTextOk, new Vector2(10, 270), color);
            }

            spriteBatch.End();
        }


        #endregion

        public string wordwrap(int width, String in_string, SpriteFont in_font)
        {
            int x;
            String current_line = "";
            String current_word = "";
            String new_string = "";
            for (x = 0; x < in_string.Length; x++)
            {
                if (in_string[x].CompareTo(' ') == 0)
                {
                    if (in_font.MeasureString(current_word).X + in_font.MeasureString(current_line + " ").X > width)
                    {
                        new_string = new_string + current_line + "\n";
                        current_line = current_word + " ";
                        current_word = "";
                    }
                    else
                    {
                        if (current_line.Length > 0)
                        {
                            current_line = current_line + " " + current_word;
                            current_word = "";
                        }
                        else
                        {
                            current_line = current_word;
                            current_word = "";
                        }
                    }
                }
                else
                {
                    current_word = current_word + in_string[x];
                }
            }
            new_string = new_string + current_line + " " + current_word;
            return new_string;
        }

    }
}
