using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Content;

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
            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, width, height, 1000f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            LoadBorderGeom(physicsSimulator);                  
        }

        public void LoadBorderGeom(PhysicsSimulator physicsSimulator)
        {
            Vector2 geometryOffset = Vector2.Zero;

            base.Geom = new Geom[4];
            //left border
            geometryOffset = new Vector2(-(width *.5f - borderWidth * .5f), 0);
            base.Geom[0] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator,base.Body, borderWidth, height, geometryOffset, 0, 5f);
            base.Geom[0].RestitutionCoefficient = .2f;
            base.Geom[0].FrictionCoefficient = .5f;
            base.Geom[0].CollisionGroup = 100;

            //right border (clone left border since geometry is same size)
            geometryOffset = new Vector2(width * .5f - borderWidth * .5f, 0);
            base.Geom[1] = GeomFactory.Instance.CreateGeom(physicsSimulator,base.Body, base.Geom[0], geometryOffset,0);


            //top border
            geometryOffset = new Vector2(0,-(height * .5f - borderWidth * .5f));
            base.Geom[2] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator,base.Body, width, borderWidth, geometryOffset, 0, 5f);
            base.Geom[2].RestitutionCoefficient = .2f;
            base.Geom[2].FrictionCoefficient = .2f;
            base.Geom[2].CollisionGroup = 100;
            base.Geom[2].ComputeCollisonGrid();

            //bottom border (clone top border since geometry is same size)
            geometryOffset = new Vector2(0,height * .5f - borderWidth * .5f);
            base.Geom[3] = GeomFactory.Instance.CreateGeom(physicsSimulator,base.Body, base.Geom[2], geometryOffset, 0);

            foreach (Geom geometry in base.Geom)
            {
                //geometry.Tag = GameObjects.Border;

                geometry.CollisionCategories = Enums.CollisionCategories.Cat11;
                geometry.CollidesWith = Enums.CollisionCategories.Cat1 & Enums.CollisionCategories.Cat2 & Enums.CollisionCategories.Cat3;

                geometry.Collision += CollisionHandler;
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

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Border))
            //{
                //if (g2.Tag.Equals(GameObjects.LowBall) || g2.Tag.Equals(GameObjects.HighBall) ||
                //    g2.Tag.Equals(GameObjects.NormalBall))
                //{
                    foreach (Contact contact in contactList)
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
                        
                    }
            //    }
            ////}
            //else
            //{
            //    throw new ArgumentException("g1 isn't a border geom.", "g1");
            //}

            return true;
        }  

    }
}
