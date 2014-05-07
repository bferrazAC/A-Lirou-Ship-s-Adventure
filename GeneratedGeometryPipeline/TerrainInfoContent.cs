using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework;

namespace Ship
{
    public class TerrainInfoContent
    {
        // As alturas relativas a cada posição do mapa
        public float[,] Height
        {
            get { return height; }
        }
        float[,] height;
        //A escala das dimensões x,z do terreno
        public float TerrainScale
        {
            get { return terrainScale; }
        }
        private float terrainScale;
        //A escala das alturas do mapa
        


        public TerrainInfoContent(PixelBitmapContent<float> bitmap,
            float terrainScale, float terrainBumpiness)
        {
            this.terrainScale = terrainScale;
            
            height = new float[bitmap.Width, bitmap.Height];
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    height[x, y] = (bitmap.GetPixel(x, y) - 1) * terrainBumpiness;
                }
            }
        }
    }


    [ContentTypeWriter]
    public class TerrainInfoContentWriter : ContentTypeWriter<TerrainInfoContent>
    {
        protected override void Write(ContentWriter output, TerrainInfoContent value)
        {
            output.Write(value.TerrainScale);
            
            output.Write(value.Height.GetLength(0));
            output.Write(value.Height.GetLength(1));
            foreach (float height in value.Height)
            {
                output.Write(height);
            }
        }

        /// <summary>
        /// Tells the content pipeline what CLR type the
        /// data will be loaded into at runtime.
        /// </summary>
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return "Ship.TerrainInfo, " +
                "GeneratedGeometry, Version=1.0.0.0, Culture=neutral";
        }


        /// <summary>
        /// Tells the content pipeline what worker type
        /// will be used to load the data.
        /// </summary>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Ship.TerrainInfoReader, " +
                "GeneratedGeometry, Version=1.0.0.0, Culture=neutral";
        }
    }
}
