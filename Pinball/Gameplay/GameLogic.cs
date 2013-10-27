using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace Pinball
{
    public class GameLogic
    {
        /// <summary>
        /// The number of arrows needed to be gotten until jackpot is lit.
        /// </summary>
        public const int NumberOfConsecutiveArrowsToJackpot = 3;

        /// <summary>
        /// The number of arrows to have lit at one time, on a normal, not jackpot basis.
        /// </summary>
        public const int NumberOfArrowsToLightAtOneTime = 2;

        /// <summary>
        /// The time left until an arrow is expired.
        /// </summary>
        public static float ArrowTimeLeft { get; set; }

        /// <summary>
        /// The time left until an arrow relights.
        /// </summary>
        public static float ArrowDeadTime { get; set; }

        /// <summary>
        /// The number of consecutive arrows that have been hit without time expiring.
        /// </summary>
        public static int NumberofConsecutiveArrows { get; set; }

        /// <summary>
        /// The time left until Jackpot expires.
        /// </summary>
        public static float JackpotTimeLeft { get; set; }

        ///// <summary>
        ///// The amount in the jackpot.
        ///// </summary>
        //public static long JackpotAmount { get; set; }

        public static void Update(float elaspedTime)
        {
            // Checks for resting ball
            World.Ball.Update(elaspedTime);

            // Updates when the ball goes in a hole
            World.Sensors.Update(elaspedTime);

            // Updates to check if the ball was lost
            World.Border.Update(elaspedTime);

            // Run some of the arrow, jackpot, and multiplier logic
            if (ArrowDeadTime >= 0f)
            {
                ArrowDeadTime -= elaspedTime;
            }
            else
            {
                if (ArrowTimeLeft == 20f)
                {
                    //SoundManager.SoundEffects["SpaceBeep5"].Play();
                    
                    World.Message = "Ramps Activated";

                    PickNewArrowLights(NumberOfArrowsToLightAtOneTime, null);

                    ArrowTimeLeft -= elaspedTime;
                }
                else if (ArrowTimeLeft < 20f || JackpotTimeLeft > 0)
                {

                    for (int i = 0; i < 5; i++)
                    {
                        if (World.Sensors.ArrowHit[i] && World.ArrowLights.PulsateArrow[i])
                        {
                            //World.Sensors.ArrowHit[i] = false;

                            World.ArrowLights.ResetArrowLights();

                            //for (int j = 0; j < 5; j++)
                            //{
                            //    ArrowLights.PulsateArrow[j] = false;
                            //}
                            
                            DoArrowHitLogic(i);

                            if (NumberofConsecutiveArrows == NumberOfConsecutiveArrowsToJackpot)
                            {
                                EnableJackpot();

                                NumberofConsecutiveArrows = 0;
                            }
                        }

                        World.Sensors.ArrowHit[i] = false;
                    }

                    ArrowTimeLeft -= elaspedTime;
                }
                else if (ArrowTimeLeft == 0f)
                {
                    World.Message = "Ramps Timeout";

                    World.ArrowLights.ResetArrowLights();

                    //for (int i = 0; i < 5; i++)
                    //{
                    //    ArrowLights.PulsateArrow[i] = false;
                    //}

                    NumberofConsecutiveArrows = 0;

                    ArrowDeadTime = 10f;
                }
            }

            if (World.MultiplierTimeLeft > 0f)
            {
                World.MultiplierTimeLeft -= elaspedTime;
            }
            else
            {
                World.Multiplier = 1;
            }

            if (JackpotTimeLeft > 0f)
            {
                JackpotTimeLeft -= elaspedTime;
            }
            else
            {
                JackpotTimeLeft = 0f;
            }

            // If the number of balls is 0 set gameover;
            if (World.BallsLeft == 0)
            {
                World.GameOver = true;
            }
        }

        private static void PickNewArrowLights(int numberOfArrowsToLight, Nullable<int> dontPick)
        {
            for (int i = 1; i <= numberOfArrowsToLight; i++)
            {
                int pick = RandomMath.Random.Next(0, 4);

                if (dontPick != null)
                {
                    while (pick == dontPick)
                    {
                        pick = RandomMath.Random.Next(0, 4);
                    }
                }

                World.ArrowLights.PulsateArrow[pick] = true;


                //switch (RandomMath.Random.Next(1, 5))
                //{
                //    case 1:
                //        arrowLights.PulsateArrow1 = true;
                //        break; 
                //    case 2:
                //        arrowLights.PulsateArrow2 = true;
                //        break;
                //    case 3:
                //        arrowLights.PulsateArrow3 = true;
                //        break;
                //    case 4:
                //        arrowLights.PulsateArrow4 = true;
                //        break;
                //    case 5:
                //        arrowLights.PulsateArrow5 = true;
                //        break;
                //}
            }
        }

        private static void DoArrowHitLogic(int arrowHit)
        {
            ArrowTimeLeft = 20f;

            PickNewArrowLights(NumberOfArrowsToLightAtOneTime, arrowHit);

            if (NumberofConsecutiveArrows < NumberOfConsecutiveArrowsToJackpot)
            {
                NumberofConsecutiveArrows++;
            }

            World.Multiplier++;

            World.Message = "Multiplier X" + World.Multiplier.ToString();

            World.MultiplierTimeLeft = 15f;

            if (JackpotTimeLeft <= 0f)
            {
                SoundManager.SoundEffects["SpaceBeep5"].Play();
            }
            else
            {
                if (MediaPlayer.Queue.ActiveSong != null)
                {
                    if (MediaPlayer.Queue.ActiveSong.Name == "Songs\\BrickBeat")
                    {
                        MediaPlayer.Pause();

                        SoundManager.SoundEffects["Jackpot"].Play();

                        World.Message = StorageManager.GameDataInstance.JackpotAmount.ToString() + " Jackpot Awarded!!!";

                        MediaPlayer.Play(SoundManager.MainSong);
                    }
                }

                JackpotTimeLeft = 0f;

                World.Score += StorageManager.GameDataInstance.JackpotAmount;

                StorageManager.GameDataInstance.JackpotAmount = 1000;
            }
        }

        private static void EnableJackpot()
        {
            JackpotTimeLeft = 30f;

            World.Message = "Jackpot Activated";

            if (MediaPlayer.Queue.ActiveSong != null)
            {
                if (MediaPlayer.Queue.ActiveSong.Name == "Songs\\MainLoop")
                {
                    MediaPlayer.Pause();

                    MediaPlayer.Play(SoundManager.SpeedUpSong);
                }
            }

            //PickNewArrowLights(1, null);
        }
    }
}