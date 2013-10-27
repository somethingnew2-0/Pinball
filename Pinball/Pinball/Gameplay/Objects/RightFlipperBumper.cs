using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;

namespace Pinball
{
    public class RightFlipperBumper : GameObject
    {
        private static Texture2D texture;

        private bool hit = false;
        private float elapsedDraw = 0f;

        public RightFlipperBumper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//CreateRightFlipperBumper(), 1f);
            base.Body.IsStatic = true;
            //base.Body.Position = position;

            foreach (Vertices vertices in CreateRightFlipperBumper())
            {
                // Flipper Bumper Fixture
                base.Fixtures.AddLast(FixtureFactory.CreatePolygon(vertices, 1, base.Body, Vector2.Zero));

                base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat6;
                base.Fixtures.Last.Value.CollidesWith = CollisionCategory.Cat2;

                base.Fixtures.Last.Value.OnCollision += CollisionHandler;
            }
              
        }

        public static void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(@"GameTextures\RightFlipperBumperHit");
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

        private Vector2 linearImpluse = Vector2.Zero;

        public override bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {

                /*foreach (Contact contact in contactList)
	            {
                    if ((contact.Position.X > 158 && contact.Position.X < 170) &&
                        contact.Position.Y > 230 && contact.Position.Y < 262)
                    {
                        linearImpluse = new Vector2((float)Math.Sin(MathHelper.ToRadians(270)), (float)Math.Cos(MathHelper.ToRadians(270))) * 5000;
                        g2.Body.ApplyLinearImpulse(ref linearImpluse);

                        hit = true;

                        World.Score += (25 * World.Multiplier);

                        World.Message = "Bumper " + (25 * World.Multiplier).ToString();

                        if (GameLogic.JackpotTimeLeft > 0f)
                        {
                            StorageManager.GameDataInstance.JackpotAmount += (25 * World.Multiplier);
                        }

                        SoundManager.SoundEffects["FlipperBumper"].Play();

                        break;
                    }
		 
	            }*/

                return true;
            }
            else
            {
                return false;
            }
            
        }

        private List<Vertices> CreateRightFlipperBumper()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(175, 225));
            vertices.Add(new Vector2(172, 227));
            vertices.Add(new Vector2(168, 236));
            vertices.Add(new Vector2(165, 244));
            vertices.Add(new Vector2(162, 253));
            vertices.Add(new Vector2(158, 263));
            vertices.Add(new Vector2(156, 269));
            vertices.Add(new Vector2(157, 272));
            vertices.Add(new Vector2(160, 273));
            vertices.Add(new Vector2(164, 272));
            vertices.Add(new Vector2(172, 265));
            vertices.Add(new Vector2(178, 259));
            vertices.Add(new Vector2(178, 244));
            vertices.Add(new Vector2(178, 228));
            vertices.Add(new Vector2(175, 225));
            
            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 170, vertices[counter].Y - 251);
            }

            return EarclipDecomposer.ConvexPartition(vertices);
        }

    }
}