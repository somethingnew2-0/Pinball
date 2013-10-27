using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Pinball
{
    public class ArrowLights : GameObject
    {
        private static Texture2D[] textures = new Texture2D[5];

        public bool[] PulsateArrow { get; set; }

        //public bool PulsateArrow1 { get; set; }

        //public bool PulsateArrow2 { get; set; }

        //public bool PulsateArrow3 { get; set; }

        //public bool PulsateArrow4 { get; set; }

        //public bool PulsateArrow5 { get; set; }

        private float time1 = 0f;
        private float time2 = 0f;
        private float time3 = 0f;
        private float time4 = 0f;
        private float time5 = 0f;

        public ArrowLights()
        {
            PulsateArrow = new Boolean[5];

            for (int i = 0; i < 5; i++)
            {
                PulsateArrow[i] = false;
            }
        }

        public static void LoadContent(ContentManager contentManager)
        {
            for (int counter = 1; counter <= 5; counter++)
            {
                textures[counter - 1] = (contentManager.Load<Texture2D>(@"GameTextures\Arrow" + counter.ToString()));
            }
        }

        public void Draw(float elaspedTime, SpriteBatch spriteBatch)
        {


            if (PulsateArrow[0])
            {
                spriteBatch.Draw(textures[0], new Vector2(32f, 130f), null,
                    new Color((byte)255, (byte)255, (byte)255, (byte)((float)Math.Abs(Math.Sin(time1 * 3) * 255))),
                    0f, new Vector2(textures[0].Width / 2, textures[0].Height / 2), 1f, SpriteEffects.None, 0f);

                time1 += elaspedTime;
            }
            else
            {
                time1 = 0f;
            }

            if (PulsateArrow[1])
            {

                spriteBatch.Draw(textures[1], new Vector2(49f, 111f), null,
                    new Color((byte)255, (byte)255, (byte)255, (byte)((float)Math.Abs(Math.Sin(time2 * 3) * 255))),
                    0f, new Vector2(textures[1].Width / 2, textures[1].Height / 2), 1f, SpriteEffects.None, 0f);


                time2 += elaspedTime;   
            }
            else
            {
                time2 = 0f;
            }

            if (PulsateArrow[2])
            {
                spriteBatch.Draw(textures[2], new Vector2(72f, 80f), null,
                    new Color((byte)255, (byte)255, (byte)255, (byte)((float)Math.Abs(Math.Sin(time3 * 3) * 255))),
                    0f, new Vector2(textures[2].Width / 2, textures[2].Height / 2), 1f, SpriteEffects.None, 0f);

                time3 += elaspedTime;
            }
            else
            {
                time3 = 0f;
            }

            if (PulsateArrow[3])
            {

                spriteBatch.Draw(textures[3], new Vector2(122f, 103f), null,
                    new Color((byte)255, (byte)255, (byte)255, (byte)((float)Math.Abs(Math.Sin(time4 * 3) * 255))),
                    0f, new Vector2(textures[3].Width / 2, textures[3].Height / 2), 1f, SpriteEffects.None, 0f);   


                time4 += elaspedTime;
            }
            else
            {
                time4 = 0f;
            }

            if (PulsateArrow[4])
            {
                 spriteBatch.Draw(textures[4], new Vector2(179f, 146f), null,
                    new Color((byte)255, (byte)255, (byte)255, (byte)((float)Math.Abs(Math.Sin(time5 * 3) * 255))),
                    0f, new Vector2(textures[4].Width / 2, textures[4].Height / 2), 1f, SpriteEffects.None, 0f);

                time5 += elaspedTime;
            }
            else
            {
                time5 = 0f;
            }

        }

        public void ResetArrowLights()
        {
            for (int i = 0; i < 5; i++)
            {
                PulsateArrow[i] = false;
            }

            time1 = 0f;
            time2 = 0f;
            time3 = 0f;
            time4 = 0f;
            time5 = 0f;
        }
    }
}