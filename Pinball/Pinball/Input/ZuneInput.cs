using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pinball
{
    public struct ZunePadState
    {
        GamePadState state;
		Vector2 flick;
        bool tapped;

        /// <summary>
        /// Has the pad been touched or pressed at all?
        /// </summary>
        public bool IsTouchedorPressed
        {
            get
            { 
                if (TouchablePad)
	            {
                     return state.Buttons.LeftStick == ButtonState.Pressed;
	            }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Has the pad been tapped?
        /// </summary>
        public bool IsTapped
        {
            get { return tapped; }
        }

        //tells if the zune's pad has been clicked at any point
        /// <summary>
        /// Has the pad been clicked anywhere?
        /// </summary>
        public ButtonState IsClickedAnywhere
        {
            get { return state.Buttons.LeftShoulder; }
        }

        /// <summary>
        /// Tells if this zune has a touchable pad.
        /// </summary>
        public static bool TouchablePad
        {
            get
            {
                return GamePad.GetCapabilities(PlayerIndex.One).HasLeftXThumbStick;
            }
        }

        /// <summary>
        /// Tells if this zune has a flickable pad (Same as TouchablePad). 
        /// </summary>
        public static bool FlickablePad
        { 
            get
            {
                return TouchablePad;   
            }
        }

        public static bool ClickableCenterPad
        {
            get
            {
                return GamePad.GetCapabilities(PlayerIndex.One).HasAButton;
            }
        }

        /// <summary>
        /// Was the pad flicked?
        /// </summary>
        public Vector2 Flick
        {
            get
            {
                if (tapped == true)
                {
                    return Vector2.Zero;
                }
                else
	            {
                    return flick; 
	            }                          
            }
        }

        /// <summary>
        /// The position the finger is on the pad (thumbstick).
        /// </summary>
        public Vector2 TouchPosition
        {
            get
            {
                if (TouchablePad)
                {
                    return state.ThumbSticks.Left;
                }
                else
                {
                    return Vector2.Zero;
                }
            }
        }

        /// <summary>
        /// The back button.
        /// </summary>
        public ButtonState BackButton
        {
            get { return state.Buttons.Back; }
        }

        /// <summary>
        /// The play button (B).
        /// </summary>
        public ButtonState PlayButton
        {
            get { return state.Buttons.B; }
        }
        
        //used when the zune's pad is clicked in the center
        /// <summary>
        /// The center of the pad clicked (A).
        /// </summary>
        public ButtonState PadCenterPressed
        {
            get
            {
                if (ClickableCenterPad)
                {
                    return state.Buttons.A; 
                }
                else
                {
                    return ButtonState.Released;
                }
            }
        }

        /// <summary>
        /// The pad with only 4 directions.
        /// </summary>
        public GamePadDPad DPad
        {
            get { return state.DPad; }
        }

        public ZunePadState(GamePadState state, Vector2 flick, bool tapped)
        {
            this.state = state;
            this.flick = flick;
            this.tapped = tapped;
        }
    }

    public static class ZunePad
    {
        static ZunePadState zps;

        static float flickStartTime;
        static Vector2 flickStart;

        /// <summary>
        /// Gets the state of the Zune input allowing for flicks and taps.
        /// </summary>
        /// <param name="gameTime">The current time snapshot</param>
        /// <returns>The new ZunePadState object.</returns>
        public static ZunePadState GetState(GameTime gameTime)
        {
            GamePadState gps = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);
            Vector2 flick = Vector2.Zero;
            bool tapped = false;

            if (GamePad.GetCapabilities(PlayerIndex.One).HasLeftXThumbStick)
            {
                if (gps.Buttons.LeftStick == ButtonState.Pressed && !zps.IsTouchedorPressed)
                {
                    flickStart = gps.ThumbSticks.Left;
                    flickStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }
                else if (gps.Buttons.LeftStick == ButtonState.Released && zps.IsTouchedorPressed)
                {
                    flick = zps.TouchPosition - flickStart;
                    float elapsed = (float)(gameTime.TotalGameTime.TotalSeconds - flickStartTime);

                    //scale the flick based on how long it took
                    flick /= (float)elapsed;

                    //adjust the .5 and .3 to fit your sensitivity needs. .5 and .3 seem
                    //to be pretty decent, but they might need tweaking for some situations
                    tapped = (flick.Length() < .5f && elapsed < .3f);

                    flickStart = Vector2.Zero;
                } 
            }

            zps = new ZunePadState(gps, flick, tapped);

            return zps;
        }

        /// <summary>
        /// Gets the state of the Zune input without flicks or taps.
        /// </summary>
        /// <returns>The new ZunePadState object.</returns>
        public static ZunePadState GetState()
        {
            GamePadState gps = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

            zps = new ZunePadState(gps, Vector2.Zero, false);

            return zps;
        }
    } 
}