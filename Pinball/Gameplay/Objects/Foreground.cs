using System;
using System.Collections.Generic;
using System.Text;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class Foreground : GameObject
    {
        private static Texture2D foregroundTexture;

        public Foreground(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 240, 320, 1f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[6];

            // Red Geom Section
            base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateRedVertices(), 7f);

            // Blue Geom Section
            base.Geom[1] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateBlueVertices(), 3f);

            // Green Geom Section
            base.Geom[2] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateGreenVertices(), 7f);

            // Magenta Geom Section
            base.Geom[3] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateMagentaVertices(), 5f);

            // Orange Geom Section
            base.Geom[4] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateOrangeVertices(), 5f);

            // Yellow Geom Section
            base.Geom[5] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateYellowVertices(), 1f);

            foreach (Geom geometry in base.Geom)
            {
                //geometry.Tag = GameObjects.HighWall;

                geometry.CollisionCategories = Enums.CollisionCategories.Cat8;
                geometry.CollidesWith = Enums.CollisionCategories.Cat1;

                //geometry.Collision += CollisionHandler;
            }
        }

        public static void LoadContent(ContentManager contentManager)
        {
           foregroundTexture = contentManager.Load<Texture2D>(@"GameTextures\GameForeground");
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            base.Draw(elapsedTime, spriteBatch, foregroundTexture, Color.White, 0.5f);
        }

        //public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    if (g1.Tag.Equals(GameObjects.HighWall))
        //    {
        //        if (g2.Tag.Equals(GameObjects.NormalWall) || g2.Tag.Equals(GameObjects.LowWall) ||
        //            g2.Tag.Equals(GameObjects.NormalBall) || g2.Tag.Equals(GameObjects.LowBall))
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("g1 isn't a foreground geom.");
        //    }
        //}

        private Vertices CreateRedVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(37, 266));
            vertices.Add(new Vector2(34, 265));
            vertices.Add(new Vector2(30, 263));
            vertices.Add(new Vector2(30, 250));
            vertices.Add(new Vector2(30, 235));
            vertices.Add(new Vector2(23, 229));
            vertices.Add(new Vector2(23, 200));
            vertices.Add(new Vector2(23, 175));
            vertices.Add(new Vector2(23, 150));
            vertices.Add(new Vector2(23, 125));
            vertices.Add(new Vector2(23, 100));
            vertices.Add(new Vector2(23, 75));
            vertices.Add(new Vector2(23, 50));
            vertices.Add(new Vector2(23, 32));

            vertices.Add(new Vector2(23, 20));
            vertices.Add(new Vector2(23, 10));

            vertices.Add(new Vector2(10, 10));

            vertices.Add(new Vector2(10, 32));
            vertices.Add(new Vector2(10, 100));
            vertices.Add(new Vector2(10, 200));
            vertices.Add(new Vector2(10, 275));
            vertices.Add(new Vector2(37, 275));
            vertices.Add(new Vector2(37, 266));
            
            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateBlueVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(40, 29));
            vertices.Add(new Vector2(40, 50));
            vertices.Add(new Vector2(40, 75));
            vertices.Add(new Vector2(40, 100));
            vertices.Add(new Vector2(40, 125));
            vertices.Add(new Vector2(40, 150));
            vertices.Add(new Vector2(40, 175));
            vertices.Add(new Vector2(40, 200));
            vertices.Add(new Vector2(40, 227));
            vertices.Add(new Vector2(44, 232));
            vertices.Add(new Vector2(44, 247));
            vertices.Add(new Vector2(44, 262));
            vertices.Add(new Vector2(42, 264));
            vertices.Add(new Vector2(38, 266));
            vertices.Add(new Vector2(38, 275));
            vertices.Add(new Vector2(50, 275));
            vertices.Add(new Vector2(50, 200));
            vertices.Add(new Vector2(50, 100));
            vertices.Add(new Vector2(50, 29));
            vertices.Add(new Vector2(40, 29));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateGreenVertices()
        {
            Vertices vertices = new Vertices();

            //vertices.Add(new Vector2(106, 113));
            //vertices.Add(new Vector2(107, 108));
            //vertices.Add(new Vector2(108, 103));
            //vertices.Add(new Vector2(110, 94));
            //vertices.Add(new Vector2(113, 84));

            vertices.Add(new Vector2(115, 82));
            vertices.Add(new Vector2(119, 79));
            vertices.Add(new Vector2(121, 78));
            vertices.Add(new Vector2(121, 53));
            vertices.Add(new Vector2(121, 30));
            vertices.Add(new Vector2(124, 23));
            vertices.Add(new Vector2(130, 15));
            vertices.Add(new Vector2(138, 9));
            vertices.Add(new Vector2(146, 7));
            vertices.Add(new Vector2(161, 7));
            vertices.Add(new Vector2(170, 10));
            vertices.Add(new Vector2(178, 15));
            vertices.Add(new Vector2(183, 22));
            vertices.Add(new Vector2(187, 30));
            vertices.Add(new Vector2(187, 50));
            vertices.Add(new Vector2(187, 75));
            vertices.Add(new Vector2(187, 100));
            vertices.Add(new Vector2(187, 118));
            vertices.Add(new Vector2(195, 126));
            vertices.Add(new Vector2(202, 133));

            vertices.Add(new Vector2(207, 140));
            vertices.Add(new Vector2(210, 145));
            vertices.Add(new Vector2(210, 155));
            vertices.Add(new Vector2(207, 160));
            vertices.Add(new Vector2(204, 168));
            vertices.Add(new Vector2(200, 172));
            vertices.Add(new Vector2(195, 174));

            vertices.Add(new Vector2(195, 180));
            vertices.Add(new Vector2(207, 177));
            vertices.Add(new Vector2(214, 170));
            vertices.Add(new Vector2(214, 150));
            vertices.Add(new Vector2(214, 141));
            vertices.Add(new Vector2(207, 134));
            vertices.Add(new Vector2(206, 130));
            
            vertices.Add(new Vector2(202, 124));
            vertices.Add(new Vector2(192, 116));
            vertices.Add(new Vector2(192, 50));
            vertices.Add(new Vector2(192, 1));
            vertices.Add(new Vector2(105, 1));
            vertices.Add(new Vector2(105, 82));

            //vertices.Add(new Vector2(105, 91));
            //vertices.Add(new Vector2(102, 100));
            //vertices.Add(new Vector2(100, 110));
            //vertices.Add(new Vector2(102, 113));
            //vertices.Add(new Vector2(106, 113));

            //vertices.Add(new Vector2(115, 82));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateMagentaVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(194, 174));
            vertices.Add(new Vector2(190, 172));
            vertices.Add(new Vector2(188, 168));
            vertices.Add(new Vector2(189, 163));
            vertices.Add(new Vector2(194, 157));
            vertices.Add(new Vector2(196, 153));
            vertices.Add(new Vector2(198, 150));
            vertices.Add(new Vector2(195, 145));


            vertices.Add(new Vector2(193, 146));
            vertices.Add(new Vector2(184, 136));
            vertices.Add(new Vector2(172, 124));
            vertices.Add(new Vector2(172, 100));
            vertices.Add(new Vector2(173, 75));
            vertices.Add(new Vector2(173, 50));
            vertices.Add(new Vector2(173, 36));
            vertices.Add(new Vector2(168, 27));
            vertices.Add(new Vector2(162, 23));
            vertices.Add(new Vector2(156, 21));
            vertices.Add(new Vector2(149, 21));
            vertices.Add(new Vector2(142, 24));
            vertices.Add(new Vector2(137, 29));
            vertices.Add(new Vector2(134, 35));
            vertices.Add(new Vector2(134, 42));
            vertices.Add(new Vector2(134, 50));
            vertices.Add(new Vector2(134, 60));
            vertices.Add(new Vector2(134, 70));
            vertices.Add(new Vector2(134, 87));

            //vertices.Add(new Vector2(133, 88));
            //vertices.Add(new Vector2(132, 93));
            //vertices.Add(new Vector2(132, 101));
            //vertices.Add(new Vector2(132, 110));
            //vertices.Add(new Vector2(132, 117));

            //vertices.Add(new Vector2(142, 116));
            //vertices.Add(new Vector2(143, 104));
            //vertices.Add(new Vector2(144, 94));

            vertices.Add(new Vector2(145, 87));
            vertices.Add(new Vector2(145, 40));
            vertices.Add(new Vector2(165, 40));
            vertices.Add(new Vector2(165, 100));
            vertices.Add(new Vector2(165, 130));
            vertices.Add(new Vector2(183, 146));
            vertices.Add(new Vector2(175, 159));
            vertices.Add(new Vector2(174, 168));
            vertices.Add(new Vector2(183, 176));
            vertices.Add(new Vector2(194, 180));
            vertices.Add(new Vector2(194, 174));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateOrangeVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(189, 266));
            vertices.Add(new Vector2(185, 264));
            vertices.Add(new Vector2(183, 261));
            vertices.Add(new Vector2(183, 247));
            vertices.Add(new Vector2(183, 232));
            vertices.Add(new Vector2(200, 214));
            vertices.Add(new Vector2(210, 203));
            vertices.Add(new Vector2(222, 190));
            vertices.Add(new Vector2(222, 175));
            vertices.Add(new Vector2(222, 150));
            vertices.Add(new Vector2(222, 125));
            vertices.Add(new Vector2(222, 104));
            vertices.Add(new Vector2(220, 102));
            vertices.Add(new Vector2(218, 103));
            vertices.Add(new Vector2(214, 107));
            vertices.Add(new Vector2(210, 112));
            vertices.Add(new Vector2(215, 117));
            vertices.Add(new Vector2(215, 185));
            vertices.Add(new Vector2(176, 232));
            vertices.Add(new Vector2(176, 273));
            vertices.Add(new Vector2(189, 273));
            vertices.Add(new Vector2(189, 266));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateYellowVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(194, 105));
            vertices.Add(new Vector2(201, 96));
            vertices.Add(new Vector2(206, 91));
            vertices.Add(new Vector2(217, 86));
            vertices.Add(new Vector2(222, 86));
            vertices.Add(new Vector2(229, 89));
            vertices.Add(new Vector2(234, 94));
            vertices.Add(new Vector2(237, 101));
            vertices.Add(new Vector2(237, 125));
            vertices.Add(new Vector2(237, 150));
            vertices.Add(new Vector2(237, 175));
            vertices.Add(new Vector2(237, 197));
            vertices.Add(new Vector2(227, 208));
            vertices.Add(new Vector2(212, 224));
            vertices.Add(new Vector2(205, 232));
            vertices.Add(new Vector2(197, 241));
            vertices.Add(new Vector2(197, 251));
            vertices.Add(new Vector2(197, 262));
            vertices.Add(new Vector2(194, 265));
            vertices.Add(new Vector2(190, 266));
            vertices.Add(new Vector2(190, 273));
            vertices.Add(new Vector2(215, 273));
            vertices.Add(new Vector2(215, 241));
            vertices.Add(new Vector2(250, 203));
            vertices.Add(new Vector2(250, 150));
            vertices.Add(new Vector2(250, 80));
            vertices.Add(new Vector2(225, 70));
            vertices.Add(new Vector2(215, 70));
            vertices.Add(new Vector2(194, 80));
            vertices.Add(new Vector2(194, 105));

            vertices.SubDivideEdges(20f);

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }
    }
}