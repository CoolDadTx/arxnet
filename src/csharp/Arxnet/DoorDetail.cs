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

        [Obsolete("Use Position")]
        public int x
        {
            get => _position.X;
            set => _position.X = value;
        }

        [Obsolete("Use Position")]
        public int y
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        public Point Position 
        {
            get => _position;
            set => _position = value;
        }

        #region Private Members

        //TODO: Remove when obsolete removed
        private Point _position;
        #endregion
    }
}