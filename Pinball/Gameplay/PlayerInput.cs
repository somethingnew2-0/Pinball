using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;


namespace Pinball
{
    /// <summary>
    /// Player input abstraction for tanks.
    /// </summary>
    public class PlayerInput
    {
        #region Static Constants

        /// <summary>
        /// The empty version of this structure.
        /// </summary>
        private static PlayerInput empty =
            new PlayerInput(new InputState());
        public static PlayerInput Empty
        {
            get { return empty; }
        }
       
        #endregion


        #region Input Data

        public static FlipperStyles CurrentStyle { get; set; }

        public bool LeftHit { get; set; }

        public bool RightHit { get; set; }

        public bool BottomHit { get; set; }
        
        #endregion


        #region Initialization Methods

        public PlayerInput(InputState input)
        {
            LeftHit = false;
            RightHit = false;
            BottomHit = false;


            if (CurrentStyle == FlipperStyles.Touch)
            {
                if (ZunePadState.TouchablePad)
                {
                    if (input.ZunePadState.TouchPosition.X < -0.5)
                    {
                        LeftHit = true;
                    }
                    else if (input.ZunePadState.TouchPosition.X > 0.5)
                    {
                        RightHit = true;
                    }
                    else if (input.ZunePadState.TouchPosition.Y < -0.5)
                    {
                        BottomHit = true;
                    }
                }
                else
                {
                    throw new Exception("The current style shouldn't be set to touch.");
                }
            }
            else if (CurrentStyle == FlipperStyles.DPad)
            {
                if (input.ZunePadState.DPad.Left == ButtonState.Pressed)
                {
                    LeftHit = true;
                }
                else if (input.ZunePadState.DPad.Right == ButtonState.Pressed)
                {
                    RightHit = true;
                }
                else if (input.ZunePadState.DPad.Down == ButtonState.Pressed)
                {
                    BottomHit = true;
                }
            }
            else if (CurrentStyle == FlipperStyles.PlayBack)
            {
                if (input.ZunePadState.BackButton == ButtonState.Pressed) //&&
                    //input.LastZunePadState.BackButton == ButtonState.Released)
                {
                    LeftHit = true;
                }
                if (input.ZunePadState.PlayButton == ButtonState.Pressed) //&&
                    //input.LastZunePadState.PlayButton == ButtonState.Released)
                {
                    RightHit = true;
                }
                if (ZunePadState.TouchablePad)
	            {
                    if (input.ZunePadState.TouchPosition.Y < 0)
                    {
                        BottomHit = true;
                    }
	            }
                if (input.ZunePadState.DPad.Down == ButtonState.Pressed)
                {
                    BottomHit = true;
                }


            }
        }
        #endregion
    }

    [Serializable]
    public enum FlipperStyles
    {
        DPad = 0,
        PlayBack = 1,
        Touch = 2
    }
}
