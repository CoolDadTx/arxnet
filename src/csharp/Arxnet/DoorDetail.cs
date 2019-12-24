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
    public class DoorDetail
    {
        //TODO: Make Directions
        public int direction { get; set; }

        public int level { get; set; }

        [Obsolete("Use Location")]
        public int x
        {
            get => _location.X;
            set => _location.X = value;
        }

        [Obsolete("Use Location")]
        public int y
        {
            get => _location.Y;
            set => _location.Y = value;
        }

        public Point Location 
        {
            get => _location;
            set => _location = value;
        }

        #region Private Members

        //TODO: Remove when obsolete removed
        private Point _location;
        #endregion
    }
}