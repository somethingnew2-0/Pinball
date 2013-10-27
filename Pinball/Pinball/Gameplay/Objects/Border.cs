using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;

namespace Pinball
{
    public class Border : GameObject
    {
        public bool LostBall { get; set; }

        private float elaspedLost = 0f;

        private int width;
        private int height;
        private int borderWidth;

        public Border(PhysicsSimulator physicsSimulator, int width, int height, int borderWidth, Vector2 position)
        {
            LostBall = false;

            this.width = width;
            this.height = height;
            this.borderWidth = borderWidth;
            
            //use the body factory to create the physics body
            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//, width, height, 1000f);
            base.Body.IsStatic = true;
            //base.Body.Position = position;

            LoadBorderGeom(physicsSimulator);                  
        }

        public void LoadBorderGeom(PhysicsSimulator physicsSimulator)
        {
            Vector2 geometryOffset = Vector2.Zero;

            //left border
            geometryOffset = new Vector2(-(width *.5f - borderWidth * .5f), 0);
            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(borderWidth, height, 1, base.Body, geometryOffset));
            base.Fixtures.Last.Value.Restitution = .2f;
            base.Fixtures.Last.Value.Friction = .5f;
            base.Fixtures.Last.Value.CollisionGroup = 100;

            //right border (clone left border since geometry is same size)
            geometryOffset = new Vector2(width * .5f - borderWidth * .5f, 0);
            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(borderWidth, height, 1, base.Body, geometryOffset));
            base.Fixtures.Last.Value.Restitution = .2f;
            base.Fixtures.Last.Value.Friction = .5f;
            base.Fixtures.Last.Value.CollisionGroup = 100;

            //top border
            geometryOffset = new Vector2(0,-(height * .5f - borderWidth * .5f));
            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(width, borderWidth, 1, base.Body, geometryOffset));
            base.Fixtures.Last.Value.Restitution = .2f;
            base.Fixtures.Last.Value.Friction = .2f;
            base.Fixtures.Last.Value.CollisionGroup = 100;

            //bottom border (clone top border since geometry is same size)
            geometryOffset = new Vector2(0, height * .5f - borderWidth * .5f); 
            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(width, borderWidth, 1, base.Body, geometryOffset));
            base.Fixtures.Last.Value.Restitution = .2f;
            base.Fixtures.Last.Value.Friction = .2f;
            base.Fixtures.Last.Value.CollisionGroup = 100;

            foreach (Fixture geometry in base.Fixtures)
            {
                geometry.CollisionCategories = CollisionCategory.Cat11;
                geometry.CollidesWith = CollisionCategory.Cat1 & CollisionCategory.Cat2 & CollisionCategory.Cat3;

                geometry.OnCollision += CollisionHandler;
            }
        }

        public override void Update(float elapsedTime)
        {
            if (LostBall)
            {
                elaspedLost += elapsedTime;
            }

            base.Update(elapsedTime);
        }

        public override bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            /*foreach (Contact contact in contactList)
            {
                if ((contact.Position.X > -5 && contact.Position.X < 245) &&
                    contact.Position.Y > 320 && contact.Position.Y < 360)
                {
                    if (!LostBall)
                    {
                        World.Message = "Ball Lost";

                        // Lost the ball
                        SoundManager.SoundEffects["LostBall"].Play();
                                
                        // Subtract the number of balls left
                        World.BallsLeft--;

                        LostBall = true;
                    }
                    else
                    {
                        if (elaspedLost > 3f)
                        {
                            SoundManager.SoundEffects["Start"].Play();

                            elaspedLost = 0f;

                            LostBall = false;

                            World.ResetBall();
                        }
                    }


                    break;
                }
                        
            }*/

            return true;
        }  

    }
}
