using System;
using System.Collections.Generic;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Content;

namespace Pinball
{
    public class RightFlipperBumper : GameObject
    {
        private static Texture2D texture;

        private bool hit = false;
        private float elapsedDraw = 0f;

        public RightFlipperBumper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreatePolygonBody(physicsSimulator, CreateRightFlipperBumper(), 1f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[1];

            // Flipper Bumper Geom
            base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateRightFlipperBumper(), 5f);
            
            // base.Geom[0].Tag = GameObjects.Bumper;

            base.Geom[0].CollisionCategories = Enums.CollisionCategories.Cat6;
            base.Geom[0].CollidesWith = Enums.CollisionCategories.Cat2;

            base.Geom[0].Collision += CollisionHandler;
  
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

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Bumper))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {

                    foreach (Contact contact in contactList)
	                {
                        if ((contact.Position.X > 158 && contact.Position.X < 170) &&
                            contact.Position.Y > 230 && contact.Position.Y < 262)
                        {
                            g2.Body.ApplyImpulse(new Vector2((float)Math.Sin(MathHelper.ToRadians(270)), (float)Math.Cos(MathHelper.ToRadians(270))) * 5000);

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

        private Vertices CreateRightFlipperBumper()
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

            return vertices;
        }

    }
}