using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using PhysicsSimulator = FarseerPhysics.Dynamics.World;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;

namespace Pinball
{
    public class LeftFlipper : GameObject
    {
        private static Texture2D texture;

        private bool playOnce = true;
        
        FixedRevoluteJoint revoluteJoint;
        //FixedAngleSpring fixedAngleSpring;

        public float DampningConstant { get; set; }

        public float SpringConstant { get; set; }

        public LeftFlipper(PhysicsSimulator physicsSimulator, Vector2 position)
        {

            base.Body = BodyFactory.CreateBody(physicsSimulator, position); //, 15, 51, 10f);

            // Offset
            //base.Body.Position = position - (new Vector2(-11, -11)); // Offsets according to texture
            
            // Flipper Fixture
            base.Fixtures.AddLast(FixtureFactory.CreateRectangle(15, 51, 10, base.Body, -(new Vector2(-11, -11)))); //Vector2.Zero, -MathHelper.ToRadians(40f), 0.5f);
            
            base.Fixtures.Last.Value.CollisionCategories = CollisionCategory.Cat4;
            base.Fixtures.Last.Value.CollidesWith = CollisionCategory.Cat2;

            this.DampningConstant = 350000;
            this.SpringConstant = 12500000;

            revoluteJoint = JointFactory.CreateFixedRevoluteJoint(physicsSimulator, base.Body, Vector2.Zero, position);

            //fixedAngleSpring = ControllerFactory.CreateFixedAngleSpring(physicsSimulator, base.Body, SpringConstant, DampningConstant);

            //fixedAngleSpring.TargetAngle = MathHelper.TwoPi - MathHelper.ToRadians(30f);

            base.Body.Rotation = MathHelper.TwoPi - MathHelper.ToRadians(30f);
        }

        public static void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(@"GameTextures\LeftFlipper");
        }

        public override void Update(float elapsedTime)
        {
            base.Update(elapsedTime);
        }

        public void HandleInput(bool leftHit)
        {
            if (leftHit)
            {
                //fixedAngleSpring.TargetAngle -= (MathHelper.Pi / 12);

                if (playOnce)
                {
                    SoundManager.SoundEffects["Flipper"].Play();

                    playOnce = false;
                }
            }
            else
            {
                //fixedAngleSpring.TargetAngle += (MathHelper.Pi / 12);

                playOnce = true;
            }

            //fixedAngleSpring.TargetAngle = MathHelper.Clamp(fixedAngleSpring.TargetAngle, (3 * MathHelper.PiOver2),  MathHelper.TwoPi - (MathHelper.ToRadians(30f)));
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            
            base.Draw(elapsedTime, spriteBatch, texture, Color.White);
        }

        private List<Vertices> CreateLeftFlipper()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(10, 1));
            vertices.Add(new Vector2(4, 3));
            vertices.Add(new Vector2(2, 5));
            vertices.Add(new Vector2(1, 9));
            vertices.Add(new Vector2(2, 14));
            vertices.Add(new Vector2(14, 29));
            vertices.Add(new Vector2(27, 37));
            vertices.Add(new Vector2(35, 43));
            vertices.Add(new Vector2(37, 45));
            vertices.Add(new Vector2(39, 47));
            //vertices.Add(new Vector2(40, 47));
            //vertices.Add(new Vector2(35, 35));
            vertices.Add(new Vector2(42, 41));
            vertices.Add(new Vector2(42, 39));
            vertices.Add(new Vector2(40, 37));
            vertices.Add(new Vector2(38, 33));
            vertices.Add(new Vector2(31, 26));
            vertices.Add(new Vector2(27, 19));
            vertices.Add(new Vector2(23, 13));
            vertices.Add(new Vector2(17, 5));
            vertices.Add(new Vector2(13, 2));
            vertices.Add(new Vector2(10, 1));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 23, vertices[counter].Y - 23);
            }

            return EarclipDecomposer.ConvexPartition(vertices);
        }
    }
}