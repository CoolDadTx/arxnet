/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 * 
 * Converted code from ARX C++ (http://www.landbeyond.net/arx/index.php)
 * Code converted using C++ to C# Code Converter, Tangible Software (https://www.tangiblesoftwaresolutions.com/)
 */
using System;
using System.Drawing;

namespace P3Net.Arx
{
    public class AnimFrame
    {
        public Point Offset
        {
            get => _offset;
            set => _offset = value;
        }

        [Obsolete("Use Offset")]
        public int xOffset
        {
            get => _offset.X;
            set => _offset.X = value;
        }

        [Obsolete("Use Offset")]
        public int yOffset
        {
            get => _offset.Y;
            set => _offset.Y = value;
        }

        public int image { get; set; }
        public int duration { get; set; }

        #region Private Members

        //None for most animations
        private Point _offset;
        #endregion
    }
}