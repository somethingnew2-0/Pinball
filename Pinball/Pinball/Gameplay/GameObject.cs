using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace Pinball
{
    public abstract class GameObject
    {
        #region Status Properties

        public bool Alive { get; set; }
 
        #endregion

        #region Collision Data

        public Body Body { get; set; }

        private LinkedList<Fixture> fixtures = new LinkedList<Fixture>();
        public LinkedList<Fixture> Fixtures
        {
            get { return fixtures; }
            set { fixtures = value; }
        }


        #endregion
        
        #region Intialization

        /// <summary>
        /// The intializer of the GameObject, Geoms and Bodies need to be initialized here.
        /// </summary>
        public GameObject()
        {
            if (!Alive)
            {
                Alive = true;
            }

        }

        #endregion

        #region Updating Methods


        /// <summary>
        /// Update the GameObject.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        public virtual void Update(float elapsedTime)
        {
            //collidedThisFrame = false;
        }


        #endregion

        #region Drawing Methods


        /// <summary>
        /// Draw the gameplay object.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        /// <param name="spriteBatch">The SpriteBatch object used to draw.</param>
        /// <param name="sprite">The texture used to draw this object.</param>
        /// <param name="color">The color of the sprite.</param>
        public virtual void Draw(float elapsedTime, SpriteBatch spriteBatch,
            Texture2D sprite, Color color)
        {
            if ((spriteBatch != null) && (sprite != null) && (this.Body != null))
            {
                if (this is Ball == false)
                {
                     spriteBatch.Draw(sprite, this.Body.Position, null, color, this.Body.Rotation,
                         new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                         1f, SpriteEffects.None, 0f); 
                }
                else
                {
                    spriteBatch.Draw(sprite, this.Body.Position, null, color, 0f,
                        new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                        1f, SpriteEffects.None, 0f); 
                }
            }
            else
            {
                throw new ArgumentNullException("spriteBatch, sprite, or this.Body");
            }
        }

        /// <summary>
        /// Draw the gameplay object.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        /// <param name="spriteBatch">The SpriteBatch object used to draw.</param>
        /// <param name="sprite">The texture used to draw this object.</param>
        /// <param name="color">The color of the sprite.</param>
        /// <param name="zlayer">The Z-Layer the sprite is drawn at.</param>
        public virtual void Draw(float elapsedTime, SpriteBatch spriteBatch,
            Texture2D sprite, Color color, float zlayer)
        {
            if ((spriteBatch != null) && (sprite != null) && (this.Body != null))
            {
                spriteBatch.Draw(sprite, this.Body.Position, null, color, this.Body.Rotation,
                     new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                     1f, SpriteEffects.None, zlayer);
            }
            else
            {
                throw new ArgumentNullException("spriteBatch, sprite, or this.Body");
            }
        }

        /// <summary>
        /// Draw the gameplay object.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        /// <param name="spriteBatch">The SpriteBatch object used to draw.</param>
        /// <param name="sprite">The texture used to draw this object.</param>
        /// <param name="origin">The origin to rotate on for the texture.</param>
        /// <param name="color">The color of the sprite.</param>
        public virtual void Draw(float elapsedTime, SpriteBatch spriteBatch,
            Texture2D sprite, Vector2 origin, Color color)
        {
            if ((spriteBatch != null) && (sprite != null) && (this.Body != null))
            {
                spriteBatch.Draw(sprite, this.Body.Position, null, color, this.Body.Rotation,
                     origin,
                     1f, SpriteEffects.None, 0f);
            }
            else
            {
                throw new ArgumentNullException("spriteBatch, sprite, or this.Body");
            }
        }

        /// <summary>
        /// Draw the gameplay object.
        /// </summary>
        /// <param name="elapsedTime">The amount of elapsed time, in seconds.</param>
        /// <param name="spriteBatch">The SpriteBatch object used to draw.</param>
        /// <param name="sprite">The texture used to draw this object.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="color">The color of the sprite.</param>
        public virtual void Draw(float elapsedTime, SpriteBatch spriteBatch,
            Texture2D sprite, Rectangle? sourceRectangle, Color color)
        {
            if ((spriteBatch != null) && (sprite != null) && (this.Body != null))
            {
                spriteBatch.Draw(sprite, this.Body.Position, sourceRectangle, color, this.Body.Rotation,
                    new Vector2(sprite.Width / 2f, sprite.Height / 2f),
                    1f, SpriteEffects.None, 0f);
            }
            else
            {
                throw new ArgumentNullException("spriteBatch, sprite, or this.Body");
            }
        }


        #endregion

        #region Interaction Methods

        /// <summary>
        /// Defines the interaction between this GameObject and 
        /// a target GameObject when they touch.
        /// </summary>
        /// <param name="g1">The geom that is being hit.</param>
        /// <param name="g2">The geom that hit the object.</param>
        /// <param name="contactList">The list of contact points.</param>
        /// <returns>True if the objects should collide.</returns>
        public virtual bool CollisionHandler(Fixture g1, Fixture g2, Contact contactList)
        {
            return true;
        }
        
        #endregion
    }
}
