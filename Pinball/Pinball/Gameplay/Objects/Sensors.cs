using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;

namespace Pinball
{
    public class Sensors : GameObject
    {
        private static Texture2D[] textures = new Texture2D[3];

        private Fixture tempBall;

        public static bool ChuteBlockerEnabled { get; set; }

        public static bool LeftAlleyBlockerEnabled { get; set; }

        public static bool RightAlleyBlockerEnabled { get; set; }

        public bool[] ArrowHit { get; set; }
        
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

            base.Body = BodyFactory.CreateBody(physicsSimulator, position);//, 240, 320, 1000f);
            base.Body.IsStatic = true;
            //base.Body.Position = position;

            // Red Sensor One
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(9, 1, base.Body, new Vector2(30, 33) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += RedSensorOneCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Orange Sensor One
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(38, 256) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += OrangeSensorOneCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Yellow Sensor One
            foreach (Vertices vertices in CreateYellowOneVertices())
            {
                base.Fixtures.AddLast(FixtureFactory.CreatePolygon(vertices, 1, base.Body, Vector2.Zero));
                base.Fixtures.Last.Value.OnCollision += YellowSensorOneCollisionHandler;
                base.Fixtures.Last.Value.IsSensor = true;
            }               

            // Light Blue Sensor One
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(195, 167) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += LightBlueSensorOneCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Blue Sensor One
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(7, 1, base.Body, new Vector2(199, 112) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += BlueSensorOneCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Red Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(190, 257) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += RedSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Orange Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(6, 1, base.Body, new Vector2(42, 53) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += OrangeSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Yellow Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(5, 1, base.Body, new Vector2(75, 26) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += YellowSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Green Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(10, 17) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += GreenSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Light Blue Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(140, 122) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += LightBlueSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Blue Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(188, 189) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += BlueSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Magenta Sensor Two
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(24, 294) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += MagentaSensorTwoCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Red Sensor Three
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(4, 1, base.Body, new Vector2(204, 287) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += RedSensorThreeCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            // Orange Sensor Three
            foreach (Vertices vertices in CreateOrangeThreeVertices())
            {
                base.Fixtures.AddLast(FixtureFactory.CreatePolygon(vertices, 1, base.Body, Vector2.Zero));
                base.Fixtures.Last.Value.OnCollision += OrangeSensorThreeCollisionHandler;
                base.Fixtures.Last.Value.IsSensor = true;
            }

            // Yellow Sensor Three
            base.Fixtures.AddLast(FixtureFactory.CreateCircle(8, 1, base.Body, new Vector2(185, 18) - new Vector2(120, 160)));
            base.Fixtures.Last.Value.OnCollision += YellowSensorThreeCollisionHandler;
            base.Fixtures.Last.Value.IsSensor = true;

            foreach (Fixture geometry in base.Fixtures)
            {
                geometry.CollisionCategories = CollisionCategory.Cat7;
                geometry.CollidesWith = CollisionCategory.Cat1 & CollisionCategory.Cat2 & CollisionCategory.Cat3;
            }
        }

        private List<Vertices> CreateYellowOneVertices()
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

            return EarclipDecomposer.ConvexPartition(vertices);
        }
       
        private List<Vertices> CreateOrangeThreeVertices()
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

            return EarclipDecomposer.ConvexPartition(vertices);
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

                    tempBall.Body.Active = true;

                    tempBall.CollisionCategories = CollisionCategory.Cat2;

                    World.Score += (350 * World.Multiplier);

                    World.Message = "Arc Ramp " + (350 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (350 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(187, 188);

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

                    tempBall.Body.Active = true;

                    tempBall.CollisionCategories = CollisionCategory.Cat2;

                    World.Score += (250 * World.Multiplier);

                    World.Message = "Alley Hole " + (250 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (250 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(140, 122);

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

                    tempBall.Body.Active = true;

                    tempBall.CollisionCategories = CollisionCategory.Cat2;

                    World.Score += (200 * World.Multiplier);

                    World.Message = "Hole " + (200 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (200 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(187, 188);
                    
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

                    tempBall.Body.Active = true;

                    tempBall.CollisionCategories = CollisionCategory.Cat2;

                    World.Score += (200 * World.Multiplier);

                    World.Message = "Hole " + (200 * World.Multiplier).ToString();

                    if (GameLogic.JackpotTimeLeft > 0f)
                    {
                        StorageManager.GameDataInstance.JackpotAmount += (200 * World.Multiplier);
                    }

                    tempBall.Body.Position = new Vector2(140, 122);

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
        
        private bool RedSensorOneCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                if (g2.Body.LinearVelocity.Y > 0)
                {
                    //g2.Tag = GameObjects.HighBall;
                    g2.CollisionCategories = CollisionCategory.Cat1;

                    ArrowHit[2] = true; 
                }
            }
            return false;
        }

        private bool OrangeSensorOneCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
           
            if (g2.CollisionCategories == CollisionCategory.Cat1)
            {
                g2.CollisionCategories = CollisionCategory.Cat2;

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

            return false;
        }

        private bool YellowSensorOneCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is touching the top part of the sensor 
                if (g2.Body.Position.Y > 70 && g2.Body.Position.Y < 90)
                {                        
                    return true;
                }
                else
                {
                    // Ball is going up, but not touching the top part of the wall
                    g2.CollisionCategories = CollisionCategory.Cat1;

                    ArrowHit[3] = true;
                }
            }
            else if (g2.CollisionCategories == CollisionCategory.Cat1)
            {
                // Ball is heading back down
                if (g2.Body.LinearVelocity.Y > -5)
                {
                    if (g2.Body.Position.Y > 90)
                    {
                        g2.CollisionCategories = CollisionCategory.Cat2;
                    }
                }
            }

            return false;
        }
               
        private bool LightBlueSensorOneCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            
            if (g2.CollisionCategories == CollisionCategory.Cat1)
            {
                g2.CollisionCategories = CollisionCategory.Cat3;

                SoundManager.SoundEffects["PowerUp3"].Play();

                lightBlueOneHit = true;

                g2.Body.Active = false;
                    
                g2.Body.Position = new Vector2(195, 167);

                tempBall = g2;
            }
                
            return false;
        }

        private bool BlueSensorOneCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {            
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is heading up
                if (g2.Body.LinearVelocity.Y < 5 && g2.Body.LinearVelocity.X > 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat1;

                    ArrowHit[4] = true;
                }
            }
            else if (g2.CollisionCategories == CollisionCategory.Cat1)
            {
                // Ball is heading back down
                if (g2.Body.LinearVelocity.Y > -1)
                {
                    g2.CollisionCategories = CollisionCategory.Cat2;
                }
            }

            return false;
        }
        
        private bool RedSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat1)
            {
                g2.CollisionCategories = CollisionCategory.Cat2;

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

            return false;
        }

        private bool OrangeSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is traveling up
                if (g2.Body.LinearVelocity.Y < 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat3;

                    ArrowHit[1] = true;
                }
            }
            else if (g2.CollisionCategories == CollisionCategory.Cat3)
            {
                // Ball is traveling down
                if (g2.Body.LinearVelocity.Y > 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat2;
                }
            }

            return false;
        }

