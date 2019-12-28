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
    public class Inn
    {
        public float costMultiplier { get; set; }

        public int jobProbability { get; set; }

        public string name { get; set; }

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
            //TODO: Use auto prop once obsolete removed
            get => _position;
            set => _position = value;
        }

        #region Private Members

        private Point _position;
        #endregion
    }
}