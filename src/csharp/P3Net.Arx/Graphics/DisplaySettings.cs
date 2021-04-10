/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 */
using System;

namespace P3Net.Arx.Graphics
{
    public class DisplaySettings
    {
        public bool FullScreen { get; set; }

        public GraphicsMode GraphicsMode { get; set; }

        public int Height { get; set; } = 480;

        public int Width { get; set; } = 640;
    }
}
