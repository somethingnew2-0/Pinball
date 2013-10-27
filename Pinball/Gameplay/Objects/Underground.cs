using System;
using System.Collections.Generic;
using System.Text;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

namespace Pinball
{
    public class Underground : GameObject
    {
        public Underground(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 240, 320, 1f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[2];

            // Upper Bridge Geom Section
            base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateUpperBridgeVertices(), 2f);

            // Lower Bridge Geom Section
            base.Geom[1] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateLowerBridgeVertices(), 2f);

            foreach (Geom geometry in base.Geom)
            {
                //geometry.Tag = GameObjects.LowWall;

                geometry.CollisionCategories = CollisionCategory.Cat10;
                geometry.CollidesWith = CollisionCategory.Cat3;

                // geometry.Collision += CollisionHandler;
            }
        }

        //public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    //if (g1.Tag.Equals(GameObjects.LowWall))
        //    //{
        //        //if (g2.Tag.Equals(GameObjects.NormalWall) || g2.Tag.Equals(GameObjects.HighWall) ||
        //        //    g2.Tag.Equals(GameObjects.NormalBall) || g2.Tag.Equals(GameObjects.HighBall))
        //        //{
        //        //    return false;
        //        //}
        //        //else
        //        //{
        //        //    return true;
        //        //}
        //    //}
        //    //else
        //    //{
        //    //    throw new ArgumentException("g1 isn't a underground geom.");
        //    //}
        //}

        private Vertices CreateUpperBridgeVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(34, 52));
            vertices.Add(new Vector2(35, 49));
            vertices.Add(new Vector2(37, 45));
            
            vertices.Add(new Vector2(39, 41));


            vertices.Add(new Vector2(44, 34));
            vertices.Add(new Vector2(48, 31));
            vertices.Add(new Vector2(55, 26));
            vertices.Add(new Vector2(60, 23));
            vertices.Add(new Vector2(66, 20));
            vertices.Add(new Vector2(74, 17));
            vertices.Add(new Vector2(71, 12));
            vertices.Add(new Vector2(53, 20));
            vertices.Add(new Vector2(44, 27));

            vertices.Add(new Vector2(39, 35));

            vertices.Add(new Vector2(34, 43));
            vertices.Add(new Vector2(34, 52));

            //vertices.Add(new Vector2(39, 41));
            //vertices.Add(new Vector2(44, 34));
            
            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateLowerBridgeVertices()
        {
            Vertices vertices = new Vertices();


            vertices.Add(new Vector2(100, 24));
            vertices.Add(new Vector2(92, 24));
            vertices.Add(new Vector2(86, 27));
            
            vertices.Add(new Vector2(79, 30));
            vertices.Add(new Vector2(75, 32));
            vertices.Add(new Vector2(72, 34));
            vertices.Add(new Vector2(69, 35));
            vertices.Add(new Vector2(65, 37));
            vertices.Add(new Vector2(63, 39));
            vertices.Add(new Vector2(60, 41));
            vertices.Add(new Vector2(58, 43));
            vertices.Add(new Vector2(55, 46));
            vertices.Add(new Vector2(52, 48));
            vertices.Add(new Vector2(50, 52));

            vertices.Add(new Vector2(48, 57));
            vertices.Add(new Vector2(46, 64));
            vertices.Add(new Vector2(46, 70));

            vertices.Add(new Vector2(50, 70));

            vertices.Add(new Vector2(53, 56));
            vertices.Add(new Vector2(65, 46));
            vertices.Add(new Vector2(79, 37));
            //vertices.Add(new Vector2(79, 30));

            vertices.Add(new Vector2(86, 34));
            vertices.Add(new Vector2(92, 32));
            vertices.Add(new Vector2(100, 33));
            vertices.Add(new Vector2(100, 24));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }
            
    }
}