        private bool YellowSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
           
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is traveling down
                if (g2.Body.LinearVelocity.Y > -5)
                {
                    g2.CollisionCategories = CollisionCategory.Cat3;
                }
            }
            else if (g2.CollisionCategories == CollisionCategory.Cat3)
            {
                // Ball is traveling up
                if (g2.Body.LinearVelocity.Y < 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat2;
                }
            }

            return false;
        }

        private bool GreenSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is traveling up
                if (g2.Body.LinearVelocity.Y < 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat3;

                    SoundManager.SoundEffects["PowerUp1"].Play();

                    greenTwoHit = true;

                    g2.Body.Active = false;

                    g2.Body.Position = new Vector2(10, 17);

                    tempBall = g2;

                    ArrowHit[0] = true;
                }
            }                

            return false;
        }

        private bool LightBlueSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {               
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is traveling up
                if (g2.Body.LinearVelocity.Y < 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat3;

                    SoundManager.SoundEffects["PowerUp2"].Play();

                    lightBlueTwoHit = true;

                    g2.Body.Active = false;
                        
                    g2.Body.Position = new Vector2(140, 122);

                    tempBall = g2;
                }
            }

            return false;
        }

        private bool BlueSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {
                // Ball is going up
                if (g2.Body.LinearVelocity.Y < 0)
                {
                    g2.CollisionCategories = CollisionCategory.Cat3;

                    SoundManager.SoundEffects["PowerUp2"].Play();

                    blueTwoHit = true;

                    g2.Body.Active = false;

                    g2.Body.Position = new Vector2(187, 188);

                    tempBall = g2;
                }
            }
            return false;
        }

        private bool MagentaSensorTwoCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            // TODO: Fix this so a bar appears and can be changed

            if (g2.CollisionCategories == CollisionCategory.Cat2)
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
            

            return false;
        }

        private bool RedSensorThreeCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            // TODO: Fix this so a bar appears and can be changed

            if (g2.CollisionCategories == CollisionCategory.Cat2)
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

            return false;
        }

        private bool OrangeSensorThreeCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            
            // TODO: Fix this so a bar appears

            if (g2.CollisionCategories == CollisionCategory.Cat2)
            {

                if (ChuteBlockerEnabled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
 
            }

            return false;
        }

        public bool YellowSensorThreeCollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {


            if (g2.CollisionCategories == CollisionCategory.Cat2)
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
    }
}