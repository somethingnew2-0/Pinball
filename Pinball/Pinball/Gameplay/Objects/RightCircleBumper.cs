using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;

namespace Pinball
{
    public class RightCircleBumper : GameObject
    {
        private static Texture2D texture;
        private bool hit = false;
        private float elapsedDraw = 0f;

        public RightCircleBumper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//, 14f, 1f);
            base.Body.IsStatic = true;
            //base.Body.Position = position;
                       
            // Inner Bumper Fixture
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(14, 1, base.Body, Vector2.Zero));//14f, 8, 10f));
 
            base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat6;
            base.Fixtures.Last.Value.CollidesWith = CollisionCategory.Cat2;

            base.Fixtures.Last.Value.OnCollision += CollisionHandler;
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

        private Vector2 linearImpulse = Vector2.Zero;

        public override bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
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

                linearImpulse = (Vector2.Normalize(g2.Body.LinearVelocity) * 3000);
                g2.Body.ApplyLinearImpulse(ref linearImpulse);

                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}