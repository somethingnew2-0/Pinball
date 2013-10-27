using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;

namespace Pinball
{
    public class LeftFlipperBumper : GameObject
    {
        //FixedRevoluteJoint revoluteJoint;
        //FixedLinearSpring fixedLinearSpring;

        //public float DampningConstant { get; set; }

        //public float SpringConstant { get; set; }

        private static Texture2D texture;

        private bool hit = false;
        private float elapsedDraw = 0f;

        public LeftFlipperBumper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//CreateLeftFlipperBumper(), 1f);
            base.Body.IsStatic = true;
            //base.Body.Position = position;

            foreach (Vertices vertices in CreateLeftFlipperBumper())
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
            texture = contentManager.Load<Texture2D>(@"GameTextures\LeftFlipperBumperHit");
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
                /*foreach (Contact contact in contactList)
	            {
                    if ((contact.Position.X > 58 && contact.Position.X < 70) &&
                        contact.Position.Y > 230 && contact.Position.Y < 262)
                    {                            
                        hit = true;

                        World.Score += (25 * World.Multiplier);

                        World.Message = "Bumper " + (25 * World.Multiplier).ToString();

                        if (GameLogic.JackpotTimeLeft > 0f)
                        {
                            StorageManager.GameDataInstance.JackpotAmount += (25 * World.Multiplier);
                        }

                        linearImpulse = new Vector2((float)Math.Sin(MathHelper.ToRadians(90)), (float)Math.Cos(MathHelper.ToRadians(90))) * 5000;
                        g2.Body.ApplyLinearImpulse(ref linearImpulse);

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

        private List<Vertices> CreateLeftFlipperBumper()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(54, 225));
            vertices.Add(new Vector2(50, 227));
            vertices.Add(new Vector2(50, 242));
            vertices.Add(new Vector2(50, 259));
            vertices.Add(new Vector2(60, 268));
            vertices.Add(new Vector2(68, 273));
            vertices.Add(new Vector2(71, 272));
            vertices.Add(new Vector2(72, 267));
            vertices.Add(new Vector2(68, 257));
            vertices.Add(new Vector2(64, 246));
            vertices.Add(new Vector2(60, 236));
            vertices.Add(new Vector2(54, 225));            
 
 
            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 58, vertices[counter].Y - 251);
            }
            
            return EarclipDecomposer.ConvexPartition(vertices);
        }
    }
}