using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;

namespace Pinball
{
    public class Background : GameObject
    {
        private static Texture2D backgroundTexture;

        public Background(PhysicsSimulator physicsSimulator, Vector2 position)
        {
            base.Body = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 240, 320, 1000f);
            base.Body.IsStatic = true;
            base.Body.Position = position;

            base.Geom = new Geom[16];

            // Yellow One Geom Section
            base.Geom[0] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateYellowOneVertices(), 5f);
        
            // Green One Geom Section
            base.Geom[1] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateGreenOneVertices(), 3f);
        
            // Blue One Geom Section
            base.Geom[2] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateBlueOneVertices(), 5f);
        
            // Magenta One Geom Section
            base.Geom[3] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateMagentaOneVertices(), 4f);
            
            // Red One Geom Section
            base.Geom[4] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateRedOneVertices(), 5f);

            // Orange One Geom Section
            base.Geom[5] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateOrangeOneVertices(), 3f);

            // LightBlue One Geom Section
            base.Geom[6] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateLightBlueOneVertices(), 3f);

            // Yellow Two Geom Section
            base.Geom[7] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateYellowTwoVertices(), 1f);

            // Green Two Geom Section
            base.Geom[8] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateGreenTwoVertices(), 5f);

            //// Light Blue Two Geom Section
            //base.Geom[9] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateLightBlueTwoVertices(), 5f);

            // Blue Two Geom Section
            base.Geom[9] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateBlueTwoVertices(), 5f);

            // Green Three Geom Section
            base.Geom[10] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateGreenThreeVertices(), 2f);

            // Red Two Geom Section
            base.Geom[11] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateRedTwoVertices(), 2f);

            // Magenta Two Geom Section
            //base.Geom[12] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateMagentaTwoVertices(), 0.75f);
            base.Geom[12] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, base.Body, 3, 58, new Vector2(89, 89), 0f, 1f);
            base.Geom[12].LocalVertices.SubDivideEdges(58f);

            // Magenta Three Geom Section
            base.Geom[13] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateMagentaThreeVertices(), 1f);
            
            // Red Three Geom Section
            base.Geom[14] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, base.Body, 3, 22, new Vector2(89, -81), 0f, 1f);
            base.Geom[14].LocalVertices.SubDivideEdges(22f);

            // Orange Two Geom Section
            base.Geom[15] = GeomFactory.Instance.CreatePolygonGeom(physicsSimulator, base.Body, CreateOrangeTwoVertices(), 1f);

            foreach (Geom geometry in base.Geom)
            {
                //geometry.Tag = GameObjects.NormalWall;

                geometry.CollisionCategories = CollisionCategory.Cat9;
                geometry.CollidesWith = CollisionCategory.Cat2;

                //geometry.Collision += CollisionHandler;
            }

            //// Change this so it's not always a wall
            //base.Geom[9].Collision += UnderLeftCircleBumperWallCollsionHandler;
        
        }

        public static void LoadContent(ContentManager contentManager)
        {
            backgroundTexture = contentManager.Load<Texture2D>(@"GameTextures\GameBackground");
        }

        public void Draw(float elapsedTime, SpriteBatch spriteBatch)
        {
            base.Draw(elapsedTime, spriteBatch, backgroundTexture, Color.White);
        }

        //public override bool CollisionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    //if (g1.Tag.Equals(GameObjects.NormalWall))
        //    //{
        //        //if (g2.Tag.Equals(GameObjects.HighWall) || g2.Tag.Equals(GameObjects.LowBall) ||
        //        //    g2.Tag.Equals(GameObjects.HighBall) || g2.Tag.Equals(GameObjects.LowWall))
        //        //{
        //        //    return false;
        //        //}
        //        //else
        //        //{
        //        //    return true;
        //        }
        //    //}
        //    //else
        //    //{
        //    //    throw new ArgumentException("g1 isn't a background geom.");
        //    //}

        //    //return base.CollisionHandler(g1, g2, contactList);
        //}

        //public bool UnderLeftCircleBumperWallCollsionHandler(Geom g1, Geom g2, ContactList contactList)
        //{
        //    if (g1.Tag.Equals(GameObjects.NormalWall))
        //    {
        //        if (g2.Tag.Equals(GameObjects.HighWall) || g2.Tag.Equals(GameObjects.LowBall) ||
        //            g2.Tag.Equals(GameObjects.HighBall) || g2.Tag.Equals(GameObjects.LowWall))
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
        //        throw new ArgumentException("g1 isn't a background geom.");
        //    }
        //}

        private Vertices CreateYellowOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(1, 1));
            vertices.Add(new Vector2(1, 100));
            vertices.Add(new Vector2(1, 200));
            vertices.Add(new Vector2(1, 319));
            vertices.Add(new Vector2(44, 319));
            vertices.Add(new Vector2(44, 304));
            vertices.Add(new Vector2(37, 301));
            vertices.Add(new Vector2(31, 298));
            vertices.Add(new Vector2(27, 298));
            vertices.Add(new Vector2(20, 293));
            vertices.Add(new Vector2(16, 293));
            vertices.Add(new Vector2(16, 239));
            vertices.Add(new Vector2(16, 218));
            vertices.Add(new Vector2(20, 210));
            vertices.Add(new Vector2(24, 207));
            vertices.Add(new Vector2(27, 206));
            vertices.Add(new Vector2(33, 202));
            vertices.Add(new Vector2(33, 183));
            vertices.Add(new Vector2(33, 167));
            vertices.Add(new Vector2(29, 156));
            vertices.Add(new Vector2(26, 148));
            vertices.Add(new Vector2(24, 141));
            vertices.Add(new Vector2(19, 127));
            vertices.Add(new Vector2(13, 110));
            vertices.Add(new Vector2(8, 97));
            vertices.Add(new Vector2(4, 89));
            vertices.Add(new Vector2(4, 86));
            vertices.Add(new Vector2(3, 83));
            vertices.Add(new Vector2(3, 60));
            vertices.Add(new Vector2(3, 40));
            vertices.Add(new Vector2(3, 18));
            vertices.Add(new Vector2(4, 15));
            vertices.Add(new Vector2(6, 12));
            vertices.Add(new Vector2(9, 11));
            vertices.Add(new Vector2(9, 1));
            vertices.Add(new Vector2(1, 1));


            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateGreenOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(10, 11));
            vertices.Add(new Vector2(15, 15));
            vertices.Add(new Vector2(16, 17));
            vertices.Add(new Vector2(16, 40));
            vertices.Add(new Vector2(16, 60));
            vertices.Add(new Vector2(16, 79));


            //vertices.Add(new Vector2(21, 88));
            //vertices.Add(new Vector2(27, 100));
            //vertices.Add(new Vector2(33, 112));
            //vertices.Add(new Vector2(42, 126));
            //vertices.Add(new Vector2(44, 125));
            //vertices.Add(new Vector2(42, 118));
            //vertices.Add(new Vector2(37, 104));
            //vertices.Add(new Vector2(33, 92));

            //vertices.Add(new Vector2(32, 88));

            vertices.Add(new Vector2(32, 79));

            vertices.Add(new Vector2(32, 74));
            vertices.Add(new Vector2(32, 58));
            vertices.Add(new Vector2(36, 47));
            vertices.Add(new Vector2(39, 41));

            vertices.Add(new Vector2(39, 36));
            vertices.Add(new Vector2(23, 36));
            vertices.Add(new Vector2(23, 27));
            vertices.Add(new Vector2(23, 19));
            vertices.Add(new Vector2(29, 13));

            vertices.Add(new Vector2(34, 9));

            vertices.Add(new Vector2(10, 11));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateBlueOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(40, 40));

            vertices.Add(new Vector2(43, 35));
            vertices.Add(new Vector2(47, 32));
            vertices.Add(new Vector2(50, 38));
            vertices.Add(new Vector2(52, 46));
            vertices.Add(new Vector2(52, 48));
            vertices.Add(new Vector2(49, 53));
            vertices.Add(new Vector2(47, 56));
            vertices.Add(new Vector2(46, 63));
            vertices.Add(new Vector2(46, 72));
            vertices.Add(new Vector2(46, 79));
            vertices.Add(new Vector2(50, 87));
            vertices.Add(new Vector2(54, 97));
            vertices.Add(new Vector2(58, 100));
            vertices.Add(new Vector2(63, 99));
            vertices.Add(new Vector2(65, 97));
            vertices.Add(new Vector2(65, 84));
            vertices.Add(new Vector2(63, 74));
            vertices.Add(new Vector2(59, 61));
            vertices.Add(new Vector2(56, 53));
            vertices.Add(new Vector2(56, 45));
            vertices.Add(new Vector2(52, 35));
            vertices.Add(new Vector2(47, 24));
            vertices.Add(new Vector2(45, 22));
            vertices.Add(new Vector2(42, 33));
            vertices.Add(new Vector2(39, 26));

            vertices.Add(new Vector2(39, 35));

            vertices.Add(new Vector2(40, 40));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateMagentaOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(35, 10));

            vertices.Add(new Vector2(38, 8));
            vertices.Add(new Vector2(49, 8));
            vertices.Add(new Vector2(54, 9));
            vertices.Add(new Vector2(60, 16));
            vertices.Add(new Vector2(64, 24));

            vertices.Add(new Vector2(66, 30));
            vertices.Add(new Vector2(67, 28));

            vertices.Add(new Vector2(65, 22));
            vertices.Add(new Vector2(80, 15));
            vertices.Add(new Vector2(96, 9));
            vertices.Add(new Vector2(114, 4));
            vertices.Add(new Vector2(135, 2));
            vertices.Add(new Vector2(156, 2));
            vertices.Add(new Vector2(193, 2));
            
            vertices.Add(new Vector2(193, 1));
            vertices.Add(new Vector2(156, 1));
            vertices.Add(new Vector2(100, 1));
            vertices.Add(new Vector2(50, 1));
            vertices.Add(new Vector2(35, 1));

            vertices.Add(new Vector2(35, 10));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateRedOneVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(67, 32));
            vertices.Add(new Vector2(70, 44));
            vertices.Add(new Vector2(74, 57));
            vertices.Add(new Vector2(78, 70));
            vertices.Add(new Vector2(86, 84));
            vertices.Add(new Vector2(95, 101));
            vertices.Add(new Vector2(103, 113));
            vertices.Add(new Vector2(106, 113));
            vertices.Add(new Vector2(109, 97));
            vertices.Add(new Vector2(113, 85));
            vertices.Add(new Vector2(113, 82));
            vertices.Add(new Vector2(105, 76));
            vertices.Add(new Vector2(99, 67));
            vertices.Add(new Vector2(98, 60));
            vertices.Add(new Vector2(99, 51));
            vertices.Add(new Vector2(106, 42));
            vertices.Add(new Vector2(116, 35));
            vertices.Add(new Vector2(117, 30));
            vertices.Add(new Vector2(116, 25));
            vertices.Add(new Vector2(114, 24));
            vertices.Add(new Vector2(103, 24));
            vertices.Add(new Vector2(92, 24));
            vertices.Add(new Vector2(81, 29));
            vertices.Add(new Vector2(70, 34));
            vertices.Add(new Vector2(68, 28));

            vertices.Add(new Vector2(67, 32));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateOrangeOneVertices()
        {
            Vertices vertices = new Vertices();



            vertices.Add(new Vector2(194, 2));
            vertices.Add(new Vector2(206, 6));
            vertices.Add(new Vector2(213, 11));
            vertices.Add(new Vector2(219, 20));
            vertices.Add(new Vector2(223, 34));
            vertices.Add(new Vector2(223, 60));
            vertices.Add(new Vector2(223, 90));
            vertices.Add(new Vector2(223, 120));
            vertices.Add(new Vector2(223, 150));
            vertices.Add(new Vector2(223, 170));
            vertices.Add(new Vector2(223, 200));
            vertices.Add(new Vector2(223, 230));
            vertices.Add(new Vector2(223, 260));
            vertices.Add(new Vector2(223, 278));

            vertices.Add(new Vector2(225, 278));
            vertices.Add(new Vector2(225, 200));
            vertices.Add(new Vector2(225, 100));
            vertices.Add(new Vector2(225, 1));

            vertices.Add(new Vector2(194, 1));
            vertices.Add(new Vector2(194, 2));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateLightBlueOneVertices()
        {
            Vertices vertices = new Vertices();

            //vertices.Add(new Vector2(222, 278));

            vertices.Add(new Vector2(210, 219));
             
            vertices.Add(new Vector2(210, 200));
            vertices.Add(new Vector2(210, 170));
            vertices.Add(new Vector2(210, 150));
            vertices.Add(new Vector2(210, 120));

            vertices.Add(new Vector2(210, 91));
            
            vertices.Add(new Vector2(208, 91));
             
            vertices.Add(new Vector2(203, 99));
            vertices.Add(new Vector2(195, 104));

            vertices.Add(new Vector2(189, 107));

            vertices.Add(new Vector2(194, 106));

            vertices.Add(new Vector2(208, 114));
            vertices.Add(new Vector2(198, 133));
            vertices.Add(new Vector2(186, 158));
            vertices.Add(new Vector2(181, 168));
            vertices.Add(new Vector2(175, 178));
            vertices.Add(new Vector2(175, 185));
            vertices.Add(new Vector2(179, 185));
            vertices.Add(new Vector2(184, 181));
            vertices.Add(new Vector2(189, 180));
            vertices.Add(new Vector2(194, 186));
            vertices.Add(new Vector2(194, 191));
            vertices.Add(new Vector2(191, 197));
            vertices.Add(new Vector2(190, 200));
            vertices.Add(new Vector2(198, 208));

            vertices.Add(new Vector2(208, 219));
            vertices.Add(new Vector2(210, 219));

            //vertices.Add(new Vector2(222, 279));
            //vertices.Add(new Vector2(222, 278));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateYellowTwoVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(188, 107));
            vertices.Add(new Vector2(180, 109));
            vertices.Add(new Vector2(174, 115));
            vertices.Add(new Vector2(170, 125));
            vertices.Add(new Vector2(169, 130));
            vertices.Add(new Vector2(169, 133));
            vertices.Add(new Vector2(172, 135));
            vertices.Add(new Vector2(179, 126));
            vertices.Add(new Vector2(186, 116));
            vertices.Add(new Vector2(193, 107));

            vertices.Add(new Vector2(188, 107));

            vertices.SubDivideEdges(5f);

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateGreenTwoVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(209, 280));

            vertices.Add(new Vector2(209, 282));
            vertices.Add(new Vector2(204, 289));
            vertices.Add(new Vector2(196, 294));
            vertices.Add(new Vector2(189, 302));
            vertices.Add(new Vector2(186, 307));
            vertices.Add(new Vector2(185, 311));
            vertices.Add(new Vector2(183, 319));
            vertices.Add(new Vector2(225, 319));

            vertices.Add(new Vector2(225, 300));
            vertices.Add(new Vector2(225, 280));
            vertices.Add(new Vector2(209, 280));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        //private Vertices CreateLightBlueTwoVertices()
        //{
        //    Vertices vertices = new Vertices();


        //    for (int counter = 0; counter < vertices.Count; counter++)
        //    {
        //        vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
        //    }

        //    return vertices;
        //}
        
        private Vertices CreateBlueTwoVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(149, 124));
            vertices.Add(new Vector2(153, 120));
            vertices.Add(new Vector2(156, 115));
            vertices.Add(new Vector2(157, 108));
            vertices.Add(new Vector2(157, 101));
            vertices.Add(new Vector2(151, 92));
            vertices.Add(new Vector2(145, 89));
            vertices.Add(new Vector2(137, 87));
            vertices.Add(new Vector2(132, 88));
            vertices.Add(new Vector2(132, 103));
            vertices.Add(new Vector2(132, 116));
            vertices.Add(new Vector2(140, 116));
            vertices.Add(new Vector2(145, 120));
            vertices.Add(new Vector2(149, 124));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateGreenThreeVertices()
        {            
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(31, 267));
            vertices.Add(new Vector2(31, 278));
            vertices.Add(new Vector2(43, 286));
            vertices.Add(new Vector2(59, 294));
            vertices.Add(new Vector2(63, 293));
            vertices.Add(new Vector2(63, 290));
            vertices.Add(new Vector2(52, 281));
            vertices.Add(new Vector2(45, 276));
            vertices.Add(new Vector2(38, 270));
            vertices.Add(new Vector2(31, 267));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateRedTwoVertices()
        {
            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(197, 268));
            vertices.Add(new Vector2(196, 268));
            vertices.Add(new Vector2(189, 273));
            vertices.Add(new Vector2(179, 280));
            vertices.Add(new Vector2(170, 287));
            vertices.Add(new Vector2(167, 290));
            vertices.Add(new Vector2(167, 293));
            vertices.Add(new Vector2(171, 294));
            vertices.Add(new Vector2(175, 293));
            vertices.Add(new Vector2(182, 288));
            vertices.Add(new Vector2(190, 283));
            vertices.Add(new Vector2(197, 277));
            vertices.Add(new Vector2(197, 268));
            
            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        //private Vertices CreateMagentaTwoVertices()
        //{
        //    Vertices vertices = new Vertices();


        //    vertices.Add(new Vector2(210, 278));
        //    vertices.Add(new Vector2(210, 270));
        //    vertices.Add(new Vector2(210, 260));
        //    vertices.Add(new Vector2(210, 250));
        //    vertices.Add(new Vector2(210, 240));
        //    vertices.Add(new Vector2(210, 230));
        //    vertices.Add(new Vector2(210, 220));

        //    vertices.Add(new Vector2(208, 220));
        //    vertices.Add(new Vector2(208, 230));
        //    vertices.Add(new Vector2(208, 240));
        //    vertices.Add(new Vector2(208, 250));
        //    vertices.Add(new Vector2(208, 260));
        //    vertices.Add(new Vector2(208, 270));

        //    vertices.Add(new Vector2(208, 278));

        //    vertices.Add(new Vector2(210, 278));


        //    for (int counter = 0; counter < vertices.Count; counter++)
        //    {
        //        vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
        //    }

        //    return vertices;
        //}

        private Vertices CreateMagentaThreeVertices()
        {
            Vertices vertices = new Vertices();

            //vertices.Add(new Vector2(210, 90));
            //vertices.Add(new Vector2(210, 80));
            //vertices.Add(new Vector2(210, 70));
            //vertices.Add(new Vector2(210, 60));

            vertices.Add(new Vector2(210, 67));

            vertices.Add(new Vector2(210, 47));
            vertices.Add(new Vector2(206, 40));
            vertices.Add(new Vector2(199, 34));
            vertices.Add(new Vector2(199, 48));
            vertices.Add(new Vector2(204, 57));
            vertices.Add(new Vector2(208, 67));

            vertices.Add(new Vector2(210, 67));

            //vertices.Add(new Vector2(208, 75));
            //vertices.Add(new Vector2(208, 80));
            //vertices.Add(new Vector2(208, 85));
            //vertices.Add(new Vector2(208, 90));
            //vertices.Add(new Vector2(210, 90));

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }

        private Vertices CreateOrangeTwoVertices()
        {
            Vertices vertices = new Vertices();

            //vertices.Add(new Vector2(10, 11));
            //vertices.Add(new Vector2(15, 15));
            //vertices.Add(new Vector2(16, 17));
            //vertices.Add(new Vector2(16, 40));
            //vertices.Add(new Vector2(16, 60));
            //vertices.Add(new Vector2(16, 79));
            vertices.Add(new Vector2(17, 80));

            vertices.Add(new Vector2(21, 88));
            vertices.Add(new Vector2(27, 100));
            vertices.Add(new Vector2(33, 112));
            vertices.Add(new Vector2(41, 126));
            vertices.Add(new Vector2(43, 126));
            vertices.Add(new Vector2(44, 125));
            vertices.Add(new Vector2(44, 123));
            vertices.Add(new Vector2(42, 118));
            vertices.Add(new Vector2(37, 104));
            vertices.Add(new Vector2(33, 92));
            vertices.Add(new Vector2(32, 80));
            //vertices.Add(new Vector2(32, 88));
            //vertices.Add(new Vector2(32, 74));
            //vertices.Add(new Vector2(32, 58));
            //vertices.Add(new Vector2(36, 47));
            //vertices.Add(new Vector2(39, 41));

            //vertices.Add(new Vector2(39, 36));
            //vertices.Add(new Vector2(23, 36));
            //vertices.Add(new Vector2(23, 27));
            //vertices.Add(new Vector2(23, 19));
            //vertices.Add(new Vector2(29, 13));

            //vertices.Add(new Vector2(34, 9));

            //vertices.Add(new Vector2(10, 11));

            vertices.Add(new Vector2(17, 80));

            vertices.SubDivideEdges(5f);

            for (int counter = 0; counter < vertices.Count; counter++)
            {
                vertices[counter] = new Vector2(vertices[counter].X - 120, vertices[counter].Y - 160);
            }

            return vertices;
        }
    }
}