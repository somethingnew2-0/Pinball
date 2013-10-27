using System;
using System.Collections.Generic;
using System.Text;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework.Content;
using FarseerGames.FarseerPhysics.Factories;

namespace Pinball
{
    public class Plunger : GameObject
    {
        private static List<Texture2D> textures = new List<Texture2D>();

        private int power = 0;
        private bool applyForce = false;
 
        public Plunger(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 12, 39, 50f);
            base.Body.Position = position;
            base.Body.IsStatic = true;

            base.Geom = new Geom[1];

            base.Geom[0] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, base.Body, 12, 39, 3f);

            // base.Geom[0].Tag = GameObjects.Plunger;

            base.Geom[0].CollisionCategories = CollisionCategory.Cat5;
            base.Geom[0].CollidesWith = CollisionCategory.Cat2;

            base.Geom[0].OnCollision += CollisionHandler;
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

        public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        {
            //if (g1.Tag.Equals(GameObjects.Plunger))
            //{
                if (g2.CollisionCategories == CollisionCategory.Cat2)
                {
                    if (applyForce)
                    {
                        g2.Body.ApplyImpulse(new Vector2(0, -power));

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
            //}
            //else
            //{
            //    throw new ArgumentException("g1 isn't the flipper geom");
            //}

        }
    }
}