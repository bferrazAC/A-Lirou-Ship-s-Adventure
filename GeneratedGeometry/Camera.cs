#region Library Imports
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Ship
{
    class Camera
    {
        #region Fields
        Vector3 position;
        float distanceFromReference;        
        Vector3 targetPosition;
        Vector3 targetRotation;
        Vector2 rotation;
        Vector3 UpVector;

        Vector2 mouseOldPos;

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        #endregion

        #region Constructors

        public Camera(float distanceFromReference, Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
            position = targetPosition + new Vector3(0, 0, distanceFromReference);
            this.distanceFromReference = distanceFromReference;
            rotation = new Vector2(0.5f, 0.0f);
        }

        #endregion

        #region Methods

        #region Update

        public void Update(MouseState mouse, GameTime gameTime, Vector3 targetPosition, Vector3 targetRotation, GraphicsDeviceManager graphics)
        {
            this.targetPosition = targetPosition;
            this.targetRotation = targetRotation;

            #region InputHandling

            mouseOldPos.X = graphics.PreferredBackBufferWidth / 2;
            mouseOldPos.Y = graphics.PreferredBackBufferHeight / 2;
            
            float speed = MathHelper.Pi;

            if (true)//(mouse.RightButton == ButtonState.Pressed)
            {
                rotation.X += (mouse.X - mouseOldPos.X) * Constants.SMOOTHING;
                if (rotation.Y + (mouse.Y - mouseOldPos.Y) * Constants.SMOOTHING < Constants.QUARTERCIRCLE && rotation.Y + (mouse.Y - mouseOldPos.Y) * 0.001f > -Constants.QUARTERCIRCLE)
                {
                    rotation.Y += (mouse.Y - mouseOldPos.Y) * Constants.SMOOTHING;
                }

                if (rotation.X >= Constants.MAXFORWARD)
                {
                    rotation.X -= Constants.TURNOVER;
                }

                if (rotation.X < Constants.MINBACKWARD)
                {
                    rotation.X += Constants.TURNOVER;
                }

                position.X = (float)(Math.Cos(rotation.X * speed) * Math.Cos(rotation.Y * speed) * distanceFromReference + targetPosition.X);
                position.Z = (float)(Math.Sin(rotation.X * speed) * Math.Cos(rotation.Y * speed) * distanceFromReference + targetPosition.Z);
                position.Y = (float)(Math.Sin(rotation.Y * speed) * distanceFromReference + targetPosition.Y);
                //Console.WriteLine("" + rotation);
            }
            else
            {
                //rotation.Y = (float)(targetRotation.X / Math.PI);
                rotation = new Vector2(0.5f, 0);
                position = targetPosition + new Vector3(0, 0, distanceFromReference);
                //position.X = (float)(Math.Cos(rotation.X * speed) * Math.Cos(rotation.Y * speed) * distanceFromReference + targetPosition.X);
                //position.Z = (float)(Math.Sin(rotation.X * speed) * Math.Cos(rotation.Y * speed) * distanceFromReference + targetPosition.Z);
                //position.Y = (float)(Math.Sin(rotation.Y * speed) * distanceFromReference + targetPosition.Y);
            }
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); 
            #endregion
        }

        #endregion

        #endregion
    }
}
