using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework.Audio;
using FarseerGames.FarseerPhysics.Factories;

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

            base.Body = BodyFactory.Instance.CreateCircleBody(physicsSimulator, 5f, 5f);
            base.Body.Position = position;

            base.Geom = new Geom[1];
            base.Geom[0] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 5f, 24, 0.5f);

            //Vertices extendedAABB = Vertices.CreateRectangle(50, 50);
            //base.Geom[0].AABB.Update(ref extendedAABB);
            
            base.Geom[0].CollisionCategories = CollisionCategory.Cat2;
            // TODO: Fix this so it collides correctly
            base.Geom[0].CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat10;

            base.Geom[0].OnCollision += CollisionHandler;

            //base.Geom[0].RestitutionCoefficient = 0.5f;
            base.Body.RotationalDragCoefficient = 0.02f;
            base.Body.LinearDragCoefficient = 0.005f;

            //base.Geom[0].Tag = GameObjects.NormalBall;
        
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
                            base.Geom[0].CollisionCategories = CollisionCategory.Cat2;
                        }
                    }
                    else
                    {
                        elapsedBallRestingTime = 0f;
                    } 
                }
            }

            if (base.Geom[0].CollisionCategories == CollisionCategory.Cat1)
            {
                base.Geom[0].CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat9 & ~CollisionCategory.Cat10;
            }
            else if (base.Geom[0].CollisionCategories == CollisionCategory.Cat2)
            {
                base.Geom[0].CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat10;
            }
            else if (base.Geom[0].CollisionCategories == CollisionCategory.Cat3)
            {
                base.Geom[0].CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat8 & ~CollisionCategory.Cat9;
            }

            if (base.Geom[0].CollisionCategories == CollisionCategory.Cat1 || base.Geom[0].CollisionCategories == CollisionCategory.Cat3)
            {
                if ((Math.Abs(base.Body.Position.X - 114) < 60) && (Math.Abs(base.Body.Position.Y - 174) < 40))
                {
                    base.Geom[0].CollisionCategories = CollisionCategory.Cat2;
                }   
            }

            base.Update(elapsedTime);
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            //if (base.Geom[0].Tag.Equals(GameObjects.HighBall) || base.Geom[0].Tag.Equals(GameObjects.NormalBall))
            if (base.Geom[0].CollisionCategories == CollisionCategory.Cat1 || base.Geom[0].CollisionCategories == CollisionCategory.Cat2)
            {
                base.Draw(elapsedTime, spriteBatch, Texture, Color.White);                
            }
        }

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
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
                        instanceCheck = SoundManager.SoundEffects["Click" + RandomMath.Random.Next(1, 3).ToString()].Play();
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