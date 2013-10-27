using System;
using System.Collections.Generic;
using System.Text;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class RightFlipper : GameObject
    {
        private static Texture2D texture;

        //private static Texture2D secondtexture;

        private bool playOnce = true;

        //private SoundEffectInstance instanceCheck = null;

        //private bool hit = false;
        //private float elapsedDraw = 0f;

        FixedRevoluteJoint revoluteJoint;
        FixedAngleSpring fixedAngleSpring;

        public float DampningConstant { get; set; }

        public float SpringConstant { get; set; }

        public RightFlipper(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            //base.Body = BodyFactory.Instance.CreatePolygonBody(physicsSimulator, CreateRightFlipper(), 10f);

            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 15, 51, 10f);

            // Offset
            base.Body.Position = position - (new Vector2(11, -11)); // Offsets according to texture
            
            base.Geom = new Geom[1];

            // Flipper Geom
            //base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateRightFlipper(), 1.0f);
            base.Geom[0] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, base.Body, 15, 51, Vector2.Zero, MathHelper.ToRadians(40f), 0.5f);
            //base.Geom[0].LocalVertices.SubDivideEdges(51f);
            base.Geom[0].CollisionCategories = Enums.CollisionCategories.Cat4;
            base.Geom[0].CollidesWith = Enums.CollisionCategories.Cat2;

            //Vertices extendedAABB = Vertices.CreateRectangle(30, 60);
            //base.Geom[0].AABB.Update(ref extendedAABB);

            this.DampningConstant = 350000;
            this.SpringConstant = 12500000;

            revoluteJoint = JointFactory.Instance.CreateFixedRevoluteJoint(physicsSimulator, base.Body, position);

            fixedAngleSpring = ControllerFactory.Instance.CreateFixedAngleSpring(physicsSimulator, base.Body, SpringConstant, DampningConstant);

            fixedAngleSpring.TargetAngle = MathHelper.ToRadians(30f);

            base.Body.Rotation = MathHelper.ToRadians(30f);

            //base.Geom[0].Tag = GameObjects.Flipper;

            //base.Geom[0].Collision += CollisionHandler;
        }
                
        public static void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(@"GameTextures\RightFlipper");

            //secondtexture = contentManager.Load<Texture2D>(@"GameTextures\RightFlipperHit");
        }

        public override void  Update(float elapsedTime)
        {
             base.Update(elapsedTime);
        }

        public void HandleInput(bool rightHit)
        {
            if (rightHit)
            {
                //fixedAngleSpring.TargetAngle += MathHelper.ToRadians(100f);

                //fixedAngleSpring.TargetAngle = MathHelper.PiOver2;

                fixedAngleSpring.TargetAngle += (MathHelper.Pi / 12);

                if (playOnce)
                {
                    SoundManager.SoundEffects["Flipper"].Play();

                    playOnce = false;
                }
                
            }
            else
            {
                //fixedAngleSpring.TargetAngle = MathHelper.ToRadians(30f);

                fixedAngleSpring.TargetAngle -= (MathHelper.Pi / 12);

                playOnce = true;
            }

            fixedAngleSpring.TargetAngle = MathHelper.Clamp(fixedAngleSpring.TargetAngle, MathHelper.ToRadians(30f), MathHelper.PiOver2); //+ MathHelper.ToRadians(35f));
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        { 
            
             base.Draw(elapsedTime, spriteBatch, texture, Color.White);

             //if (hit)
             //{
             //    if (elapsedDraw < 0.25f)
             //    {
             //        base.Draw(elapsedTime, spriteBatch, secondtexture, Color.White);

             //        elapsedDraw += elapsedTime;
             //    }
             //    else
             //    {
             //        hit = false;

             //        elapsedDraw = 0.0f;
             //    }
             //}
        }

        //public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    //if (g1.Tag.Equals(GameObjects.Flipper))
        //    //{
        //        if (g2.CollisionCategories == Enums.CollisionCategories.Cat2)
        //        {
        //            //hit = true;

        //            // Makes sure there isn't more than one instance playing at once
        //            if (instanceCheck == null || instanceCheck.State == SoundState.Stopped)
        //            {
        //                instanceCheck = SoundManager.SoundEffects["Click" + RandomMath.Random.Next(1, 3).ToString()].Play();
        //            }
                
        //        }
        //    //}
        //    //else
        //    //{
        //    //    throw new ArgumentException("g1 isn't the flipper geom");
        //    //}

        //    return true;
        //}

        private Vertices CreateRightFlipper()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(10, 1));
            vertices.Add(new Vector2(13, 2));
            vertices.Add(new Vector2(17, 5));
            vertices.Add(new Vector2(23, 13));
            vertices.Add(new Vector2(27, 19));
            vertices.Add(new Vector2(31, 26));
            vertices.Add(new Vector2(38, 33));
            vertices.Add(new Vector2(40, 37));
            vertices.Add(new Vector2(42, 39));
            vertices.Add(new Vector2(42, 41));
            //vertices.Add(new Vector2(35, 35));
            //vertices.Add(new Vector2(40, 47));
            vertices.Add(new Vector2(39, 47));
            vertices.Add(new Vector2(37, 45));
            vertices.Add(new Vector2(35, 43));
            vertices.Add(new Vector2(27, 37));
            vertices.Add(new Vector2(14, 29));
            vertices.Add(new Vector2(2, 14));
            vertices.Add(new Vector2(1, 9));
            vertices.Add(new Vector2(2, 5));
            vertices.Add(new Vector2(4, 3));
            vertices.Add(new Vector2(10, 1));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(-vertices[counter].X + 23, vertices[counter].Y - 23);
            }

            return vertices;
        }
    }
}