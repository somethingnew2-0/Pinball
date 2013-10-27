using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using FarseerPhysics;
using Microsoft.Xna.Framework.Audio;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;

namespace Pinball
{
    public class Ball : GameObject
    {
        public static Texture2D Texture { get; set; }

        /// <summary>
        /// Makes sure the ball doesn't sit around
        /// </summary>
        public static bool AutoTilt { get; set; }

        private float elapsedBallRestingTime = 0f;

        private const float maxBallRestingTime = 1.5f;

        private SoundEffectInstance instanceCheck = null;

        public Ball(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            // TODO: Change this later into an option
            AutoTilt = true;

            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//5f, 5f);

            base.Fixtures.AddLast(FixtureFactory.CreateCircle(5, 5, base.Body, Vector2.Zero));
                        
            base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat2;
            // TODO: Fix this so it collides correctly
            base.Fixtures.Last.Value.CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat10;

            base.Fixtures.Last.Value.OnCollision += CollisionHandler;

            //base.Body.RotationalDragCoefficient = 0.02f;
            //base.Body.LinearDragCoefficient = 0.005f;
                   
        }

        public static void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(@"GameTextures\Ball");
        }

        public override void Update(float elapsedTime)
        {
            if (AutoTilt)
            {
                // Check to make sure it's not near the plunger
                if (!(base.Body.Position.X >= 210 && base.Body.Position.X <= 223) &&
                        !(base.Body.Position.Y >= 267 && base.Body.Position.Y <= 278))
                {
                    // Check to see if the ball is sitting around
                    if ((base.Body.LinearVelocity.X > -10 && base.Body.LinearVelocity.X < 10) &&
                        (base.Body.LinearVelocity.Y > -10 && base.Body.LinearVelocity.Y < 10))
                    {
                        elapsedBallRestingTime += elapsedTime;

                        if (elapsedBallRestingTime >= maxBallRestingTime)
                        {
                            // Shoot the ball in a random direction
                            base.Body.LinearVelocity = RandomMath.RandomDirection() * 100;
                        
                            // Change the tag so the ball is normal
                            //base.Body.Tag = GameObjects.NormalBall;
                            base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat2;
                        }
                    }
                    else
                    {
                        elapsedBallRestingTime = 0f;
                    } 
                }
            }

            if (base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat1)
            {
                base.Fixtures.Last.Value.CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat9 & ~CollisionCategory.Cat10;
            }
            else if (base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat2)
            {
                base.Fixtures.Last.Value.CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat10;
            }
            else if (base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat3)
            {
                base.Fixtures.Last.Value.CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat9;
            }

            if (base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat1 || base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat3)
            {
                if ((Math.Abs(base.Body.Position.X - 114) < 60) && (Math.Abs(base.Body.Position.Y - 174) < 40))
                {
                    base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat2;
                }   
            }

            base.Update(elapsedTime);
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            //if (base.Fixtures.Last.Value.Tag.Equals(GameObjects.HighBall) || base.Fixtures.Last.Value.Tag.Equals(GameObjects.NormalBall))
            if (base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat1 || base.Fixtures.Last.Value.CollisionCategories == CollisionCategory.Cat2)
            {
                base.Draw(elapsedTime, spriteBatch, Texture, Color.White);                
            }
        }

        public override bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (((g1.CollisionCategories == CollisionCategory.Cat2) && (g2.CollisionCategories == CollisionCategory.Cat9 || g2.CollisionCategories == CollisionCategory.Cat4))
                || ((g1.CollisionCategories == CollisionCategory.Cat1) && (g2.CollisionCategories == CollisionCategory.Cat8))
                || ((g1.CollisionCategories == CollisionCategory.Cat3) && (g2.CollisionCategories == CollisionCategory.Cat10)))
            {
                if (Math.Abs(g1.Body.LinearVelocity.X) > 25 || Math.Abs(g1.Body.LinearVelocity.Y) > 25)
                {
                    // Makes sure there isn't more than one instance playing at once
                    if (instanceCheck == null || instanceCheck.State == SoundState.Stopped)
                    {
                        instanceCheck = SoundManager.SoundEffects["Click" + RandomMath.Random.Next(1, 3).ToString()].CreateInstance();
                        instanceCheck.Play();
                    }
                } 
            }

            if ((Math.Abs(g1.Body.Position.X - 197) < 5) && (Math.Abs(g1.Body.Position.Y - 117) < 5))
            {
                if (g1.CollisionCategories == CollisionCategory.Cat2 && g2.CollisionCategories == CollisionCategory.Cat8)
                {
                    return false;
                }
            }

            return base.CollisionHandler(g1, g2, contactList);
        }
    }
}