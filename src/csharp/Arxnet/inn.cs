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
            //TODO: Use auto prop once obsolete removed
            get => _location;
            set => _location = value;
        }

        #region Private Members

        private Point _location;
        #endregion
    }
}