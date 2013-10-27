using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pinball
{
    public class Sensors : GameObject
    {
        private static Texture2D[] textures = new Texture2D[3];

        private Geom tempBall;

        public static bool ChuteBlockerEnabled { get; set; }

        public static bool LeftAlleyBlockerEnabled { get; set; }

        public static bool RightAlleyBlockerEnabled { get; set; }

        public bool[] ArrowHit { get; set; }

        //public bool ArrowOneHit { get; set; }

        //public bool ArrowTwoHit { get; set; }

        //public bool ArrowThreeHit { get; set; }

        //public bool ArrowFourHit { get; set; }

        //public bool ArrowFiveHit { get; set; }

        private bool lightBlueOneHit = false;
        private float lightBlueOneElapsed = 0f;

        private bool greenTwoHit = false;
        private float greenTwoElapsed = 0f;

        private bool lightBlueTwoHit = false;
        private float lightBlueTwoElapsed = 0f;

        private bool blueTwoHit = false;
        private float blueTwoElapsed = 0f;

        public Sensors(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            ChuteBlockerEnabled = false;
            LeftAlleyBlockerEnabled = true;
            RightAlleyBlockerEnabled = true;

            ArrowHit = new Boolean[5];

            for (int i = 0; i < 5; i++)
            {
                ArrowHit[i] = false;
            }

            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 240, 320, 1000f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[15];

            // Red Sensor One
            base.Geom[0] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 9f, 4, new Vector2(30, 33) - new Vector2(120, 160), 0f, 6f);
            base.Geom[0].Collision += RedSensorOneCollisionHandler;

            // Orange Sensor One
            base.Geom[1] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(38, 256) - new Vector2(120, 160), 0f, 6f);
            base.Geom[1].Collision += OrangeSensorOneCollisionHandler;

            // Yellow Sensor One
            //base.Geom[2] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(122, 94) - new Vector2(120, 160), 0f, 6f);
            //base.Geom[2] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(125, 85) - new Vector2(120, 160), 0f, 6f);
            base.Geom[2] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateYellowOneVertices(), 1f);
            base.Geom[2].Collision += YellowSensorOneCollisionHandler;

            // TODO: Delete Later
            // Green Sensor One
            //base.Geom[3] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(128, 79) - new Vector2(120, 160), 0f, 6f);
            //base.Geom[3].Collision += GreenSensorOneCollisionHandler;

            // Light Blue Sensor One
            base.Geom[3] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(195, 167) - new Vector2(120, 160), 0f, 6f);
            base.Geom[3].Collision += LightBlueSensorOneCollisionHandler;

            // Blue Sensor One
            //base.Geom[4] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateBlueOneVertices(), 1f);
            base.Geom[4] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 7f, 10, new Vector2(199, 112) - new Vector2(120, 160), 0f, 2f);
            //base.Geom[4] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, base.Body, 18, 5, new Vector2(198, 114) - new Vector2(120, 160), 0f, 2f); 
            base.Geom[4].Collision += BlueSensorOneCollisionHandler;

            // TODO: Delete Later
            //// Magenta Sensor One
            //base.Geom[5] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(205, 105) - new Vector2(120, 160), 0f, 6f);
            //base.Geom[5].Collision += MagentaSensorOneCollisionHandler;

            // Red Sensor Two
            base.Geom[5] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(190, 257) - new Vector2(120, 160), 0f, 6f);
            base.Geom[5].Collision += RedSensorTwoCollisionHandler;

            // Orange Sensor Two
            base.Geom[6] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 6f, 4, new Vector2(42, 53) - new Vector2(120, 160), 0f, 2f);
            base.Geom[6].Collision += OrangeSensorTwoCollisionHandler;

            // Yellow Sensor Two
            base.Geom[7] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 5f, 4, new Vector2(75, 26) - new Vector2(120, 160), 0f, 6f);
            base.Geom[7].Collision += YellowSensorTwoCollisionHandler;

            // Green Sensor Two
            base.Geom[8] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(10, 17) - new Vector2(120, 160), 0f, 6f);
            base.Geom[8].Collision += GreenSensorTwoCollisionHandler;

            // Light Blue Sensor Two
            base.Geom[9] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(140, 122) - new Vector2(120, 160), 0f, 6f);
            base.Geom[9].Collision += LightBlueSensorTwoCollisionHandler;

            // Blue Sensor Two
            base.Geom[10] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 6, new Vector2(188, 189) - new Vector2(120, 160), 0f, 6f);
            base.Geom[10].Collision += BlueSensorTwoCollisionHandler;

            // Magenta Sensor Two
            base.Geom[11] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(24, 294) - new Vector2(120, 160), 0f, 6f);
            base.Geom[11].Collision += MagentaSensorTwoCollisionHandler;

            // Red Sensor Three
            base.Geom[12] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 4f, 4, new Vector2(204, 287) - new Vector2(120, 160), 0f, 6f);
            base.Geom[12].Collision += RedSensorThreeCollisionHandler;

            // Orange Sensor Three
            //base.Geom[13] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 8f, 4, new Vector2(207, 24) - new Vector2(120, 160), 0f, 8f);
            base.Geom[13] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateOrangeThreeVertices(), 2f);
            base.Geom[13].Collision += OrangeSensorThreeCollisionHandler;

            // Yellow Sensor Three
            base.Geom[14] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 8f, 6, new Vector2(185, 18) - new Vector2(120, 160), 0f, 8f);
            base.Geom[14].Collision += YellowSensorThreeCollisionHandler;


            //// Green Sensor Three
            //base.Geom[17] = GeomFactory.Instance.CreateCircleGeom(physicsSimulator, base.Body, 6f, 4, new Vector2(64, 31) - new Vector2(120, 160), 0f, 8f);

            foreach (Geom geometry in base.Geom)
            {
                //geometry.Tag = GameObjects.Sensor;


                geometry.CollisionCategories = Enums.CollisionCategories.Cat7;
                geometry.CollidesWith = Enums.CollisionCategories.Cat1 & Enums.CollisionCategories.Cat2 & Enums.CollisionCategories.Cat3;

                //geometry.Collision += CollisionHandler;
            }
        }

        private Vertices CreateYellowOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(131, 87));
            vertices.Add(new Vector2(126, 85));
            vertices.Add(new Vector2(124, 85));
            vertices.Add(new Vector2(120, 84));
            vertices.Add(new Vector2(117, 83));
            vertices.Add(new Vector2(114, 82));
            vertices.Add(new Vector2(114, 85));
            vertices.Add(new Vector2(113, 89));

            vertices.Add(new Vector2(111, 96));

            vertices.Add(new Vector2(118, 97));
            vertices.Add(new Vector2(127, 97));
            vertices.Add(new Vector2(131, 98));
            vertices.Add(new Vector2(131, 92));
            vertices.Add(new Vector2(131, 87));


            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        //private Vertices CreateBlueOneVertices()
        //{
        //    Vertices vertices = new Vertices();

        //    vertices.Add(new Vector2(193, 108));
        //    vertices.Add(new Vector2(191, 111));
        //    vertices.Add(new Vector2(196, 114));
        //    vertices.Add(new Vector2(200, 116));
        //    vertices.Add(new Vector2(205, 118));
        //    vertices.Add(new Vector2(207, 114));
        //    vertices.Add(new Vector2(201, 111));
        //    vertices.Add(new Vector2(197, 109));
        //    vertices.Add(new Vector2(193, 108));
                     
        //    vertices.Add(new Vector2());

        //    for (int counter = 0; counter < vertices.Count; counter++)
        //    {
        //        vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
        //    }

        //    return vertices;
        //}


        private Vertices CreateOrangeThreeVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(208, 10));
            vertices.Add(new Vector2(203, 20));
            vertices.Add(new Vector2(200, 28));
            vertices.Add(new Vector2(199, 33));
            vertices.Add(new Vector2(201, 34));
            vertices.Add(new Vector2(205, 25));
            vertices.Add(new Vector2(208, 18));
            vertices.Add(new Vector2(210, 11));
            vertices.Add(new Vector2(208, 10));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            textures[0] = contentManager.Load<Texture2D>(@"GameTextures\ChuteBlocker");
            textures[1] = contentManager.Load<Texture2D>(@"GameTextures\RightAlleyBlocker");
            textures[2] = contentManager.Load<Texture2D>(@"GameTextures\LeftAlleyBlocker");
        }

        public override void Update(float elapsedTime)
        {
            // Timed light blue one
            if (lightBlueOneHit == true )
            {
                lightBlueOneElapsed += elapsedTime;

                if (lightBlueOneElapsed > 1.5f)
                {
                    lightBlueOneHit = false;

                    lightBlueOneElapsed = 0f;

                    tempBall.Body.Enabled = true;

                    //tempBall.Tag = GameObjects.NormalBall;
                    tempBall.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (350 * World.Multiplier);

                    World.Message = "Arc Ramp " + (350 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (350 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(187, 188);

                    //tempBall.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(210)), (float)Math.Sin(MathHelper.ToRadians(210))) * 1000);

                    SoundManager.SoundEffects["MetalCrash1"].Play();

                    tempBall.Body.LinearVelocity = (new Vector2((float)Math.Cos(MathHelper.ToRadians(155)), (float)Math.Sin(MathHelper.ToRadians(155))) * 300);

                    tempBall = null;
                }
            }
 
            // Timed green two
            if (greenTwoHit == true)
            {
                greenTwoElapsed += elapsedTime;

                if (greenTwoElapsed > 3.5f)
                {
                    greenTwoHit = false;

                    greenTwoElapsed = 0f;

                    tempBall.Body.Enabled = true;

                    //tempBall.Tag = GameObjects.NormalBall;
                    tempBall.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (250 * World.Multiplier);

                    World.Message = "Alley Hole " + (250 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (250 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(140, 122);

                    //tempBall.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(225)), (float)Math.Sin(MathHelper.ToRadians(225))) * 1000);

                    SoundManager.SoundEffects["MetalCrash2"].Play();

                    tempBall.Body.LinearVelocity = (new Vector2((float)Math.Cos(MathHelper.ToRadians(120)), (float)Math.Sin(MathHelper.ToRadians(120))) * 300);

                    tempBall = null;
                }
            }

            // Timed light blue two
            if (lightBlueTwoHit == true)
            {
                lightBlueTwoElapsed += elapsedTime;

                if (lightBlueTwoElapsed > 2.5f)
                {
                    lightBlueTwoHit = false;

                    lightBlueTwoElapsed = 0f;

                    tempBall.Body.Enabled = true;

                    //tempBall.Tag = GameObjects.NormalBall;
                    tempBall.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (200 * World.Multiplier);

                    World.Message = "Hole " + (200 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (200 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(187, 188);

                    //tempBall.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(210)), (float)Math.Sin(MathHelper.ToRadians(210))) * 1000);

                    //tempBall.Body.ApplyImpulse(new Vector2(0, 1000));

                    SoundManager.SoundEffects["MetalCrash1"].Play();

                    tempBall.Body.LinearVelocity = (new Vector2((float)Math.Cos(MathHelper.ToRadians(155)), (float)Math.Sin(MathHelper.ToRadians(155))) * 300);

                    tempBall = null;
                }
            }

            // Timed blue two
            if (blueTwoHit == true)
            {
                blueTwoElapsed += elapsedTime;

                if (blueTwoElapsed > 2.5f)
                {
                    blueTwoHit = false;

                    blueTwoElapsed = 0f;

                    tempBall.Body.Enabled = true;

                    //tempBall.Tag = GameObjects.NormalBall;
                    tempBall.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (200 * World.Multiplier);

                    World.Message = "Hole " + (200 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (200 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(140, 122);

                    //tempBall.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(225)), (float)Math.Sin(MathHelper.ToRadians(225))) * 1000);

                    //tempBall.Body.ApplyImpulse(new Vector2(0, 1000));

                    SoundManager.SoundEffects["MetalCrash2"].Play();

                    tempBall.Body.LinearVelocity = (new Vector2((float)Math.Cos(MathHelper.ToRadians(120)), (float)Math.Sin(MathHelper.ToRadians(120))) * 300);

                    tempBall = null;
                }
            }

            base.Update(elapsedTime);
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            if (ChuteBlockerEnabled)
            {
                spriteBatch.Draw(textures[0], new Vector2(204, 22), null, Color.White, 0f, new Vector2(textures[0].Width / 2, textures[0].Height / 2), 1f, SpriteEffects.None, 0f);
            }
            if (RightAlleyBlockerEnabled)
            {
                spriteBatch.Draw(textures[1], new Vector2(202, 283), null, Color.White, 0f, new Vector2(textures[1].Width / 2, textures[1].Height / 2), 1f, SpriteEffects.None, 0f);
            }
            if (LeftAlleyBlockerEnabled)
            {
                spriteBatch.Draw(textures[2], new Vector2(30, 288), null, Color.White, 0f, new Vector2(textures[2].Width / 2, textures[2].Height / 2), 1f, SpriteEffects.None, 0f);
            }
        }

        // TODO: Delete Later
        //public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    return false;
        //}

        private bool RedSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    if (g2.Body.LinearVelocity.Y > 0)
                    {
                        //g2.Tag = GameObjects.HighBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat1;

                        ArrowHit[2] = true; 
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1"); 
            //}

            return false;
        }

        private bool OrangeSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat1)
                {
                    //g2.Tag = GameObjects.NormalBall;
                    g2.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (150 * World.Multiplier);

                    World.Message = "Power Down Ramp " + (150 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (150 * World.Multiplier);
                    }

                    SoundManager.SoundEffects["PowerDown"].Play();

                    g2.Body.LinearVelocity = Vector2.Zero;

                    g2.Body.AngularVelocity = 0f;
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool YellowSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is touching the top part of the sensor 
                    if (g2.Body.Position.Y > 70 && g2.Body.Position.Y < 90)
                    {
                        // Make a wall so it doesn't go down the ramp
                        //if (g2.Body.LinearVelocity.Y > -5)
                        //{
                            return true;
                        //}
                    }
                    else
                    {
                        // Ball is going up, but not touching the top part of the wall
                        //if (g2.Body.LinearVelocity.Y <= -5)
                        //{
                            //g2.Tag = GameObjects.HighBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat1;

                            ArrowHit[3] = true;
                        //}
                    }
                }
                else if (g2.CollisionCategories == Enums.CollisionCategories.Cat1)
                {
                    // Ball is heading back down
                    if (g2.Body.LinearVelocity.Y > -5)
                    {
                        if (g2.Body.Position.Y > 90)
                        {
                            //g2.Tag = GameObjects.NormalBall; 
                            g2.CollisionCategories = Enums.CollisionCategories.Cat2;
                        }
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        // TODO: Delete Later
        //public bool GreenSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    if (g1.Tag.Equals(GameObjects.Sensor))
        //    {
        //        if (g2.Tag.Equals(GameObjects.HighBall))
        //        {
        //            // Ball is heading back down
        //            if (g2.Body.LinearVelocity.Y > 0)
        //            {
        //                g2.Tag = GameObjects.NormalBall;
        //            }   
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("g1 is a sensor geom.", "g1");
        //    }

        //    return false;
        //}
        
        private bool LightBlueSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat1)
                {
                    //g2.Tag = GameObjects.LowBall;
                    g2.CollisionCategories = Enums.CollisionCategories.Cat3;

                    SoundManager.SoundEffects["PowerUp3"].Play();

                    lightBlueOneHit = true;

                    g2.Body.Enabled = false;

                    //g2.Body.ClearImpulse();

                    //g2.Body.ClearForce();

                    g2.Body.Position = new Vector2(195, 167);

                    tempBall = g2;
                }
                //else if (g2.Tag.Equals(GameObjects.LowBall))
                //{
                //    //g2.Body.ClearImpulse();

                //    //g2.Body.Position = new Vector2(195, 167);

                //    if (lightBlueOneHit == false)
                //    {
                //        g2.Body.Enabled = true;

                //        g2.Tag = GameObjects.NormalBall;

                //        World.Score += (350 * World.Multiplier);

                //        g2.Body.Position = new Vector2(187, 188);

                //        g2.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(210)), (float)Math.Sin(MathHelper.ToRadians(210))) * 1000); 
                //    }
                //}
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool BlueSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is heading up
                    if (g2.Body.LinearVelocity.Y < 5 && g2.Body.LinearVelocity.X > 0)
                    {
                        //g2.Tag = GameObjects.HighBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat1;

                        ArrowHit[4] = true;
                    }
                }
                else if (g2.CollisionCategories == Enums.CollisionCategories.Cat1)
                {
                    // Ball is heading back down
                    if (g2.Body.LinearVelocity.Y > -1)
                    {
                        //g2.Tag = GameObjects.NormalBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat2;
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        // TODO: Delete Later
        //public bool MagentaSensorOneCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    if (g1.Tag.Equals(GameObjects.Sensor))
        //    {
        //        if (g1.Tag.Equals(GameObjects.HighBall))
        //        {
        //            // Ball is heading back down
        //            if (g2.Body.LinearVelocity.Y > 0 && g2.Body.LinearVelocity.X < 0)
        //            {
        //                g2.Tag = GameObjects.NormalBall;
        //            }   
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("g1 is a sensor geom.", "g1");
        //    }

        //    return false;
        //}

        private bool RedSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat1)
                {
                    //g2.Tag = GameObjects.NormalBall;
                    g2.CollisionCategories = Enums.CollisionCategories.Cat2;

                    World.Score += (150 * World.Multiplier);

                    World.Message = "Power Down Ramp " + (150 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (150 * World.Multiplier);
                    }

                    SoundManager.SoundEffects["PowerDown"].Play();

                    g2.Body.LinearVelocity = Vector2.Zero;

                    g1.Body.AngularVelocity = 0f;
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool OrangeSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is traveling up
                    if (g2.Body.LinearVelocity.Y < 0)
                    {
                        //g2.Tag = GameObjects.LowBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat3;

                        ArrowHit[1] = true;
                    }
                }
                else if (g2.CollisionCategories == Enums.CollisionCategories.Cat3)
                {
                    // Ball is traveling down
                    if (g2.Body.LinearVelocity.Y > 0)
                    {
                        //g2.Tag = GameObjects.NormalBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat2;
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool YellowSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is traveling down
                    if (g2.Body.LinearVelocity.Y > -5)
                    {
                        //g2.Tag = GameObjects.LowBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat3;
                    }
                }
                else if (g2.CollisionCategories == Enums.CollisionCategories.Cat3)
                {
                    // Ball is traveling up
                    if (g2.Body.LinearVelocity.Y < 0)
                    {
                        //g2.Tag = GameObjects.NormalBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat2;
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool GreenSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is traveling up
                    if (g2.Body.LinearVelocity.Y < 0)
                    {
                        //g2.Tag = GameObjects.LowBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat3;

                        SoundManager.SoundEffects["PowerUp1"].Play();

                        greenTwoHit = true;

                        //g2.Body.ClearImpulse();

                        //g2.Body.ClearForce();

                        g2.Body.Enabled = false;

                        g2.Body.Position = new Vector2(10, 17);

                        tempBall = g2;

                        ArrowHit[0] = true;
                    }
                }
                //else if (g2.Tag.Equals(GameObjects.LowBall))
                //{
                //    //g2.Body.ClearImpulse();

                //    //g2.Body.Position = new Vector2(10, 17);

                //    if (greenTwoHit == false)
                //    {
                //        g2.Body.Enabled = true;

                //        g2.Tag = GameObjects.NormalBall;

                //        World.Score += (250 * World.Multiplier);

                //        g2.Body.Position = new Vector2(140, 122);

                //        g2.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(225)), (float)Math.Sin(MathHelper.ToRadians(225))) * 1000);
                //    }
                //}
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool LightBlueSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is traveling up
                    if (g2.Body.LinearVelocity.Y < 0)
                    {
                        //g2.Tag = GameObjects.LowBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat3;

                        SoundManager.SoundEffects["PowerUp2"].Play();

                        lightBlueTwoHit = true;

                        g2.Body.Enabled = false;

                        //g2.Body.ClearImpulse();

                        //g2.Body.ClearForce();

                        g2.Body.Position = new Vector2(140, 122);

                        tempBall = g2;
                    }
                }
                //else if (g2.Tag.Equals(GameObjects.LowBall))
                //{
                //    //g2.Body.ClearImpulse();

                //    //g2.Body.Position = new Vector2(140, 122);

                //    if (lightBlueTwoHit == false)
                //    {
                //        g2.Body.Enabled = true;

                //        g2.Tag = GameObjects.NormalBall;

                //        World.Score += (200 * World.Multiplier);

                //        g2.Body.Position = new Vector2(187, 188);

                //        g2.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(210)), (float)Math.Sin(MathHelper.ToRadians(210))) * 1000);
                //    }
                //}
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool BlueSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    // Ball is going up
                    if (g2.Body.LinearVelocity.Y < 0)
                    {
                        //g2.Tag = GameObjects.LowBall;
                        g2.CollisionCategories = Enums.CollisionCategories.Cat3;


                        SoundManager.SoundEffects["PowerUp2"].Play();

                        blueTwoHit = true;

                        //g2.Body.ClearImpulse();

                        //g2.Body.ClearForce();

                        g2.Body.Enabled = false;

                        g2.Body.Position = new Vector2(187, 188);

                        tempBall = g2;
                    }
                }
                //else if (g2.Tag.Equals(GameObjects.LowBall))
                //{
                //    //g2.Body.ClearImpulse();

                //    //g2.Body.Position = new Vector2(187, 188);

                //    if (blueTwoHit == false)
                //    {
                //        g2.Body.Enabled = true;

                //        g2.Tag = GameObjects.NormalBall;

                //        World.Score += (200 * World.Multiplier);

                //        g2.Body.Position = new Vector2(140, 122);

                //        g2.Body.ApplyImpulse(new Vector2((float)Math.Cos(MathHelper.ToRadians(225)), (float)Math.Sin(MathHelper.ToRadians(225))) * 1000);
                //    }
                //}
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool MagentaSensorTwoCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                // TODO: Fix this so a bar appears and can be changed

                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    if (LeftAlleyBlockerEnabled)
                    {
                        World.Score += (75 * World.Multiplier);

                        World.Message = "Death Chute " + (75 * World.Multiplier).ToString();

                        if (GameLogic.JackpotTimeLeft > 0f)
                        {
                            StorageManager.GameDataInstance.JackpotAmount += (75 * World.Multiplier);
                        }

                        g2.Body.LinearVelocity = new Vector2(0, -300);

                        SoundManager.SoundEffects["ClickClack2"].Play();

                        LeftAlleyBlockerEnabled = false;
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool RedSensorThreeCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                // TODO: Fix this so a bar appears and can be changed

                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {
                    if (RightAlleyBlockerEnabled)
                    {
                        World.Score += (75 * World.Multiplier);

                        World.Message = "Death Chute " + (75 * World.Multiplier).ToString();

                        if (GameLogic.JackpotTimeLeft > 0f)
                        {
                            StorageManager.GameDataInstance.JackpotAmount += (75 * World.Multiplier);
                        }

                        g2.Body.LinearVelocity = new Vector2(0, -300);

                        SoundManager.SoundEffects["ClickClack2"].Play();

                        RightAlleyBlockerEnabled = false; 
                    }
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        private bool OrangeSensorThreeCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Sensor))
            //{
                // TODO: Fix this so a bar appears

                if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
                {

                    if (ChuteBlockerEnabled)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    //// TODO: Put stopper bar here
                    //if ((!ChuteBlockerEnabled) && g2.Body.LinearVelocity.Y < 5 && g2.Body.LinearVelocity.X < 5)
                    //{
                    //    ChuteBlockerEnabled = true;

                    //    SoundManager.SoundEffects["ClickClack1"].Play();

                    //    return false;
                    //}
                    //else if (g2.Body.LinearVelocity.Y > 0 && g2.Body.LinearVelocity.X > 0)
                    //{
                    //    //g2.Body.ApplyImpulse(new Vector2(-100, 0));

                    //    return true;
                    //}
                    //g2.Body.ApplyImpulse(new Vector2(-1000, 0));
                }
            //}
            //else
            //{
            //    throw new ArgumentException("g1 is a sensor geom.", "g1");
            //}

            return false;
        }

        public bool YellowSensorThreeCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {


            if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
            {
                // TODO: Put stopper bar here
                if (!ChuteBlockerEnabled)
                {
                    ChuteBlockerEnabled = true;

                    SoundManager.SoundEffects["ClickClack1"].Play();
                }
            }

            return false;
        }

        //public bool GreenSensorThreeCollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    if (g1.Tag.Equals(GameObjects.Sensor))
        //    {

        //    }
        //    else
        //    {
        //        throw new ArgumentException("g1 is a sensor geom.", "g1");
        //    }

        //    return false;
        //}
    }
}