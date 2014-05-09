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
    class CObject
    {
        #region Fields
        protected Model model;
        protected Vector3 position, rotation;
        protected float scale;
        protected string id;

        #region Get/Set
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
        public Vector3 Rotation
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
        public Model Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        #endregion

        #endregion
        
        #region Methods

        #region Constructor/Destructor
        public CObject(Model nmodel, Vector3 nposition, Vector3 nrotation, float nscale, string nid)
        {
            model = nmodel;
            position = nposition;
            rotation = nrotation;
            scale = nscale;
            id = nid;
        }
        public CObject()
        {
            model = null;
            position = Vector3.Zero;
            rotation = Vector3.Zero;
            scale = 0.0f;
            id = "null";
        }
        #endregion

        #region Collision
        /*private BoundingBox createMerged(Model model) // Use it for low quality computers.
        {
            BoundingBox b = new BoundingBox();
            BoundingBox aux;
            foreach (ModelMesh mesh in model.Meshes)
            {
                aux = BoundingBox.CreateFromSphere(mesh.BoundingSphere);
                BoundingBox.CreateMerged(b, aux);
            }
            return b;
        }*/
        protected bool isCollidingBB(Model model2)
        {
            BoundingBox BB1, BB2;
            foreach (ModelMesh mesh in model.Meshes) 
            {
                BB1 = BoundingBox.CreateFromSphere(mesh.BoundingSphere);
                foreach (ModelMesh mesh2 in model2.Meshes) 
                {
                    BB2 = BoundingBox.CreateFromSphere(mesh2.BoundingSphere);
                    if (BB1.Intersects(BB2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected bool isCollidingBS(Model model2)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMesh mesh2 in model2.Meshes)
                {
                    if (mesh.BoundingSphere.Intersects(mesh2.BoundingSphere))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Drawing
        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }
        }

        public void EditEffects()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Set the specular lighting to match the sky color.
                    effect.SpecularColor = new Vector3(0.6f, 0.4f, 0.2f);
                    effect.SpecularPower = 8;

                    // Set the fog to match the distant mountains
                    // that are drawn into the sky texture.
                    effect.FogEnabled = true;
                    effect.FogColor = new Vector3(0.15f);
                    effect.FogStart = 100;
                    effect.FogEnd = 3200;
                }
            }
        }
        #endregion
        #endregion
    }
}
