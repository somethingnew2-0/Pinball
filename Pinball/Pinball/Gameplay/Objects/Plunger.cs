using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;

namespace Pinball
{
    public class Plunger : GameObject
    {
        private static List<Texture2D> textures = new List<Texture2D>();

        private int power = 0;
        private bool applyForce = false;
 
        public Plunger(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//12, 39, 50f);
            //base.Body.Position = position;
            base.Body.IsStatic = true;

            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(12, 39, 50, base.Body, Vector2.Zero));
            
            base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat5;
            base.Fixtures.Last.Value.CollidesWith = CollisionCategory.Cat2;

            base.Fixtures.Last.Value.OnCollision += CollisionHandler;
         }

        public static void LoadContent(ContentManager contentManager)
        {
            for (int counter = 1; counter <= 5; counter++)
            {
                textures.Add(contentManager.Load<Texture2D>(@"GameTextures\Plunger" + counter.ToString()));
            }
        }

        public void HandleInput(bool bottomHit)
        {
            if (applyForce)
            {
                applyForce = false;

                power = 0;
            }

            if (bottomHit)
            {
                power += 200;

                if (power > 12000)
                {
                    power = 12000;
                }
            }
            else
            {
                if (power > 0)
                {
                    applyForce = true;
                }
            }
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            if (power != 0)
            {
                base.Draw(elapsedTime, spriteBatch, textures[(4 - ((int)(Math.Round((double)(power / 3000), 0))))], Color.White); 
            }
        }

        private Vector2 linearImpulse = Vector2.Zero;

        public override bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                if (applyForce)
                {
                    linearImpulse.Y = -power;
                    g2.Body.ApplyLinearImpulse(ref linearImpulse);

                    if (power > 7000)
                    {
                        SoundManager.SoundEffects["LargePlungerHit"].Play();
                    }
                    else
                    {
                        SoundManager.SoundEffects["SmallPlungerHit"].Play();
                    }

                    applyForce = false;
                    power = 0;
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}