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
    class Ring : CObject
    {
        #region Fields

        float rotatingSpeed;
        Matrix modelMatrix;

        #region Get/Set

        public Vector3 Position
        {
            get
            {
                Vector3 pos = modelMatrix.Translation;
                return pos;
            }
            set
            {
                modelMatrix.Translation = value;
            }
        }

        public float RotatingSpeed
        {
            get
            {
                return rotatingSpeed;
            }
            set
            {
                rotatingSpeed = value;
            }
        }

        #endregion

        #endregion

        #region Constructors

        public Ring(Vector3 m_position, Vector3 rotation, float rotatingSpeed, float scale)
        {
            this.position = m_position;
            this.rotatingSpeed = rotatingSpeed;
            this.rotation = rotation;
            this.scale = scale;

            this.modelMatrix = Matrix.Identity;
            this.modelMatrix *= Matrix.CreateTranslation(m_position);
            this.modelMatrix *= Matrix.CreateScale(scale);
            this.modelMatrix *= Matrix.CreateRotationX(rotation.X);
            this.modelMatrix *= Matrix.CreateRotationY(rotation.Y);
            this.modelMatrix *= Matrix.CreateRotationZ(rotation.Z);
            
        }

        #endregion

        #region Methods

        #region Animating

        public void Animate()
        {
            if (modelMatrix.Up.Y + rotatingSpeed >= Constants.MAXFORWARD)
            {
                modelMatrix.Up = Vector3.Zero;
            }
            else
            {
                modelMatrix *= Matrix.CreateRotationY(rotatingSpeed);
                modelMatrix *= Matrix.CreateRotationX(rotatingSpeed);
                modelMatrix *= Matrix.CreateRotationZ(rotatingSpeed);
            }
        }

        #endregion

        public int GetPoint()
        {
            int point = 50;
            return point;
        }

        #region ContentLoad/Drawing

        public void LoadContent(ContentManager Content)
        {
            model = Content.Load<Model>(@"Models\\p1_wedge");
        }

        public void Draw(Matrix view, Matrix projection, float aspectRatio)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    Matrix[] transforms = new Matrix[model.Bones.Count];
                    model.CopyAbsoluteBoneTransformsTo(transforms);

                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * modelMatrix ;
                    effect.View = view;
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), aspectRatio,
                        1.0f, 1000000.0f);
                }

                mesh.Draw();
            }
        }

        #endregion

        #endregion

    }
}
