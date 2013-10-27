using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Content;

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
            base.Body = BodyFactory.Instance.CreatePolygonBody(physicsSimulator, CreateLeftFlipperBumper(), 1f);
            base.Body.IsStatic = true;
            base.Body.Position = position;


            base.Geom = new Geom[1];

            base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateLeftFlipperBumper(), 5f);

            //base.Geom[0].Tag = GameObjects.Bumper;

            base.Geom[0].CollisionCategories = Enums.CollisionCategories.Cat6;
            base.Geom[0].CollidesWith = Enums.CollisionCategories.Cat2;

            base.Geom[0].Collision += CollisionHandler;
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

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Bumper))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    foreach (Contact contact in contactList)
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

                            g2.Body.ApplyImpulse(new Vector2((float)Math.Sin(MathHelper.ToRadians(90)), (float)Math.Cos(MathHelper.ToRadians(90))) * 5000);

                            SoundManager.SoundEffects["FlipperBumper"].Play();

                            break;
                        }
		 
	                }
                                      
                    return true;
                }
                else
                {
                    return false;
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 isn't a flipper bumper geom.");
            //}
        }

        private Vertices CreateLeftFlipperBumper()
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
            
            return vertices;
        }
    }
}