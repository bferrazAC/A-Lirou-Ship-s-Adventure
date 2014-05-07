#region File Description
//-----------------------------------------------------------------------------
// GeneratedGeometry.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
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

namespace GeneratedGeometry
{
    /// <summary>
    /// Sample showing how to use geometry that is programatically
    /// generated as part of the content pipeline build process.
    /// </summary>
    public class GeneratedGeometryGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        Ship.Camera camera;
        float aspectRatio;

        Ship.CObject terrain;
        Sky sky;
        Ship.LirouShip ship;
        Ship.Ring ring;
        List<Ship.CObject> collidableObjects;

        Ship.TerrainInfo terrainInfo;


        #endregion

        #region Initialization


        public GeneratedGeometryGame()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //IsMouseVisible = true;

            terrain = new Ship.CObject();
            terrain.ID = "terrain";

            aspectRatio = (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            Content.RootDirectory = "Content";

            ship = new Ship.LirouShip(new Vector3(0, 50 , 0), new Vector3(0, 0, 0), 1.0f, (float)(Math.PI / 50), 0.002f);
            ring = new Ship.Ring(new Vector3(30, 0, 30), (float)(Math.PI / 50), 0.08f);
            camera = new Ship.Camera(25, ship.Position);
            

            collidableObjects = new List<Ship.CObject>();
            //collidableObjects.Add(terrain);
            collidableObjects.Add(ship);
            collidableObjects.Add(ring);

            #if WINDOWS_PHONE
                        // Frame rate is 30 fps by default for Windows Phone.
                        TargetElapsedTime = TimeSpan.FromTicks(333333);

                        graphics.IsFullScreen = true;
            #endif
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            
            terrain.Model = Content.Load<Model>("Terrain/terrain");
            terrainInfo = terrain.Model.Tag as Ship.TerrainInfo;
            if (terrainInfo == null)
            // Avisar caso não seja associado a Tag ao modelo
            {
                string message = "The terrain model did not have a TerrainInfo " +
                    "object attached. Are you sure you are using the " +
                    "TerrainProcessor?";
                throw new InvalidOperationException(message);
            }
            sky = Content.Load<Sky>("sky");
            ship.LoadContent(this.Content);
            //ring.LoadContent(this.Content);
            ring.Model = Content.Load<Model>(@"Models\\ring");
        }

        
        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows the game to run logic.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            
            KeyboardState ks = Keyboard.GetState();

            HandleInput();
            collisionTerrain(ks);
            //ship.Update(getHeight,ks, camera, collidableObjects);
            camera.Update(Mouse.GetState(), gameTime, ship.Position, ship.Rotation, graphics);
            ring.Animate();

            base.Update(gameTime);
        }

        Vector3 position;
        float getHeight;
        public void collisionTerrain(KeyboardState ks)
        {
            position = ship.Position;
            if(terrainInfo.IsOnHeightmap(position))
            {
                getHeight = terrainInfo.GetHeight(position);
                float terrainScale = terrainInfo.TerrainScale();
                ship.Update(getHeight, terrainScale, ks, camera, collidableObjects);
            }
            if (ks.IsKeyDown(Keys.RightShift))
            {
                Debug.WriteLine("" + position.Y/100 + getHeight );
                Debug.Indent();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;

            device.Clear(Color.Black);
            
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1, 10000);
            Matrix view = Matrix.CreateLookAt(camera.Position, ship.Position, Vector3.Up);
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the terrain first, then the sky. This is faster than
            // drawing the sky first, because the depth buffer can skip
            // bothering to draw sky pixels that are covered up by the
            // terrain. This trick works because the code used to draw
            // the sky forces all the sky vertices to be as far away as
            // possible, and turns depth testing on but depth writes off.

            terrain.EditEffects();
            terrain.Draw(view, projection);

            ship.Draw(aspectRatio, view);
            ring.Draw(view,projection,aspectRatio);

            sky.Draw(view, projection);

            // If there was any alpha blended translucent geometry in
            // the scene, that would be drawn here, after the sky.

            base.Draw(gameTime);
        }


        /// <summary>
        /// Helper for drawing the terrain model.
        /// </summary>



        #endregion

        #region Handle Input


        /// <summary>
        /// Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Check for exit.
            if (currentKeyboardState.IsKeyDown(Keys.Escape) ||
                currentGamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
        }


        #endregion
    }


    #region Entry Point

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (GeneratedGeometryGame game = new GeneratedGeometryGame())
            {
                game.Run();
            }
        }
    }

    #endregion
}
