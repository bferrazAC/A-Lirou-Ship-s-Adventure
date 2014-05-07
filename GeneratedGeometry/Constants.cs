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
    class Constants
    {
        public
            const double PI = Math.PI, DOUBLEPI = Math.PI * 2, ZERO = 0.0f;
        public
            const float SMOOTHING = 0.001f, QUARTERCIRCLE = 0.5f, MAXFORWARD = (float)Math.PI * 2, MINBACKWARD = 0.0f, TURNOVER = 2.0f;
    }
}