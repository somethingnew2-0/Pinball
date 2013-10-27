using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework.Content;
using FarseerGames.FarseerPhysics.Collisions;

namespace Pinball
{
    public class RightCircleBumper : GameObject
    {
        private static Texture2D texture;
        private bool hit = false;
        private float elapsedDraw = 0f;

        public RightCircleBumper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreateCircleBody(physicsSimulator, 14f, 1f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[1];

            // Inner Bumper Geom
            base.Geom[0] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 14f, 8, 10f);
 
            // base.Geom[0].Tag = GameObjects.Bumper;

            base.Geom[0].CollisionCategories = Enums.CollisionCategories.Cat6;
            base.Geom[0].CollidesWith = Enums.CollisionCategories.Cat2;

            base.Geom[0].Collision += CollisionHandler;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(@"GameTextures\CircleBumperHit");
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            if (hit)
	        {
                if (elapsedDraw < 0.25f)
                {
                    base.Draw(elapsedTime, spriteBatch, texture, Color.White);

                    elapsedDraw += elapsedTime;

                }
                else
                {
                    hit = false;

                    elapsedDraw = 0.0f;
                }
            }
                       
        }

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Bumper))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {

                    hit = true;

                    World.Score += (50 * World.Multiplier);

                    World.Message = "Bumper " + (50 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (50 * World.Multiplier);
                    }

                    SoundManager.SoundEffects["CircleBumper"].Play();

                    //g2.Body.ApplyImpulse((Vector2.Normalize(Vector2.Negate(g2.Body.LinearVelocity)) - new Vector2((float)Math.Sin(base.Body.Rotation), (float)Math.Cos(base.Body.Rotation))) * 8000);

                    g2.Body.ApplyImpulse((Vector2.Normalize(g2.Body.LinearVelocity) * 3000));

                    return true;
                }
                else
                {
                    return false;
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 isn't a bumper geom.");
            //}
        }

    }
}