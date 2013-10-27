#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Pinball
{
    /// <summary>
    /// Helper for reading input from keyboard and gamepad. This class tracks both
    /// the current and previous state of both input devices, and implements query
    /// properties for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {

        #region Properties
        
        public ZunePadState ZunePadState { get; set; }
        public ZunePadState LastZunePadState { get; set; }

        public static bool Touch { get; set; }

        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            // TODO: Load from Xml
            Touch = true;

            ZunePadState = ZunePad.GetState();
        }


        #endregion

        #region Properties


        /// <summary>
        /// Checks for a "menu up" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuUp
        {
            get
            {
                return (Touch ? (ZunePadState.Flick.Y > 0) : (ZunePadState.DPad.Up == ButtonState.Pressed &&
                    LastZunePadState.DPad.Up == ButtonState.Released));
            }
        }


        /// <summary>
        /// Checks for a "menu down" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuDown
        {
            get
            {
                return (Touch ? (ZunePadState.Flick.Y < 0) : (ZunePadState.DPad.Down == ButtonState.Pressed &&
                    LastZunePadState.DPad.Down == ButtonState.Released));
            }
        }


        /// <summary>
        /// Checks for a "menu select" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuSelect
        {
            get
            {
                return ((ZunePadState.PlayButton == ButtonState.Pressed &&
                    LastZunePadState.PlayButton == ButtonState.Released) || (
                    ZunePadState.PadCenterPressed == ButtonState.Pressed &&
                    LastZunePadState.PadCenterPressed == ButtonState.Released));
            }
        }


        /// <summary>
        /// Checks for a "menu cancel" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuCancel
        {
            get
            {
                return (ZunePadState.BackButton == ButtonState.Pressed &&
                    LastZunePadState.BackButton == ButtonState.Released);
            }
        }


        /// <summary>
        /// Checks for a "pause the game" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool PauseGame
        {
            get
            {
                if (PlayerInput.CurrentStyle == FlipperStyles.PlayBack)
                {
                    if (ZunePadState.ClickableCenterPad)
                    {
                        return (ZunePadState.PadCenterPressed == ButtonState.Pressed &&
                            LastZunePadState.PadCenterPressed == ButtonState.Released);
                    }
                    else
                    {
                        return (//(ZunePadState.DPad.Up == ButtonState.Pressed && LastZunePadState.DPad.Up == ButtonState.Released) ||
                            //(ZunePadState.DPad.Down == ButtonState.Pressed && LastZunePadState.DPad.Down == ButtonState.Released) || So the plunger can work.
                            (ZunePadState.DPad.Left == ButtonState.Pressed && LastZunePadState.DPad.Left == ButtonState.Released) ||
                            (ZunePadState.DPad.Right == ButtonState.Pressed && LastZunePadState.DPad.Right == ButtonState.Released));
                    }
                }
                else
                {
                    return (ZunePadState.BackButton == ButtonState.Pressed &&
                    LastZunePadState.BackButton == ButtonState.Released);
                } 
            }
        }


        #endregion

        #region Methods

        
        /// <summary>
        /// Reads the latest state of the keyboard and gamepad.
        /// </summary>
        /// <param name="gameTime">The gameTime variable</param>
        public void Update(GameTime gameTime)
        {
            LastZunePadState = ZunePadState;
            ZunePadState = ZunePad.GetState(gameTime);
        }


        /// <summary>
        /// Checks for a "menu select" input action from the specified player.
        /// </summary>
        public bool IsMenuSelect()
        {
            return ((ZunePadState.PlayButton == ButtonState.Pressed &&
                    LastZunePadState.PlayButton == ButtonState.Released) || (
                    ZunePadState.PadCenterPressed == ButtonState.Pressed &&
                    LastZunePadState.PadCenterPressed == ButtonState.Released));
        }


        /// <summary>
        /// Checks for a "menu cancel" input action from the specified player.
        /// </summary>
        public bool IsMenuCancel()
        {
            return (ZunePadState.BackButton == ButtonState.Pressed &&
                    LastZunePadState.BackButton == ButtonState.Released);
        }

        #endregion
    }
}
